using Dapper;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;

namespace PharmacyApp.Data.Repository
{
    public class NotificationsRepository : GenericRepository<Notifications>, INotificationsRepository
    {
        public NotificationsRepository(IBaseUnitOfWork unitofWork) : base(unitofWork)
        {
        }

        //public async Task<IEnumerable<NotificationModel>> GetByPaging(int UserId, int PageIndex, int PageSize)
        //{
        //    int Skiprow = (PageIndex - 1) * PageSize;
        //    string strQuery = @"exec GetNotification @UserId, @Skiprow, @Pagesize ";
        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("UserId", UserId);
        //    parameters.Add("Skiprow", Skiprow);
        //    parameters.Add("Pagesize", PageSize);

        //    return (await _DbConnection.QueryAsync<NotificationModel>(strQuery, parameters, _DbTransaction)).ToList();
        //}
        public async Task<int> MarkAllAsRead(IEnumerable<long> notificationid)
        {
            var ids = string.Join(",", notificationid);
            string strQuery = @"Update [Notifications] set IsRead=1 where NotificationId in (" + ids + ")";

            DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("NotificationId", ids);

            return (await _DbConnection.ExecuteAsync(strQuery, parameters, _DbTransaction)
);
        }
    }
}
