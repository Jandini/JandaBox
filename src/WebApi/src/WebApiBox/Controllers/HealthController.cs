using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiBox.Models;
using WebApiBox.Services;

namespace WebApiBox.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly IHealthService _healthService;
        private readonly IMapper _mapper;

        public HealthController(ILogger<HealthController> logger, IHealthService healthService, IMapper mapper)
        {
            _logger = logger;
            _healthService = healthService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HealthInfoDto>> GetHealthInfoAsync()
        {
            try
            {
                _logger.LogDebug("Getting health info");
                var healthInfo = await _healthService.GetHealthInfoAsync();
                return Ok(_mapper.Map<HealthInfoDto>(healthInfo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}