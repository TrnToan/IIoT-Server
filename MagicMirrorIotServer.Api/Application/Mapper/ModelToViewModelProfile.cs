using MagicMirrorIotServer.Api.Application.Queries.Metrics;

namespace MagicMirrorIotServer.Api.Application.Mapper;

public class ModelToViewModelProfile : Profile
{
	public ModelToViewModelProfile()
	{
		CreateMap<EonNode, EonNodeViewModel>();
		CreateMap<Device, DeviceViewModel>();
		CreateMap<Tag, TagViewModel>();
		CreateMap<TagReading<double>, DoubleTagReadingViewModel>();
	}
}
