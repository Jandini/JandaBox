namespace WebApiBox.Services
{
    public interface IHealthService
    {
        Task<HealthInfo> GetHealthInfoAsync();
    }
}