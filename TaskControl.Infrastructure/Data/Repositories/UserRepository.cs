using Dapper;
using System.Data;
using TaskControl.Domain.Interfaces.Database;
using TaskControl.Domain.Interfaces.Repository;

namespace TaskControl.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool UserExists(int userId)
        {
            var Query = $@"SELECT 1 FROM TaskControlUser WHERE Id=@UserId";

            var Entity = _unitOfWork.Connection.Query(Query, new { UserId = userId }, commandType: CommandType.Text);

            if (Entity.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
