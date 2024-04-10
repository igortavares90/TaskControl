using Microsoft.AspNetCore.Mvc;
using TaskControl.Domain.Commands.Project;
using TaskControl.Domain.Commands.Task;
using TaskControl.Domain.Interfaces.Service;

namespace TaskControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("GetProjectTasks")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectCommandQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectCommandQueryResult>> GetProjectTasks([FromQuery] GetProjectTaskCommand getProjectTaskCommand)
        {
            try
            {
                var result = _taskService.GetTasksByProjectId(getProjectTaskCommand);

                if (result.Count > 0)
                    return new OkObjectResult(result);
                else
                    return new NotFoundObjectResult("Nenhuma tarefa encontrada para o projeto informado!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateProjectTask")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectCommandQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProjectTask([FromQuery] CreateTaskCommand taskCommand)
        {
            try
            {
                var result = _taskService.CreateProjectTask(taskCommand);

                if (result != string.Empty)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                return new OkObjectResult("Tarefa criada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateProjectTask")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectCommandQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProjectTask([FromQuery] UpdateProjectTaskCommand taskCommand)
        {
            try
            {
                if (!_taskService.TaskExists(taskCommand.TaskId))
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Id de tarefa inexistente!");
                }

                _taskService.UpdateProjectTask(taskCommand);

                return new OkObjectResult("Tarefa atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteProjectTask")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectCommandQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProjectTask([FromQuery] DeleteProjectTaskCommand taskCommand)
        {
            try
            {
                if (!_taskService.TaskExists(taskCommand.TaskId))
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Id de tarefa inexistente!");
                }

                _taskService.DeleteProjectTask(taskCommand);

                return new OkObjectResult("Tarefa excluída com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("PerformanceReport")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectCommandQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectCommandQueryResult>> PerformanceReport()
        {
            try
            {
                var result = _taskService.GetPerformanceReport();

                if (result.Count > 0)
                    return new OkObjectResult(result);
                else
                    return new NotFoundObjectResult("Dados insuficientes para geração do relatório!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
