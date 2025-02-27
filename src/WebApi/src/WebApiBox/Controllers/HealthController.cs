using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiBox.Models;
using WebApiBox.Services;

namespace WebApiBox.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController(ILogger<HealthController> logger, IHealthService healthService, IMapper mapper) : ControllerBase
{
    [HttpGet(Name = "GetHealthInfo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HealthInfoDto>> GetHealthInfoAsync()
    {
#if (exceptionMiddleware)
        logger.LogDebug("Getting health info");
        var healthInfo = await healthService.GetHealthInfoAsync();
        return Ok(mapper.Map<HealthInfoDto>(healthInfo));
#else
        try
        {
            logger.LogDebug("Getting health info");
            var healthInfo = await healthService.GetHealthInfoAsync();
            return Ok(mapper.Map<HealthInfoDto>(healthInfo));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
#endif
    }
}