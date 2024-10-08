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
    public class RoleController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public RoleController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        private bool RoleExists(int id)
        {
            return (_dataContext.Roles?.Any(role => role.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Retrieves all Roles
        /// </summary>
        //GET api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            if (_dataContext.Roles == null) return NotFound();
            return await _dataContext.Roles.ToListAsync();
        }


        /// <summary>
        /// Retrieves a Role by Id
        /// </summary>
        //GET api/Roles/{id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<Role>> GetRole(int Id)
        {
            if (_dataContext.Roles == null) return NotFound();

            var role = await _dataContext.Roles.FindAsync(Id);
            if (role == null) return NotFound();
            return role;
        }

        /// <summary>
        /// Creates a new Role
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        ///        "name": "Fullstack developer"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created Role</response>
        /// <response code="400">If the Role is null</response>
        //POST api/Roles
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<Role>> AddRole(Role role)
        {
            _dataContext.Roles.Add(role);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
        }

        ///  <summary>
        /// Updates a specific Role.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        ///        "id": 1,
        ///        "name": "Fullstack developer"
        ///     }
        /// </remarks>
        //PUT api/Roles/{id}
        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<Role>> UpdateRole(int Id, Role role)
        {
            if (Id != role.Id) return BadRequest();

            _dataContext.Update(role);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (RoleExists(Id)) return NotFound();
                else throw;
            }

            return NoContent();
        }


        /// <summary>
        /// Deletes a specific Role.
        /// </summary>
        //DELETE api/Roles/{id}
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<Role>> DeleteRole(int Id)
        {
            if (_dataContext.Roles == null) return NotFound();

            var role = await _dataContext.Roles.FindAsync(Id);
            if (role == null) return NotFound();

            _dataContext.Roles.Remove(role);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}