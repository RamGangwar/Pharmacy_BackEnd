using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Repository
{
    public interface IUserRepository : IGenericRepository<Users>
    {
        Task<Users> GetDuplicate(int UserId, string Phone, string Email);
        Task<Users> GetByEmail(string Email);
        Task<Users> GetByPhone(string Phone);
        Task<Users> GetDuplicateByUserName(int UserId, string UserName);
        Task<Users> GetByUserName(string UserName);
    }
}
