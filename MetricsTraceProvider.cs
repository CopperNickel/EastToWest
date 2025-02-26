﻿using System.Globalization;

namespace EastToWest;

/// <summary>
/// Metrics (telemetry, say Open Telemetry) provider
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="metrics">Metrics to listen</param>
public sealed class MetricsTraceProvider(string? metrics) : AbstractTraceProvider {
  /// <summary>
  /// Default MaxTimeSeries
  /// </summary>
  public const int DefaultMaxTimeSeries = 100_000;

  /// <summary>
  /// Default MaxHistograms
  /// </summary>
  public const int DefaultMaxHistograms = 20;

  /// <summary>
  /// Default RefreshInterval
  /// </summary>
  public const double DefaultRefreshIntervalInSeconds = 1.0;

  /// <summary>
  /// Events Source Name
  /// </summary>
  public override string EventsSourceName => "System.Diagnostics.Metrics";

  /// <summary>
  /// Metrics to listen
  /// </summary>
  public string Metrics { get; init; } = metrics ?? "";

  /// <summary>
  /// Metrics Refresh interval
  /// </summary>
  public TimeSpan RefreshInterval { get; init; } = TimeSpan.FromSeconds(DefaultRefreshIntervalInSeconds);

  /// <summary>
  /// Max Series
  /// </summary>
  public int MaxTimeSeries { get; init; } = DefaultMaxTimeSeries;

  /// <summary>
  /// Max Histograms
  /// </summary>
  public int MaxHistograms { get; init; } = DefaultMaxHistograms;

  /// <summary>
  /// Metrics parameters
  /// </summary>
  /// <returns>Metrics parameters as a dictionary</returns>
  protected override IDictionary<string, string> ToDictionary() => new Dictionary<string, string>() {
    ["Metrics"] = Metrics ?? "",
    ["RefreshInterval"] = RefreshInterval.TotalSeconds.ToString(CultureInfo.InvariantCulture),
    ["MaxTimeSeries"] = MaxTimeSeries.ToString(CultureInfo.InvariantCulture),
    ["MaxHistograms"] = MaxHistograms.ToString(CultureInfo.InvariantCulture)
  };
}

