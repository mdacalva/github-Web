using Crypto.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crypto.Api.Services
{
    public interface IUserService
    {
        // Methods implemented for testing purposes
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }
}
