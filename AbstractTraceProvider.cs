using Microsoft.Diagnostics.NETCore.Client;
using System.Diagnostics.Tracing;

namespace EastToWest;

/// <summary>
/// Abstract trace provider
/// </summary>
public abstract class AbstractTraceProvider : ITraceProvider
{
    /// <summary>
    /// Events Source Names
    /// </summary>
    /// <seealso cref="https://learn.microsoft.com/en-us/dotnet/core/diagnostics/well-known-event-providers"/>
    public abstract string EventsSourceName { get; }

    /// <summary>
    /// Minimum queue length
    /// </summary>
    public int QueueLength { get; init; } = EventListener.DefaultQueueLength;

    /// <summary>
    /// Event level
    /// </summary>
    public EventLevel EventLevel { get; init; } = EventLevel.Verbose;

    /// <summary>
    /// Extra arguments dictionary
    /// </summary>
    /// <returns></returns>
    protected virtual IDictionary<string, string> ToDictionary() => new Dictionary<string, string>();

    /// <summary>
    /// Build Event Pipe Provider from settings ans extra arguments
    /// </summary>
    /// <returns>Event Pipe Providers</returns>
    public EventPipeProvider ToProvider()
    {
        return new(EventsSourceName,
            EventLevel,
            keywords: -1,
            arguments: ToDictionary());
    }

    /// <summary>
    /// String (for debug only) representation
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"{EventsSourceName} source, {EventLevel}";
    }
}

