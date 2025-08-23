using PharmacyApp.Application.Queries.UserRefreshTokens;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Repository
{
    public interface IUserRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
        Task<IEnumerable<UserRefreshTokenVM>> GetByPaging(GetUserRefreshTokenByFilterQuery filterQuery);
        Task<UserRefreshToken> GetByToken(string Token, string iPAddress);
        Task<int> UpdateInValidateToken(int UserId);
        Task<UserRefreshToken> GetDetail(string RefreshToken, string IpAddress);
    }
}

