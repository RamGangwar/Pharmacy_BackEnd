using Dapper;
using PharmacyApp.Application.Queries.Mediciness;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class MedicinesRepository : GenericRepository<Medicines>, IMedicinesRepository
    {
        public MedicinesRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<MedicinesVM>> GetByPaging(GetMedicinesByFilterQuery filterQuery)
        {
            
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, M.* , S.SupplierName from Medicines M Inner join Suppliers S on M.SupplierId=S.SupplierId where 1=1 ");
            if (!string.IsNullOrEmpty(filterQuery.MedicineName))
            {
                sb.Append(" And MedicineName like @MedicineName");
                parameters.Add("MedicineName", "%" + filterQuery.MedicineName + "%");
            }
            if (!string.IsNullOrEmpty(filterQuery.GenericName))
            {
                sb.Append(" And GenericName like @GenericName");
                parameters.Add("GenericName", "%" + filterQuery.GenericName + "%");
            }
            if (!string.IsNullOrEmpty(filterQuery.Manufacturer))
            {
                sb.Append(" And Manufacturer like @Manufacturer");
                parameters.Add("Manufacturer", "%" + filterQuery.Manufacturer + "%");
            }
            if (!string.IsNullOrEmpty(filterQuery.Category))
            {
                sb.Append(" And Category like @Category");
                parameters.Add("Category", "%" + filterQuery.Category + "%");
            }
            if (filterQuery.Price>0)
            {
                sb.Append(" And Price = @Price");
                parameters.Add("Price", filterQuery.Price);
            }
           
            if (filterQuery.SupplierId > 0)
            {
                sb.Append(" And M.SupplierId = @SupplierId");
                parameters.Add("SupplierId", filterQuery.SupplierId);
            }
            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return (await _DbConnection.QueryAsync<MedicinesVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

