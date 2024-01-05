namespace MagicMirrorIotServer.Api.Application.Queries;

public class TimeRangeQuery
{
    public DateTime StartTime { get; set; } = DateTime.MinValue;
    public DateTime EndTime { get; set; } = DateTime.UtcNow.AddHours(7);
}
