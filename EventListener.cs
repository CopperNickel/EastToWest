using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;

namespace EastToWest;

/// <summary>
/// Trace Events Listener
/// </summary>
public static class EventListener
{
    public const int DefaultQueueLength = 100_000;

    /// <summary>
    /// Trace events, infinite loop until cancelled
    /// </summary>
    /// <param name="processId">Process given by its id (PID) to listen, 0 to listen the current process</param>
    /// <param name="token">Token to cancel listening</param>
    /// <param name="traceEventsProviders">Trace events providers to use</param>
    /// <returns>Async enumeration of trace events</returns>
    public static async IAsyncEnumerable<EventDescription> Events(
        int processId = default,
        [EnumeratorCancellation]
        CancellationToken token = default,
        params IEnumerable<ITraceProvider> traceEventsProviders)
    {
        var providerFactories = traceEventsProviders is IReadOnlyList<ITraceProvider> factoriesList 
            ? factoriesList
            : traceEventsProviders?.ToArray() ?? [];

        if (providerFactories.Count <= 0)
        {
            yield break;
        }

        if (processId <= 0)
        {
            processId = Environment.ProcessId;
        }

        var queueLength = providerFactories.Max(factory => factory.QueueLength);

        if (queueLength <= 0)
        {
            queueLength = DefaultQueueLength;
        }

        var channel = Channel.CreateBounded<EventDescription>(new BoundedChannelOptions(queueLength)
        {
            FullMode = BoundedChannelFullMode.DropOldest
        });

        var providers = providerFactories
            .Select(factory => factory.ToProvider())
            .ToList();

        var streamTask = Task.Run(() =>
        {
            var client = new DiagnosticsClient(processId);

            using var session = client.StartEventPipeSession(providers, false);
            using var source = new EventPipeEventSource(session.EventStream);

            token.Register(() => session.Stop());

            try
            {
                source.Dynamic.All += obj => channel.Writer.TryWrite(new EventDescription(obj));
                source.Process();
            }
            finally
            {
                channel.Writer.TryComplete();
            }
        }, CancellationToken.None);

        await foreach (var item in channel.Reader.ReadAllAsync(CancellationToken.None))
        {
            yield return item;
        }

        await streamTask;
    }
}
