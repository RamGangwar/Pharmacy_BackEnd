using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Repository
{
    public interface INotificationsRepository : IGenericRepository<Notifications>
    {
        //Task<IEnumerable<NotificationModel>> GetByPaging(int UserId, int PageIndex, int PageSize);
        Task<int> MarkAllAsRead(IEnumerable<long> UserId);
    }
}
