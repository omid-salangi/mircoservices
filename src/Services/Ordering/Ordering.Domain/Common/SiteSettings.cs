namespace Ordering.Domain.Common;

public class SiteSettings
{
    public EventBusSettings EventBusSettings { get; set; }
}

public class EventBusSettings
{
    public string HostAddress { get; set; }
}