namespace EastToWest;

/// <summary>
/// Well-known events trace provider
/// </summary>
/// <remarks>
/// Constructor 
/// </remarks>
/// <param name="eventsSourceName">Events source name</param>
/// <seealso cref="https://learn.microsoft.com/en-us/dotnet/core/diagnostics/well-known-event-providers"/>
public sealed class WellKnownTraceProvider(string eventsSourceName) : AbstractTraceProvider {
  /// <summary>
  /// Well-known events trace providers names
  /// </summary>
  /// <seealso cref="https://learn.microsoft.com/en-us/dotnet/core/diagnostics/well-known-event-providers"/>
  public const string DotNetRuntime = "Microsoft-Windows-DotNETRuntime";
  public const string DotNetCore = "Microsoft-DotNETCore-SampleProfiler";
  public const string DependencyInjection = "Microsoft-Extensions-DependencyInjection";
  public const string ArrayPool = "System.Buffers.ArrayPoolEventSource";
  public const string NetHttp = "System.Net.Http";
  public const string NetNameResolution = "System.Net.NameResolution";
  public const string NetSockets = "System.Net.Sockets";
  public const string TplEventSource = "System.Threading.Tasks.TplEventSource";

  /// <summary>
  /// Events Source Name
  /// </summary>
  public override string EventsSourceName { get; } = string.IsNullOrWhiteSpace(eventsSourceName) ? DotNetRuntime : eventsSourceName;
}
