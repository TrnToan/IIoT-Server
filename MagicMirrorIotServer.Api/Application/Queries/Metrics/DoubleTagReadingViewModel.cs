namespace MagicMirrorIotServer.Api.Application.Queries.Metrics;

public class DoubleTagReadingViewModel
{
    public double Value { get; private set; }
    public DateTime Timestamp { get; private set; }

    public DoubleTagReadingViewModel(double value, DateTime timestamp)
    {
        Value = value;
        Timestamp = timestamp;
    }
}
