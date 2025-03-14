using Microsoft.AspNetCore.Mvc;
using ActorRepositoryLibrary;
using ActorsREST.Records;
using System.Data.Common;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActorsREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private ActorsRepositoryList actorsRepository;

        public ActorsController(ActorsRepositoryList actorsRepository)
        {
            this.actorsRepository = actorsRepository;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // GET: api/<ActorsController>
        [HttpGet]
        public ActionResult<IEnumerable<Actor>> Get()
        {
            IEnumerable<Actor> result = actorsRepository.GetActors();
            if(result.Count() > 0)
            {
                return Ok(result);
            }
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET api/<ActorsController>/5
        [HttpGet("{id}")]
        public ActionResult<Actor> Get(int id)
        {
            Actor actor = actorsRepository.GetActorById(id);
            if (actor != null)
            {
                return Ok(actor);
            }
            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST api/<ActorsController>
        [HttpPost]
        public ActionResult<Actor> Post([FromBody]ActorRecord newActorRecord)
        {
            try
            {
                Actor convertedActor = RecordHelper.ConvertActorRecord(newActorRecord);
                Actor createdActor = actorsRepository.Add(convertedActor);
                return Created("/" + createdActor.Id, createdActor);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Dit objekt indeholder null!: " + ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest("Dit objekt er ude af en range: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Dit objekt er ikke gyldigt: " + ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT api/<ActorsController>/5
        [HttpPut("{id}")]
        public ActionResult<Actor> Put(int id, [FromBody]ActorRecord actorToUpdate)
        {
            try
            {
                Actor convertedActor = RecordHelper.ConvertActorRecord(actorToUpdate);
                Actor updatedActor = actorsRepository.Update(id, convertedActor);

                if (updatedActor != null)
                {
                    return Ok(updatedActor);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Dit objekt indeholder null!: " + ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest("Dit objekt er ude af en range: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Dit objekt er ikke gyldigt: " + ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE api/<ActorsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Actor> Delete(int id)
        {
            Actor deletedActor = actorsRepository.Delete(id);
            if (deletedActor != null)
            {
                return Ok(deletedActor);
            }
            return NotFound();
        }
    }
}
