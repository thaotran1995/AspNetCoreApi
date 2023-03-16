using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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

        [HttpGet("{id}", Name ="OwnerById")]
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

        [HttpGet("{id}/account")]
        public IActionResult GetOwnerWithDetails(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerWithDetails(id);
                if(owner is null)
                {
                    _logger.LogError($"Owner with id: {id} is not be found");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with id: {id}");
                    var result = _mapper.Map<OwnerDTO> (owner);   
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerWithDetails action: {ex.Message}");
                return StatusCode(500, "Interal server error");
                
            }
        }

        [HttpPost]
        public IActionResult CreateOwner([FromBody] OwnerForCreationDTO owner)
        {
            try
            {
                if(owner is null)
                {
                    _logger.LogError($"Owner object is null");
                    return BadRequest("Owner object is null");
                }
                if(!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");

                }
                var ownerEntity = _mapper.Map<Owner>(owner);
                _repository.Owner.Create(ownerEntity);
                _repository.Save();

                var createdOwner = _mapper.Map<OwnerDTO>(ownerEntity);
                return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody] OwnerForUpdateDTO owner)
        {
            try
            {
                if(owner is null)
                {
                    _logger.LogError($"Owner Object is null");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Invalid owner object sent from client");
                    return BadRequest("Invalid model object");
                }
                var ownerEntity = _repository.Owner.GetOwnerById(id);
                if(ownerEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db");
                    return NotFound();
                }
                _mapper.Map(owner, ownerEntity);
                _repository.Owner.Update(ownerEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside Update action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);
                if(owner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db");
                    return NotFound();
                }
                if (_repository.Account.AccountsByOwner(id).Any())
                {
                    _logger.LogError($"Can't delete owner with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Can't delete owner. It has related accounts. Delete those accounts first");
                }
                _repository.Owner.Delete(owner);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
