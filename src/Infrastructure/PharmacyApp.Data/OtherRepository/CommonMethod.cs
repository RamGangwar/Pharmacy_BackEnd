using PharmacyApp.Application.OtherRepository;

namespace PharmacyApp.Data.OtherRepository
{
    public class CommonMethod : ICommonMethod
    {
        public string GetLikeTypeName(int type)
        {
            return "";// ((LikeType)type).ToString();
           
        }
    }
}
