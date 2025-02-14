# EastToWest

Simple Library to listen ETW (Trace Events for Windows) from different providers.

## Usage
```csharp

using EastToWest;

...

int procesId = // Process to listen given as PID, 0 to listen the current process

using CancellationTokenSource cts = new CancellationTokenSource(1000);

// Infnite loop (stops on cancellation) to listen ETW events
await foreach(var evt in EventListener.Events(processId, cts.Token, new WellKnownTraceProvider)) {
  Console.WriteLine(evt);
}
```