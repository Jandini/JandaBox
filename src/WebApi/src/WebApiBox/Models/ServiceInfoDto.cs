namespace WebApiBox.Models;

public class ServiceInfoDto
{
    public string? Name { get; set; }
    public string? Version { get; set; }
    public DateTime StartedOn { get; set; }
    public TimeSpan UpTime { get; set; }
}
