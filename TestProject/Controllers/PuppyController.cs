using TestProject.Services;
using TestProject.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PuppyController : ControllerBase
    {
        private PuppyService _ps;

        public PuppyController(PuppyService ps)
        {
            _ps = ps;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Puppy>> GetAllPuppies()
        {
            try
            {
                return Ok(_ps.GetAllPuppies());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{puppyID}")]
        public ActionResult<Puppy> GetByID(int puppyID)
        {
            try
            {
                return Ok(_ps.GetById(puppyID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Puppy> CreatePuppy(Puppy newPuppy)
        {
            try
            {
                return Ok(_ps.CreatePuppy(newPuppy));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{newID}")]
        public ActionResult<Puppy> EditPuppy(Puppy puppyToUpdate, int newId)
        {
            try
            {
                return Ok(_ps.EditPuppy(puppyToUpdate, newId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{puppyId}")]
        public ActionResult<Puppy> DeletePuppy(int puppyId)
        {
            try
            {
                return Ok(_ps.DeletePuppy(puppyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
