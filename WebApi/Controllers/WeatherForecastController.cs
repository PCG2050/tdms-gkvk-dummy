using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly TdmsDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, TdmsDbContext context)
        {
            _logger = logger;
            _context = context;
        }
    }
}
