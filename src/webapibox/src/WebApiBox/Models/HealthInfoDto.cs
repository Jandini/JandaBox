namespace WebApiBox.Models;

public record HealthInfoDto
{
    public ServiceInfoDto? Service { get; set; }
}
