using Dapper;
using PharmacyApp.Application.Queries.Prescriptionss;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class PrescriptionsRepository : GenericRepository<Prescriptions>, IPrescriptionsRepository
    {
        public PrescriptionsRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<PrescriptionsVM>> GetByPaging(GetPrescriptionsByFilterQuery filterQuery)
        {
            
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, *  from Prescriptions  where 1=1 ");
            if (filterQuery.prescription_id>0)
            {
                sb.Append(" And prescription_id = @prescription_id");
                parameters.Add("prescription_id",  filterQuery.prescription_id);
            }
            if (filterQuery.PatientId>0)
            {
                sb.Append(" And PatientId = @PatientId");
                parameters.Add("PatientId",  filterQuery.PatientId);
            }
            if (!string.IsNullOrEmpty(filterQuery.DoctorName))
            {
                sb.Append(" And DoctorName like @DoctorName");
                parameters.Add("DoctorName", "%" + filterQuery.DoctorName + "%");
            }
            if (!string.IsNullOrEmpty(filterQuery.DoctorContact))
            {
                sb.Append(" And DoctorContact like @DoctorContact");
                parameters.Add("DoctorContact", "%" + filterQuery.DoctorContact + "%");
            }
           
                       
            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return (await _DbConnection.QueryAsync<PrescriptionsVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

