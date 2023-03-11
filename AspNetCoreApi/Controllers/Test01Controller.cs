using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Test01Controller : ControllerBase
    {
        private static readonly string[] names = new string[]
        {
            "Hanh trinh ve phuong dong",
            "Hoa sen tren tuyet",
            "Nha gia kim",
            "Duong may qua xu tuyet"
        };

        private readonly ILoggerManager _logger;

        public Test01Controller(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTest01Result")]
        public IEnumerable<Test01> Get()
        {
            _logger.LogInfo("Here is info message from the controller.");
            _logger.LogDebug("Here is debug message from the controller.");
            _logger.LogWarning("Here is warn message from the controller.");
            _logger.LogError("Here is error message from the controller.");

            return Enumerable.Range(1, 5).Select(index => new Test01
            {
                Id =  index,
                Date = DateTime.Now.AddDays(index),
                Name = names[Random.Shared.Next(names.Length)]
            })
            .ToArray();
        }
    }
}
