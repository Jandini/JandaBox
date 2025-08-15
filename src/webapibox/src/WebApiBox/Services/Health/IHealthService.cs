namespace WebApiBox.Services.Health;

public interface IHealthService
{
    Task<HealthInfo> GetHealthInfoAsync();
}