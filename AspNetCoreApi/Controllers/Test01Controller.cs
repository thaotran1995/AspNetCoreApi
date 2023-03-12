using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Test01Controller : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        public Test01Controller(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region test 01
        private static readonly string[] names = new string[]
        {
            "Hanh trinh ve phuong dong",
            "Hoa sen tren tuyet",
            "Nha gia kim",
            "Duong may qua xu tuyet"
        };

        [HttpGet(Name = "GetTest01Result")]
        [Route("/GetTest01")]
        public IEnumerable<Test01> Get()
        {
            _logger.LogInfo("Here is info message from the controller.");
            _logger.LogDebug("Here is debug message from the controller.");
            _logger.LogWarning("Here is warn message from the controller.");
            _logger.LogError("Here is error message from the controller.");

            return Enumerable.Range(1, 5).Select(index => new Test01
            {
                Id = index,
                Date = DateTime.Now.AddDays(index),
                Name = names[Random.Shared.Next(names.Length)]
            })
            .ToArray();
        }
        #endregion

        [HttpGet(Name ="GetAllOwners")]
        [Route("/GetAllOwners")]
        public IEnumerable<Owner> GetOwners()
        {
            var owners = _repository.Owner.FindAll().ToList();
            return owners;
        }
    }
}
