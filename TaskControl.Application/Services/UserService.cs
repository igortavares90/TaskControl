using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskControl.Domain.Interfaces.Repository;
using TaskControl.Domain.Interfaces.Service;

namespace TaskControl.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool UserExists(int userId)
        { 
            return _userRepository.UserExists(userId);
        }
    }
}
