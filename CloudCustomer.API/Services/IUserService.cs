using CloudCustomer.API.Models;

namespace CloudCustomer.API.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
}