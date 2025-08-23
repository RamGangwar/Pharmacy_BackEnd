using MediatR;
using PharmacyApp.Domain.Model; 
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.UserRefreshTokens    
{        
public class GetUserRefreshTokenByIdQuery : IRequest<ResponseModel<UserRefreshTokenVM>>     
{public int UserRefreshTokenId {get; set;}
}
}
