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
    public class DevelopersController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public DevelopersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

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

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Developer>> AddDeveloper(Developer developer)
        {
            _dataContext.Developers.Add(developer);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDeveloper), new { id = developer.Id }, developer);
        }

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