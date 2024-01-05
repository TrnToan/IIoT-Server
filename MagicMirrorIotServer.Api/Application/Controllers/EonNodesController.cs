using MagicMirrorIotServer.Api.Hubs;
using Microsoft.AspNetCore.Mvc;

namespace MagicMirrorIotServer.Api.Application.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EonNodesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly NotificationHub _hub;

    public EonNodesController(IMediator mediator, NotificationHub hub)
    {
        _mediator = mediator;
        _hub = hub;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AddEonNodeCommand command)
    {
        try
        {
            bool result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorMessage.FromException(ex));
        }
    }

    [HttpPost]
    [Route("{eonNodeId}/devices")]
    public async Task<IActionResult> AddDeviceToEonNodeAsync([FromRoute] string eonNodeId, [FromBody] CreateDeviceViewModel device)
    {
        var command = new AddDeviceToEonNodeCommand(eonNodeId, device.DeviceId, device.DeviceName, device.ProtocolId, device.DeviceProtocol);
        try
        {
            bool result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorMessage.FromException(ex));
        }
    }

    [HttpPost]
    [Route("{eonNodeId}/devices/{deviceId}/tags")]
    public async Task<IActionResult> AddTagToDeviceAsync([FromRoute] string eonNodeId, [FromRoute] string deviceId, [FromBody] CreateTagViewModel tag)
    {
        var command = new AddTagToDeviceCommand(eonNodeId, deviceId, tag.TagId, tag.TagName, tag.TagType);
        try
        {
            bool result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorMessage.FromException(ex));
        }
    }

    [HttpGet]
    public async Task<IEnumerable<EonNodeViewModel>> GetNodesAsync([FromQuery]EonNodesQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet]
    [Route("{eonNodeId}/devices")]
    public async Task<IEnumerable<DeviceViewModel>> GetDevicesAsync(string eonNodeId)
    {
        DevicesQuery query = new DevicesQuery(eonNodeId);
        return await _mediator.Send(query);
    }

    [HttpGet]
    [Route("{eonNodeId}/devices/{deviceId}/tags")]
    public async Task<IEnumerable<TagViewModel>> GetTagsAsync(string eonNodeId, string deviceId)
    {
        TagsQuery query = new TagsQuery(eonNodeId, deviceId);
        return await _mediator.Send(query);
    }

    [HttpGet]
    [Route("{eonNodeId}/devices/{deviceId}/tags/{tagId}")]
    public async Task<TagViewModel> GetTagAsync(string eonNodeId, string deviceId, string tagId)
    {
        TagQuery query = new TagQuery(eonNodeId, deviceId, tagId);
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{eonNodeId}")]
    public async Task<IActionResult> RemoveAsync(string eonNodeId)
    {
        RemoveEonNodeCommand command = new RemoveEonNodeCommand(eonNodeId);
        try
        {
            bool result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorMessage.FromException(ex));
        }
    }

    [HttpDelete]
    [Route("{eonNodeId}/devices/{deviceId}")]
    public async Task<IActionResult> RemoveDeviceAsync(string eonNodeId, string deviceId)
    {
        RemoveDeviceFromNodeCommand command = new RemoveDeviceFromNodeCommand(eonNodeId, deviceId);
        try
        {
            bool result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorMessage.FromException(ex));
        }
    }

    [HttpDelete]
    [Route("{eonNodeId}/devices/{deviceId}/tags/{tagId}")]
    public async Task<IActionResult> RemoveTagAsync(string eonNodeId, string deviceId, string tagId)
    {
        RemoveTagFromDeviceCommand command = new RemoveTagFromDeviceCommand(eonNodeId, deviceId, tagId);
        try
        {
            bool result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorMessage.FromException(ex));
        }
    }

    // MQTT Test
    [HttpPost]
    [Route("{eonNodeId}/MQTT")]
    public async Task<IActionResult> SendAsync(string eonNodeId)
    {
        await _hub.SendConfig(eonNodeId);
        return Ok();
    }
}