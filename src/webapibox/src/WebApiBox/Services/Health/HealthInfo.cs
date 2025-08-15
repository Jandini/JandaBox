namespace WebApiBox.Services.Health;

public record HealthInfo
{
    public ServiceInfo? Service { get; set; }
}
