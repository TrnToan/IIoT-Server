namespace MagicMirrorIotServer.Api.Infrastructure.Caches;

public class TagValueCache
{
    private List<TagValue<bool>> _boolMetrics;
    private List<TagValue<int>> _intMetrics;
    private List<TagValue<float>> _floatMetrics;

    public TagValueCache()
    {
        _boolMetrics = new List<TagValue<bool>>();
        _intMetrics = new List<TagValue<int>>();
        _floatMetrics = new List<TagValue<float>>();
    }

    public void AddTag(string nodeId, string deviceId, string tagId, object tagValue)
    {
        if (tagValue is bool)
        {
            _boolMetrics.Add(new TagValue<bool>(nodeId, deviceId, tagId, (bool)tagValue));
        }
        else if (tagValue is int)
        {
            _intMetrics.Add(new TagValue<int>(nodeId, deviceId, tagId, (int)tagValue));
        }
        else if (tagValue is float)
        {
            _floatMetrics.Add(new TagValue<float>(nodeId, deviceId, tagId, (float)tagValue));
        }
    }

    public void RemoveTags(string nodeId, string deviceId, string tagId)
    {
        _boolMetrics.RemoveAll(t => t.EonNodeId == nodeId && t.DeviceId == deviceId && t.TagId == tagId);
        _intMetrics.RemoveAll(t => t.EonNodeId == nodeId && t.DeviceId == deviceId && t.TagId == tagId);
        _floatMetrics.RemoveAll(t => t.EonNodeId == nodeId && t.DeviceId == deviceId && t.TagId == tagId);
    }

    public void UpdateTag(string nodeId, string deviceId, string tagId, object tagValue)
    {
        if (tagValue is bool)
        {
            var existedValue = GetBoolTagValue(nodeId, deviceId, tagId);
            existedValue.Value = (bool)tagValue;
        }
        else if (tagValue is int)
        {
            var existedValue = GetIntTagValue(nodeId, deviceId, tagId);
            existedValue.Value = (int)tagValue;
        }
        else if (tagValue is float)
        {
            var existedValue = GetFloatTagValue(nodeId, deviceId, tagId);
            existedValue.Value = (float)tagValue;
        }
    }

    public bool CheckTagExistence(string nodeId, string deviceId, string tagId)
    {
        return _boolMetrics.Exists(t => t.EonNodeId == nodeId && t.DeviceId == deviceId && t.TagId == tagId) ||
               _intMetrics.Exists(t => t.EonNodeId == nodeId && t.DeviceId == deviceId && t.TagId == tagId) ||
               _floatMetrics.Exists(t => t.EonNodeId == nodeId && t.DeviceId == deviceId && t.TagId == tagId);
    }

    public List<TagValue<string>> GetAllTags()
    {
        var tags = new List<TagValue<string>>();
        foreach(var boolMetric in _boolMetrics)
        {           
            var newTag = new TagValue<string>(boolMetric.EonNodeId, boolMetric.DeviceId, boolMetric.TagId, Convert.ToString(boolMetric.Value));
            tags.Add(newTag);
        }
        foreach (var intMetric in _intMetrics)
        {
            var newTag = new TagValue<string>(intMetric.EonNodeId, intMetric.DeviceId, intMetric.TagId, Convert.ToString(intMetric.Value));
            tags.Add(newTag);
        }
        foreach (var floatMetric in _floatMetrics)
        {
            var newTag = new TagValue<string>(floatMetric.EonNodeId, floatMetric.DeviceId, floatMetric.TagId, Convert.ToString(floatMetric.Value));
            tags.Add(newTag);
        }
        return tags;
    }

    private TagValue<bool> GetBoolTagValue(string nodeId, string deviceId, string tagId)
    {
        return _boolMetrics.First(t => t.EonNodeId == nodeId && t.DeviceId == deviceId && t.TagId == tagId);
    }

    private TagValue<int> GetIntTagValue(string nodeId, string deviceId, string tagId)
    {
        return _intMetrics.First(t => t.EonNodeId == nodeId && t.DeviceId == deviceId && t.TagId == tagId);
    }

    private TagValue<float> GetFloatTagValue(string nodeId, string deviceId, string tagId)
    {
        return _floatMetrics.First(t => t.EonNodeId == nodeId && t.DeviceId == deviceId && t.TagId == tagId);
    }
}