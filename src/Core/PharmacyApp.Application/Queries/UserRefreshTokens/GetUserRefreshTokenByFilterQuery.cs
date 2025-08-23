using System.ComponentModel.DataAnnotations;
using MediatR;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.UserRefreshTokens { public class GetUserRefreshTokenByFilterQuery : PagingRquestModel, IRequest<PagingModel<UserRefreshTokenVM>> {public int UserRefreshTokenId {get; set;}
public string AccessToken {get; set;}
public string RefreshToken {get; set;}
public string IpAddress {get; set;}
public bool IsInvalidated {get; set;}
public int UserId {get; set;}
public DateTime CreatedDate {get; set;}
public DateTime ExpiredDate {get; set;}
}
}
