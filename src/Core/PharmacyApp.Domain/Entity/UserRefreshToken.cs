using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("UserRefreshToken")]
    public class UserRefreshToken 
    {
        [Key] 
 public int UserRefreshTokenId {get; set;}
public string AccessToken {get; set;}
public string RefreshToken {get; set;}
public string IpAddress {get; set;}
public bool IsInvalidated {get; set;}
public int UserId {get; set;}
public DateTime CreatedDate {get; set;}
public DateTime ExpiredDate {get; set;}
}
}
