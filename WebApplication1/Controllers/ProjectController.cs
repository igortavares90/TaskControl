using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskControl.Application.Services;
using TaskControl.Domain.Commands.Project;
using TaskControl.Domain.Commands.Task;
using TaskControl.Domain.Interfaces.Service;

namespace TaskControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public ProjectController(IProjectService projectService, IUserService userService)
        {
            _projectService = projectService;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetUserProjects")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectCommandQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectCommandQueryResult>> GetUserProjects([FromQuery] GetProjectCommand projectCommand)
        {
            try
            {
                var result = _projectService.GetUserProjects(projectCommand);

                if (result.Count > 0)
                    return new OkObjectResult(result);
                else
                    return new NotFoundObjectResult("Nenhum projeto encontrado para o usuário informado!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateProject")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectCommandQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProject([FromQuery] CreateProjectCommand projectCommand)
        {
            try
            {
                if (!_userService.UserExists(projectCommand.UserId))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Id de usuário inválido!");
                }

                _projectService.CreateProject(projectCommand);

                return new OkObjectResult("Projeto criado com sucesso!");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteProject")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectCommandQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProject([FromQuery] DeleteProjectCommand taskCommand)
        {
            try
            {
                var result = _projectService.DeleteProject(taskCommand);

                if (result != string.Empty)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                return new OkObjectResult("Projeto excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
