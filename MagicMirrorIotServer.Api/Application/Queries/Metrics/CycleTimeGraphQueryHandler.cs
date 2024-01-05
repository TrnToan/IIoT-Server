namespace MagicMirrorIotServer.Api.Application.Queries.Metrics;

public class CycleTimeGraphQueryHandler : IRequestHandler<CycleTimeGraphQuery, IEnumerable<CycleTimeGraphViewModel>>
{
    private readonly IEonNodeRepository _eonNodeRepository;
    private readonly ITagReadingRepository _tagReadingRepository;
    private readonly IMapper _mapper;

    public CycleTimeGraphQueryHandler(IEonNodeRepository eonNodeRepository, ITagReadingRepository tagReadingRepository, 
        IMapper mapper)
    {
        _eonNodeRepository = eonNodeRepository;
        _tagReadingRepository = tagReadingRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CycleTimeGraphViewModel>> Handle(CycleTimeGraphQuery request, CancellationToken cancellationToken)
    {
        var node = await _eonNodeRepository.GetNodeWithIdAsync(request.EonNodeId);
        if (node is null)
        {
            throw new EntityNotFoundException(typeof(EonNode), request.EonNodeId);
        }

        List<CycleTimeGraphViewModel> cycleTimeOfDeviceGraphs = new List<CycleTimeGraphViewModel>();
        foreach (var device in node.Devices)
        {
            var tagToRead = await _eonNodeRepository.GetTagAsync(request.EonNodeId, device.DeviceId, "cycleTime");
            var doubleTagReadings = await _tagReadingRepository.GetTagReadingsByTagId(tagToRead.Id, request.StartTime, request.EndTime);
            var tagReadingVMs = _mapper.Map<IEnumerable<TagReading<double>>, IEnumerable<DoubleTagReadingViewModel>>(doubleTagReadings);
            CycleTimeGraphViewModel deviceCycleTimeGraph = new(device.DeviceId, tagReadingVMs);
            cycleTimeOfDeviceGraphs.Add(deviceCycleTimeGraph);
        }

        return cycleTimeOfDeviceGraphs;
    }
}
