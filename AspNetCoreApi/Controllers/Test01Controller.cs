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

        private readonly ILogger<Test01Controller> _logger;

        public Test01Controller(ILogger<Test01Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTest01Result")]
        public IEnumerable<Test01> Get()
        {
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
