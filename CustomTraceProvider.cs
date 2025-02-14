using System.Globalization;

namespace EastToWest;

/// <summary>
/// Custom Trace Provider
/// </summary>
public sealed class CustomTraceProvider : AbstractTraceProvider {
  private readonly Dictionary<string, string> m_ExtraParameters = [];

  /// <summary>
  /// Event Source Name
  /// </summary>
  public override string EventsSourceName { get; }

  /// <summary>
  /// Custom Trace Provider constructor
  /// </summary>
  /// <param name="eventsSourceName">Events Source Name</param>
  /// <param name="extraParameters">Extra Parameters if any</param>
  /// <exception cref="ArgumentNullException">When eventsSourceName is null</exception>
  public CustomTraceProvider(string eventsSourceName, params IEnumerable<KeyValuePair<string, object>> extraParameters) {
    EventsSourceName = string.IsNullOrEmpty(eventsSourceName)
      ? throw new ArgumentNullException(nameof(eventsSourceName), $"{nameof(eventsSourceName)} must not be null or empty")
      : eventsSourceName;

    if (extraParameters is not null) {
      foreach (var prm in extraParameters) {
        var key = prm.Key;
        var value = prm.Value is null
            ? ""
            : (prm.Value is IFormattable formattable ? formattable.ToString(default, CultureInfo.InvariantCulture) : prm.ToString());

        m_ExtraParameters.TryAdd(key, value);
      }
    }
  }

  /// <summary>
  /// Extra Arguments
  /// </summary>
  /// <returns>Extra Arguments as a dictionary</returns>
  protected override IDictionary<string, string> ToDictionary() => m_ExtraParameters;
}

