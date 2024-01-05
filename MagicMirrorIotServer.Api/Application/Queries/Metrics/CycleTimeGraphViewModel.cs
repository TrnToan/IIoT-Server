namespace MagicMirrorIotServer.Api.Application.Queries.Metrics;

public class CycleTimeGraphViewModel
{
    public string DeviceId { get; private set; }
    public IEnumerable<DoubleTagReadingViewModel> CycleTimeData { get; private set; }

    public CycleTimeGraphViewModel(string deviceId, IEnumerable<DoubleTagReadingViewModel> cycleTimeData)
    {
        DeviceId = deviceId;
        CycleTimeData = cycleTimeData;
    }
}
