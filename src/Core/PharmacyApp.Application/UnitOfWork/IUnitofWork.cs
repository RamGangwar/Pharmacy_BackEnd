using PharmacyApp.Application.Repository;

namespace PharmacyApp.Application.UnitOfWork
{
    public interface IUnitofWork : IBaseUnitOfWork
    {
        IUserRefreshTokenRepository UserRefreshToken { get; }
        INotificationsRepository Notifications { get; }
        IAccessPermissionRepository AccessPermission { get; }
        IModuleRepository Modules { get; }
        IDiscountsRepository Discounts { get; }
        IInventoryRepository Inventory { get; }
        IMedicinesRepository Medicines { get; }
        IPatientsRepository Patients { get; }
        IPaymentsRepository Payments { get; }
        IPrescriptionsRepository Prescriptions { get; }
        IPurchaseDetailRepository PurchaseDetail { get; }
        IPurchaseOrderRepository PurchaseOrder { get; }
        IRolesRepository Roles { get; }
        ISalesDetailRepository SalesDetail { get; }
        ISalesHeaderRepository SalesHeader { get; }
        ISuppliersRepository Suppliers { get; }
        IUsersRepository Users { get; }

    }
}
