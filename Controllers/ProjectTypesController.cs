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
    public class ProjectTypesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProjectTypesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        private bool ProjectTypeExists(int id)
        {
            return (_dataContext.ProjectTypes?.Any(projectType => projectType.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Retrieves all ProjectTypes
        /// </summary>
        //GET api/ProjectTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectType>>> GetProjectTypes()
        {
            if (_dataContext.ProjectTypes == null) return NotFound();
            return await _dataContext.ProjectTypes.ToListAsync();
        }


        /// <summary>
        /// Retrieves a ProjectType by Id
        /// </summary>
        //GET api/ProjectTypes/{id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProjectType>> GetProjectType(int Id)
        {
            if (_dataContext.ProjectTypes == null) return NotFound();

            var projectType = await _dataContext.ProjectTypes.FindAsync(Id);
            if (projectType == null) return NotFound();
            return projectType;
        }

        /// <summary>
        /// Creates a new ProjectType
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        ///        "name": "BackEnd project"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created ProjectType</response>
        /// <response code="400">If the ProjectType is null</response>
        //POST api/ProjectTypes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<ProjectType>> AddProjectType(ProjectType projectType)
        {
            _dataContext.ProjectTypes.Add(projectType);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProjectType), new { id = projectType.Id }, projectType);
        }

        ///  <summary>
        /// Updates a specific ProjectType.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        ///        "id": 1,
        ///        "name": "BackEnd project"
        ///     }
        /// </remarks>
        //PUT api/ProjectTypes/{id}
        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<ProjectType>> UpdateProjectType(int Id, ProjectType projectType)
        {
            if (Id != projectType.Id) return BadRequest();

            _dataContext.Update(projectType);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ProjectTypeExists(Id)) return NotFound();
                else throw;
            }

            return NoContent();
        }


        /// <summary>
        /// Deletes a specific ProjectType.
        /// </summary>
        //DELETE api/ProjectTypes/{id}
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<ProjectType>> DeleteProjectType(int Id)
        {
            if (_dataContext.ProjectTypes == null) return NotFound();

            var projectType = await _dataContext.ProjectTypes.FindAsync(Id);
            if (projectType == null) return NotFound();

            _dataContext.ProjectTypes.Remove(projectType);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}