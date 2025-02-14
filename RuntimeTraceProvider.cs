using System.Globalization;

namespace EastToWest;

/// <summary>
/// Run time trace events provider
/// </summary>
public sealed class RuntimeTraceProvider : AbstractTraceProvider {
  /// <summary>
  /// Events source name
  /// </summary>
  public override string EventsSourceName => "System.Runtime";

  /// <summary>
  /// Refresh interval
  /// </summary>
  public TimeSpan RefreshInterval { get; init; } = TimeSpan.FromSeconds(1);

  /// <summary>
  /// Run time provider's extra parameters
  /// </summary>
  /// <returns>Run time provider's extra parameters as a dictionary</returns>
  protected override IDictionary<string, string> ToDictionary() => new Dictionary<string, string>() {
    { "EventCounterIntervalSec", RefreshInterval.TotalSeconds.ToString(CultureInfo.InvariantCulture)}
  };
}

