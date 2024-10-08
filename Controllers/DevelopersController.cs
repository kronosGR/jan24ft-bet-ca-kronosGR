using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using jan24ft_bet_ca_kronosGR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace jan24ft_bet_ca_kronosGR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DevelopersController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public DevelopersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Retrieves all developers
        /// </summary>
        //GET api/Developers/{id}
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Developer>>> GetDevelopers()
        {
            if (_dataContext.Developers == null)
            {
                return NotFound();
            }

            return await _dataContext.Developers.ToListAsync();
        }

        /// <summary>
        /// Retrieves a developer by Id
        /// </summary>
        //GET api/Developers/{id}
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<Developer>> GetDeveloper(int Id)
        {
            if (_dataContext.Developers == null)
            {
                return NotFound();
            }
            var developer = await _dataContext.Developers.FindAsync(Id);
            if (developer == null) return NotFound();
            return developer;
        }

        /// <summary>
        /// Creates a new Developer
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        ///        "id": 1,
        ///        "firstname": "Kronos",
        ///        "lastname": "GR",
        ///        "roleId": 1,
        ///        "teamId": 1
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created developer</response>
        /// <response code="400">If the developer is null</response>
        //POST api/Developers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<Developer>> AddDeveloper(Developer developer)
        {
            _dataContext.Developers.Add(developer);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDeveloper), new { id = developer.Id }, developer);
        }

        ///
        /// Updates a specific Developer.
        ///
        //PUT api/Developers/{id}
        [HttpPost("{Id:int}")]
        [Authorize]
        public async Task<ActionResult<Developer>> UpdateDeveloper(int Id, Developer developer)
        {
            if (Id != developer.Id)
            {
                return BadRequest();
            }

            _dataContext.Update(developer);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (DeveloperExists(Id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        ///
        /// Deletes a specific Developer.
        ///
        //DELETE api/Developers/{id}
        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<ActionResult<Developer>> DeleteDeveloper(int Id)
        {
            if (_dataContext.Developers == null) return NotFound();

            var developer = await _dataContext.Developers.FindAsync(Id);
            if (developer == null) return NotFound();

            _dataContext.Developers.Remove(developer);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

        private bool DeveloperExists(int id)
        {
            return (_dataContext.Developers?.Any(developer => developer.Id == id)).GetValueOrDefault();
        }
    }
}