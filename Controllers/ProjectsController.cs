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
    public class ProjectsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProjectsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        private bool ProjectExists(int id)
        {
            return (_dataContext.Projects?.Any(project => project.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Retrieves all Projects
        /// </summary>
        //GET api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            if (_dataContext.Projects == null) return NotFound();
            return await _dataContext.Projects.ToListAsync();
        }


        /// <summary>
        /// Retrieves a Project by Id
        /// </summary>
        //GET api/Projects/{id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<Project>> GetProject(int Id)
        {
            if (_dataContext.Projects == null) return NotFound();

            var project = await _dataContext.Projects.FindAsync(Id);
            if (project == null) return NotFound();
            return project;
        }

        /// <summary>
        /// Creates a new Project
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        ///        "name": "Project1",
        ///        "projecttypeid":"1",
        ///        "teamid":"1"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created Project</response>
        /// <response code="400">If the Project is null</response>
        //POST api/Projects
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<Project>> AddProject(Project project)
        {
            _dataContext.Projects.Add(project);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        ///  <summary>
        /// Updates a specific Project.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        ///        "id": 1,
        ///        "name": "Project1",
        ///        "projecttypeid":"1",
        ///        "teamid":"1"
        ///     }
        /// </remarks>
        //PUT api/Projects/{id}
        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<Project>> UpdateProject(int Id, Project project)
        {
            if (Id != project.Id) return BadRequest();

            _dataContext.Update(project);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ProjectExists(Id)) return NotFound();
                else throw;
            }

            return NoContent();
        }


        /// <summary>
        /// Deletes a specific Project.
        /// </summary>
        //DELETE api/Projects/{id}
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<Project>> DeleteProject(int Id)
        {
            if (_dataContext.Projects == null) return NotFound();

            var project = await _dataContext.Projects.FindAsync(Id);
            if (project == null) return NotFound();

            _dataContext.Projects.Remove(project);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}