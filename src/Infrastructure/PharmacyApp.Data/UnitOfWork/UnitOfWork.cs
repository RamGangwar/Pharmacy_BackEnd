using Microsoft.Extensions.Configuration;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Data.Repository;

namespace PharmacyApp.Data.UnitOfWork
{
    public class UnitofWork : BaseUnitOfWork, IUnitofWork
    {

        public UnitofWork(IConfiguration configuration) : base(configuration)
        {

        }

        public IUserRefreshTokenRepository UserRefreshToken => new UserRefreshTokenRepository(this);
        public INotificationsRepository Notifications => new NotificationsRepository(this);
        public IAccessPermissionRepository AccessPermission => new AccessPermissionRepository(this);
        public IModuleRepository Modules => new ModulesRepository(this);
        public IDiscountsRepository Discounts => new DiscountsRepository(this);
        public IInventoryRepository Inventory => new InventoryRepository(this);
        public IMedicinesRepository Medicines => new MedicinesRepository(this);
        public IPatientsRepository Patients => new PatientsRepository(this);
        public IPaymentsRepository Payments => new PaymentsRepository(this);
        public IPrescriptionsRepository Prescriptions => new PrescriptionsRepository(this);
        public IPurchaseDetailRepository PurchaseDetail => new PurchaseDetailRepository(this);
        public IPurchaseOrderRepository PurchaseOrder => new PurchaseOrderRepository(this);
        public IRolesRepository Roles => new RolesRepository(this);
        public ISalesDetailRepository SalesDetail => new SalesDetailRepository(this);
        public ISalesHeaderRepository SalesHeader => new SalesHeaderRepository(this);
        public ISuppliersRepository Suppliers => new SuppliersRepository(this);       
        public IUsersRepository Users => new UsersRepository(this);
    }
}
