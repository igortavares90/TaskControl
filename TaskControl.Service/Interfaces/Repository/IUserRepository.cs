namespace TaskControl.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        bool UserExists(int userId);
    }
}
