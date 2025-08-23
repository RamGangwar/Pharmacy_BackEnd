using Dapper;
using PharmacyApp.Application.Queries.UserRefreshTokens;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class UserRefreshTokenRepository : GenericRepository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<UserRefreshToken> GetByToken(string Token, string iPAddress)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select * from UserRefreshToken where AccessToken=@Token and IpAddress=@iPAddress and IsInvalidated=0");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Token", Token);
            parameters.Add("iPAddress", iPAddress);
            return (await _DbConnection.QueryAsync<UserRefreshToken>(sb.ToString(), parameters, _DbTransaction)).FirstOrDefault();
        }

        public async Task<int> UpdateInValidateToken(int UserId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Update UserRefreshToken Set IsInvalidated=1 where UserId=@UserId");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserId", UserId);
            return (await _DbConnection.ExecuteAsync(sb.ToString(), parameters, _DbTransaction));
        }
        public async Task<IEnumerable<UserRefreshTokenVM>> GetByPaging(GetUserRefreshTokenByFilterQuery filterQuery)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, *from UserRefreshToken  ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY "); 
           DynamicParameters parameters = new DynamicParameters();
           parameters.Add("SkipRow", filterQuery.SkipRow);
           parameters.Add("PageSize", filterQuery.PageSize);
           return (await _DbConnection.QueryAsync<UserRefreshTokenVM>(sb.ToString(), parameters, _DbTransaction));
        }
        public async Task<UserRefreshToken> GetDetail(string RefreshToken, string IpAddress)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select * from UserRefreshToken where IsInvalidated=0  IpAddress=@IpAddress and RefreshToken=@RefreshToken ");//and  AccessToken=@ExpiredToken and
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("IpAddress", IpAddress);
            //parameters.Add("ExpiredToken", ExpiredToken);
            parameters.Add("RefreshToken", RefreshToken);
            return (await _DbConnection.QueryAsync<UserRefreshToken>(sb.ToString(), parameters, _DbTransaction)).FirstOrDefault();
        }
    }
}

