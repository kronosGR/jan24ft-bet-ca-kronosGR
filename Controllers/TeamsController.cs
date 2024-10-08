using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jan24ft_bet_ca_kronosGR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jan24ft_bet_ca_kronosGR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TeamsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TeamsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        private bool TeamExists(int id)
        {
            return (_dataContext.Roles?.Any(role => role.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Retrieves all Teams
        /// </summary>
        //GET api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            if (_dataContext.Roles == null) return NotFound();
            return await _dataContext.Teams.ToListAsync();
        }

        /// <summary>
        /// Retrieves a Team by Id
        /// </summary>
        //GET api/Teams/{id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<Team>> GetTeam(int Id)
        {
            if (_dataContext.Teams == null) return NotFound();

            var team = await _dataContext.Teams.FindAsync(Id);
            if (team == null) return NotFound();
            return team;
        }

        /// <summary>
        /// Creates a new Team
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        ///        "name": "Kronos Team"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created Team</response>
        /// <response code="400">If the Team is null</response>
        //POST api/Teams
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<Team>> AddRTeam(Team team)
        {
            _dataContext.Teams.Add(team);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
        }

        ///  <summary>
        /// Updates a specific Team.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        ///        "id": 1,
        ///        "name": "Kronos2 Team"
        ///     }
        /// </remarks>
        //PUT api/Teams/{id}
        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<Team>> UpdateTeam(int Id, Team team)
        {
            if (Id != team.Id) return BadRequest();

            _dataContext.Update(team);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (TeamExists(Id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific Team.
        /// </summary>
        //DELETE api/Teams/{id}
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<Team>> DeleteTeam(int Id)
        {
            if (_dataContext.Teams == null) return NotFound();

            var team = await _dataContext.Teams.FindAsync(Id);
            if (team == null) return NotFound();

            _dataContext.Teams.Remove(team);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

    }
}