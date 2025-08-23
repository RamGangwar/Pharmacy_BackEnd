using Dapper;
using PharmacyApp.Application.Queries.SalesHeaders;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class SalesHeaderRepository : GenericRepository<SalesHeader>, ISalesHeaderRepository
    {
        public SalesHeaderRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<SalesHeaderVM>> GetByPaging(GetSalesHeaderByFilterQuery filterQuery)
        {

            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            var orderDict = new Dictionary<int, SalesHeaderVM>();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, *  from SalesHeader sh " +
                "inner join SalesDetail sd on sh.HeaderId=sd.HeaderId" +
                "Inner join Medicine M on sd.MedicineId=M.MedicineId where 1=1 ");
            if (filterQuery.HeaderId > 0)
            {
                sb.Append(" And HeaderId = @HeaderId");
                parameters.Add("HeaderId", filterQuery.HeaderId);
            }
            if (filterQuery.PatientId > 0)
            {
                sb.Append(" And PatientId = @PatientId");
                parameters.Add("PatientId", filterQuery.PatientId);
            }
            if (!string.IsNullOrEmpty(filterQuery.HeaderNumber))
            {
                sb.Append(" And HeaderNumber like @HeaderNumber");
                parameters.Add("HeaderNumber", "%" + filterQuery.HeaderNumber + "%");
            }
            if (filterQuery.DateFrom != null)
            {
                sb.Append(" And CreatedOn >= @DateFrom");
                parameters.Add("DateFrom", filterQuery.DateFrom);
            }
            if (filterQuery.DateTo != null)
            {
                sb.Append(" And CreatedOn <= @DateTo");
                parameters.Add("DateTo", filterQuery.DateTo);
            }

            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return await _DbConnection.QueryAsync<SalesHeaderVM, SalesDetailVM,MedicinesVM, SalesHeaderVM>(sb.ToString(), (header, detail,medicine) =>
            {
                // If the order isn't in the dictionary, add it
                if (!orderDict.TryGetValue(header.HeaderId, out var order))
                {
                    order = header;
                    order.Detail.Add(detail); // Add the first detail
                    orderDict.Add(header.HeaderId, order);
                }
                else
                {
                    // Add the detail to the existing order
                    order.Detail.Add(detail);
                }

                // Attach product to the order detail
                detail.Medicine = medicine;


                return order;
            }, parameters, _DbTransaction, splitOn: "DetailId,MedicineId");
        }
    }
}

