using Microsoft.Diagnostics.NETCore.Client;

namespace EastToWest;

/// <summary>
/// Trace Provider
/// </summary>
public interface ITraceProvider {
  /// <summary>
  /// Minimum queue length
  /// </summary>
  int QueueLength { get; }

  /// <summary>
  /// Creates Event Pipe Provider from trace provider settings
  /// </summary>
  /// <returns>Event Pipe Provider</returns>
  EventPipeProvider ToProvider();
}

