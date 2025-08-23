using PharmacyApp.Application.Queries.Userss;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IUsersRepository : IGenericRepository<Users>
    {
        Task<IEnumerable<UsersVM>> GetByPaging(GetUsersByFilterQuery filterQuery);
    }
}

