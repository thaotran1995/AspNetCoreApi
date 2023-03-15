using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApi.Controllers
{
    [Route("api/owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllOwners()
        {
            try
            {
                var owners = _repository.Owner.GetAllOwners();
                _logger.LogInfo($"Return {owners.Count()} owners from database. ");
                var ownerResult = _mapper.Map<IEnumerable<OwnerDTO>>(owners);
                return Ok(ownerResult);
            }catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOwnerById(Guid id) 
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);
                if(owner is null)
                {
                    _logger.LogError($"Owner wwith id: {id} not be found in the DB");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"return Owner with Id: {id}");
                    var result = _mapper.Map<OwnerDTO>(owner);
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message} ");
                return StatusCode(500, "internal server error");
            }   
        }
    }
}
