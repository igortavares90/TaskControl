using Dapper;
using System.Data;
using TaskControl.Domain.Commands.Project;
using TaskControl.Domain.Enum;
using TaskControl.Domain.Interfaces.Database;
using TaskControl.Domain.Interfaces.Repository;

namespace TaskControl.Infrastructure.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ProjectCommandQueryResult> GetUserProjects(GetProjectCommand command)
        {
            var Query = $@"SELECT Pr.Id, Pr.ProjectName, Us.UserName, Ps.Description as ProjectStatus
                           FROM Project Pr INNER JOIN TaskControlUser Us ON Us.Id = Pr.UserId
                           INNER JOIN ProjectStatus Ps ON Ps.Id = Pr.ProjectStatusId
                           WHERE Pr.UserId=@UserId";

            var Entity = _unitOfWork.Connection.Query<ProjectCommandQueryResult>(Query, new { UserId = command.UserId }, commandType: CommandType.Text);

            return Entity.ToList<ProjectCommandQueryResult>();

        }

        public bool CreateProject(CreateProjectCommand command)
        {
            var Query = $@"INSERT INTO Project(ProjectName, UserId, ProjectStatusId)
                           VALUES (@ProjectName, @UserId, @ProjectStatusId)";

            _unitOfWork.Connection.Query(Query,
                new { ProjectName = command.ProjectName, UserId = command.UserId, ProjectStatusId = ProjectStatusEnum.Active },
                commandType: CommandType.Text);

            return true;
        }

        public bool DeleteProject(DeleteProjectCommand command)
        {
            var Query = $@"DELETE FROM Project WHERE Id=@ProjectId";

            _unitOfWork.Connection.Query(Query,
                new { ProjectId = command.ProjectId },
                commandType: CommandType.Text);

            return true;
        }


    }
}
