using Dapper;
using PharmacyApp.Application.Queries.Patientss;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.ViewModels;
using System.Text;

namespace PharmacyApp.Data.Repository
{
    public class PatientsRepository : GenericRepository<Patients>, IPatientsRepository
    {
        public PatientsRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IEnumerable<PatientsVM>> GetByPaging(GetPatientsByFilterQuery filterQuery)
        {
            
            StringBuilder sb = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            sb.Append("Select COUNT(1) OVER() as TotalRecord, *  from Patients  where 1=1 ");
            if (!string.IsNullOrEmpty(filterQuery.FullName))
            {
                sb.Append(" And FullName like @FullName");
                parameters.Add("FullName", "%" + filterQuery.FullName + "%");
            }
            if (!string.IsNullOrEmpty(filterQuery.Address))
            {
                sb.Append(" And Address like @Address");
                parameters.Add("Address", "%" + filterQuery.Address + "%");
            }
            if (!string.IsNullOrEmpty(filterQuery.MobileNo))
            {
                sb.Append(" And MobileNo = @MobileNo");
                parameters.Add("MobileNo", filterQuery.MobileNo);
            }
           
            if (filterQuery.PatientId > 0)
            {
                sb.Append(" And PatientId = @PatientId");
                parameters.Add("PatientId", filterQuery.PatientId);
            }
                       
            sb.Append(" ORDER BY " + filterQuery.SortBy + " " + filterQuery.SortOrder + " OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY ");
            parameters.Add("SkipRow", filterQuery.SkipRow);
            parameters.Add("PageSize", filterQuery.PageSize);
            return (await _DbConnection.QueryAsync<PatientsVM>(sb.ToString(), parameters, _DbTransaction));
        }
    }
}

