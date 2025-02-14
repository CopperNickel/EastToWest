using Microsoft.Diagnostics.Tracing;

namespace EastToWest;

/// <summary>
/// Trace Event Description
/// </summary>
public sealed class EventDescription
{
    /// <summary>
    /// Original Trace Event
    /// </summary>
    public TraceEvent Event { get; }

    /// <summary>
    /// Trace Event Payload
    /// </summary>
    public IReadOnlyDictionary<string, object> Payload { get; }

    /// <summary>
    /// Constructor, Description for given trace event
    /// </summary>
    /// <param name="traceEvent">Original Trace Event</param>
    /// <exception cref="ArgumentNullException">When Trace Event is null</exception>
    public EventDescription(TraceEvent traceEvent)
    {
        Event = traceEvent ?? throw new ArgumentNullException(nameof(traceEvent));

        var payload = new Dictionary<string, object>();

        foreach (var name in Event.PayloadNames)
        {
            payload[name] = Event.PayloadByName(name);
        }

        Payload = payload;
    }

    /// <summary>
    /// String (to debug only) representation
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"{Event.EventName}";
    }
}
