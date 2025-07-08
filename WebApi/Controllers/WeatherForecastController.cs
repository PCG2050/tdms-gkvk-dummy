using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<User> Get()
        {
            return _context.Users
                .Include(u=> u.Organization)
                .ToList();
        }
        [HttpGet("orgs",Name = "GetWeatherForecast2")]
        public IEnumerable<Organization> Get2()
        {
            var orgs =  _context.Organizations.ToList();
            var orgsWithUsers = _context.Organizations.Include(o => o.Users).AsNoTracking().ToList();
            return orgsWithUsers;
        }
    }
}
