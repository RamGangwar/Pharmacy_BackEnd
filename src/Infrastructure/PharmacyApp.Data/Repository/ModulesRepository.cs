using Dapper;
using PharmacyApp.Application.Queries.Modules;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using System.Text;
using PharmacyApp.Domain.ViewModels;
using Microsoft.Extensions.Primitives;

namespace PharmacyApp.Data.Repository
{
    public class ModulesRepository : GenericRepository<Modules>, IModuleRepository
    {
        public ModulesRepository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<List<ModulesVM>> GetByPaging(GetModulesByFilterQuery filterQuery)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select COUNT(1) OVER () as TotalRecord , M.* from Modules M inner join [AccessPermission] P on M.ModuleId=P.ModuleId where P.RoleId=@RoleId and M.IsActive=1  ORDER BY M.DisplayOrder Asc ");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("RoleId", filterQuery.RoleId);
            return (await _DbConnection.QueryAsync<ModulesVM>(sb.ToString(), parameters, _DbTransaction)).ToList();
        }
        public async Task<List<ModulesVM>> GetListForAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select COUNT(1) OVER () as TotalRecord , M.* from Modules M where M.IsActive=1  ORDER BY M.ParentId Asc, M.DisplayOrder Asc ");

            return (await _DbConnection.QueryAsync<ModulesVM>(sb.ToString(), null, _DbTransaction)).ToList();
        }



        public async Task<string> SaveColumns(string entityname, int type = 0)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sfile = new StringBuilder();
            sb.Append($"select 'public '+(case IS_NULLABLE when 'NO' then case DATA_TYPE when 'varchar'  then 'string'  when 'text'  then 'string'  when 'nvarchar' then 'string' when 'bit' then 'bool' when 'datetime' then 'DateTime' when 'date'  then 'DateTime' else DATA_TYPE end when 'YES' then case DATA_TYPE when 'varchar'  then 'string' when 'text'  then 'string' when 'nvarchar' then 'string' when 'bit' then 'bool?' when 'datetime' then 'DateTime?'  when 'date'  then 'DateTime?' else DATA_TYPE end end)+' '+ COLUMN_NAME + ' {{get; set;}}' from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@entityname ");
            //and COLUMN_NAME not in ('IsActive','CreatedBy','CreatedOn','ModifyBy','ModifyOn')
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("entityname", entityname);
            var list = await _DbConnection.QueryAsync<string>(sb.ToString(), parameters, _DbTransaction);
            string filePath = string.Empty;
            if (type == 0)
            {
                sfile.Append($"using Dapper.Contrib.Extensions;\r\n\r\nnamespace PharmacyApp.Domain.Entity\r\n{{\r\n    [Table(\"" + entityname + "\")]\r\n    public class " + entityname + " \r\n    {\r\n        [Key] \r\n ");//: BaseEntity
                foreach (var item in list)
                {
                    sfile.Append(item);
                    sfile.Append($"\r\n");
                }
                sfile.Append($"}}\r\n}}");
                filePath = "D:\\Working Project\\PharmacyApp\\src\\Core\\PharmacyApp.Domain\\Entity\\" + entityname + ".cs";
            }
            else
            {
                sfile.Append($"using System.Text.Json.Serialization; \r\n namespace PharmacyApp.Domain.ViewModels \r\n{{\r\n public class " + entityname + "VM \r\n{\r\n");
                foreach (var item in list)
                {
                    sfile.Append(item);
                    sfile.Append($"\r\n");
                }
                //sfile.Append($"public bool IsActive {{ get; set; }}\r\n        public DateTime CreatedOn {{ get; set; }}\r\n        public int CreatedBy {{ get; set; }}\r\n        [JsonIgnore]\r\n        public int TotalRecord {{ get; set; }}\r\n    }}\r\n}}");
                sfile.Append($" [JsonIgnore]\r\n        public int TotalRecord {{ get; set; }}\r\n    }}\r\n}}");
                filePath = "D:\\Working Project\\PharmacyApp\\src\\Core\\PharmacyApp.Domain\\ViewModels\\" + entityname + "VM.cs";
            }
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(sfile);
            }
            return "done";
        }

        public async Task<bool> SaveEntityOrModel(int type)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                //sb.Append("select [name] from sys.tables where Cast(create_date as Date)>='2024-11-14' and [name]<>'Modules' order by name asc ");
                sb.Append("select [name] from sys.tables where [name] in ('Prescriptions','PurchaseDetail','PurchaseOrder','SalesHeader','SalesDetail' )order by name asc ");
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("RoleId", filterQuery.RoleId);
                var list = await _DbConnection.QueryAsync<tables>(sb.ToString(), null, _DbTransaction);
                foreach (var item in list)
                {
                    var res = await SaveColumns(item.Name, 0);
                    var resmod = await SaveColumns(item.Name, 1);
                    var cpmmnd = await SaveCommand(item.Name);
                    var qry = await SaveQueries(item.Name);
                    var isres = await CreateRepository(item.Name);
                    var cpmmndhndlr = await SaveCommandHandler(item.Name);
                    var qryhndlr = await SaveQueriesHandler(item.Name);
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }

        private async Task<bool> CreateRepository(string entityName)
        {
            StringBuilder sfile = new StringBuilder();
            sfile.Append($"using PharmacyApp.Application.Queries." + entityName + "s;\r\n");
            sfile.Append($"using PharmacyApp.Domain.Entity;\r\n");
            sfile.Append($"using PharmacyApp.Domain.ViewModels;\r\n");
            sfile.Append($"\r\n");
            sfile.Append($"namespace PharmacyApp.Application.Repository\r\n");
            sfile.Append($"{{\r\n");
            sfile.Append($"    public interface I" + entityName + "Repository : IGenericRepository<" + entityName + ">\r\n");
            sfile.Append($"    {{\r\n");
            sfile.Append($"        Task<IEnumerable<" + entityName + "VM>> GetByPaging(Get" + entityName + "ByFilterQuery filterQuery);\r\n");
            sfile.Append($"    }}\r\n");
            sfile.Append($"}}\r\n");

            string filePath = string.Empty;
            filePath = "D:\\Working Project\\PharmacyApp\\src\\Core\\PharmacyApp.Application\\Repository\\I" + entityName + "Repository.cs";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(sfile);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append($"select COLUMN_NAME as columnname,(case DATA_TYPE when 'varchar' then 'string' when 'nvarchar' then 'string' when 'bit' then 'bool' when 'datetime' then 'DateTime' when 'date' then 'DateTime' when 'text' then 'string' else DATA_TYPE end) as datatype from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@entityname ");
            //and COLUMN_NAME not in ('IsActive','CreatedBy','CreatedOn','ModifyBy','ModifyOn')
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("entityname", entityName);
            var list = await _DbConnection.QueryAsync<columns>(sb.ToString(), parameters, _DbTransaction);

            StringBuilder sfilec = new StringBuilder();

            sfilec.Append($"using Dapper;\r\n");
            sfilec.Append($"using PharmacyApp.Application.Queries." + entityName + "s;\r\n");
            sfilec.Append($"using PharmacyApp.Application.Repository;\r\n");
            sfilec.Append($"using PharmacyApp.Application.UnitOfWork;\r\n");
            sfilec.Append($"using PharmacyApp.Domain.Entity;\r\n");
            sfilec.Append($"using PharmacyApp.Domain.ViewModels;\r\n");
            sfilec.Append($"using System.Text;\r\n");
            sfilec.Append($"\r\n");
            sfilec.Append($"namespace PharmacyApp.Data.Repository\r\n");
            sfilec.Append($"{{\r\n");
            sfilec.Append($"    public class " + entityName + "Repository : GenericRepository<" + entityName + ">, I" + entityName + "Repository\r\n");
            sfilec.Append($"    {{\r\n");
            sfilec.Append($"        public " + entityName + "Repository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)\r\n");
            sfilec.Append($"        {{\r\n");
            sfilec.Append($"\r\n");
            sfilec.Append($"        }}\r\n");
            sfilec.Append($"\r\n");
            sfilec.Append($"        public async Task<IEnumerable<" + entityName + "VM>> GetByPaging(Get" + entityName + "ByFilterQuery filterQuery)\r\n");
            sfilec.Append($"        {{\r\n");
            sfilec.Append($"            \r\n");
            sfilec.Append($"            StringBuilder sb = new StringBuilder();\r\n");
            sfilec.Append($"            DynamicParameters parameters = new DynamicParameters();\r\n");
            sfilec.Append($"            sb.Append(\"Select COUNT(1) OVER() as TotalRecord, *  from " + entityName + "  where 1=1 \");\r\n");
            foreach (var item in list)
            {
                if (item.datatype=="int")
                {
                    sfilec.Append($"            if (filterQuery." + item.columnname + ">0)\r\n");
                    sfilec.Append($"            {{\r\n");
                    sfilec.Append($"                sb.Append(\" And " + item.columnname + " = @" + item.columnname + "\");\r\n");
                    sfilec.Append($"                parameters.Add(\"" + item.columnname + "\",  filterQuery." + item.columnname + ");\r\n");
                    sfilec.Append($"            }}\r\n");
                }
                else if (item.datatype=="string")
                {
                    sfilec.Append($"            if (!string.IsNullOrEmpty(filterQuery." + item.columnname + "))\r\n");
                    sfilec.Append($"            {{\r\n");
                    sfilec.Append($"                sb.Append(\" And " + item.columnname + " like @" + item.columnname + "\");\r\n");
                    sfilec.Append($"                parameters.Add(\"" + item.columnname + "\", \"%\" + filterQuery." + item.columnname + " + \"%\");\r\n");
                    sfilec.Append($"            }}\r\n");
                }
                
            }
            sfilec.Append($"                       \r\n");
            sfilec.Append($"            sb.Append(\" ORDER BY \" + filterQuery.SortBy + \" \" + filterQuery.SortOrder + \" OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY \");\r\n");
            sfilec.Append($"            parameters.Add(\"SkipRow\", filterQuery.SkipRow);\r\n");
            sfilec.Append($"            parameters.Add(\"PageSize\", filterQuery.PageSize);\r\n");
            sfilec.Append($"            return (await _DbConnection.QueryAsync<" + entityName + "VM>(sb.ToString(), parameters, _DbTransaction));\r\n");
            sfilec.Append($"        }}\r\n");
            sfilec.Append($"    }}\r\n");
            sfilec.Append($"}}\r\n");




            //sfilec.Append($"using Dapper;\r\n");
            //sfilec.Append($"using PharmacyApp.Application.Queries." + entityName + "s;\r\n");
            //sfilec.Append($"using PharmacyApp.Application.Repository;\r\n");
            //sfilec.Append($"using PharmacyApp.Application.UnitOfWork;\r\n");
            //sfilec.Append($"using PharmacyApp.Domain.Entity;\r\n");
            //sfilec.Append($"using PharmacyApp.Domain.ViewModels;\r\n");
            //sfilec.Append($"using System.Text;\r\n");
            //sfilec.Append($"\r\n");
            //sfilec.Append($"namespace PharmacyApp.Data.Repository\r\n");
            //sfilec.Append($"{{\r\n");
            //sfilec.Append($"    public class " + entityName + "Repository : GenericRepository<" + entityName + ">, I" + entityName + "Repository\r\n");
            //sfilec.Append($"    {{\r\n");
            //sfilec.Append($"        public " + entityName + "Repository(IBaseUnitOfWork unitOfWork) : base(unitOfWork)\r\n");
            //sfilec.Append($"        {{\r\n");
            //sfilec.Append($"\r\n");
            //sfilec.Append($"        }}\r\n");
            //sfilec.Append($"\r\n");
            //sfilec.Append($"        public async Task<IEnumerable<" + entityName + "VM>> GetByPaging(Get" + entityName + "ByFilterQuery filterQuery)\r\n");
            //sfilec.Append($"        {{\r\n");
            //sfilec.Append($"            StringBuilder sb = new StringBuilder();\r\n");
            //sfilec.Append($"            sb.Append(\"Select COUNT(1) OVER() as TotalRecord, *from " + entityName + "  ORDER BY \" + filterQuery.SortBy + \" \" + filterQuery.SortOrder + \" OFFSET @SkipRow ROWS FETCH NEXT @PageSize ROWS ONLY \"); \r\n");
            //sfilec.Append($"           DynamicParameters parameters = new DynamicParameters();\r\n");
            //sfilec.Append($"           parameters.Add(\"SkipRow\", filterQuery.SkipRow);\r\n");
            //sfilec.Append($"           parameters.Add(\"PageSize\", filterQuery.PageSize);\r\n");
            //sfilec.Append($"           return (await _DbConnection.QueryAsync<" + entityName + "VM>(sb.ToString(), parameters, _DbTransaction));\r\n");
            //sfilec.Append($"        }}\r\n");
            //sfilec.Append($"    }}\r\n");
            //sfilec.Append($"}}\r\n");
            string filePathc = string.Empty;
            filePathc = "D:\\Working Project\\PharmacyApp\\src\\Infrastructure\\PharmacyApp.Data\\Repository\\" + entityName + "Repository.cs";
            using (StreamWriter writer = new StreamWriter(filePathc))
            {
                writer.WriteLine(sfilec);
            }

            return true;
        }

        private async Task<string> SaveCommand(string entityname)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sfile = new StringBuilder();
            StringBuilder Ufile = new StringBuilder();
            StringBuilder Dfile = new StringBuilder();
            sb.Append($"select  case IS_NULLABLE when 'NO' then '[Required(ErrorMessage = \"'+COLUMN_NAME+' is required\")] \r\n ' else ' ' end + 'public '+(case IS_NULLABLE when 'NO' then case DATA_TYPE when 'varchar'  then 'string' when 'text'  then 'string'  when 'nvarchar' then 'string' when 'bit' then 'bool' when 'datetime' then 'DateTime' when 'date' then 'DateTime' else DATA_TYPE end when 'YES' then case DATA_TYPE when 'varchar'  then 'string' when 'text'  then 'string'  when 'nvarchar' then 'string' when 'bit' then 'bool?' when 'datetime' then 'DateTime?' when 'date' then 'DateTime?' else DATA_TYPE end end)+' '+ COLUMN_NAME + ' {{get; set;}}' from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@entityname ");
            //and COLUMN_NAME not in ('CreatedBy','CreatedOn','ModifyBy','ModifyOn')
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("entityname", entityname);
            var list = await _DbConnection.QueryAsync<string>(sb.ToString(), parameters, _DbTransaction);
            string directoryPath = string.Empty;

            sfile.Append($"using System.ComponentModel.DataAnnotations;\r\nusing MediatR;\r\nusing PharmacyApp.Domain.Model;\r\nusing PharmacyApp.Domain.ViewModels;\r\nnamespace PharmacyApp.Application.Commands." + entityname + "s\r\r {\r\n public class Create" + entityname + "Command : IRequest<ResponseModel<" + entityname + "VM>>\r\n{");
            foreach (var item in list)
            {
                sfile.Append(item);
                sfile.Append($"\r\n");
            }
            sfile.Append($"}}\r\n}}");
            directoryPath = "D:\\Working Project\\PharmacyApp\\src\\Core\\PharmacyApp.Application\\Commands\\" + entityname + "s";

            if (!Directory.Exists(directoryPath))
            {
                // If the directory does not exist, create it
                Directory.CreateDirectory(directoryPath);
            }
            string fileName = "Create" + entityname + "Command.cs";
            string filePath = Path.Combine(directoryPath, fileName);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(sfile);
            }

            Ufile.Append($"using System.ComponentModel.DataAnnotations;\r\nusing MediatR;\r\nusing PharmacyApp.Domain.Model;\r\nusing PharmacyApp.Domain.ViewModels;\r\nnamespace PharmacyApp.Application.Commands." + entityname + "s\r\r {\r\n public class Update" + entityname + "Command : IRequest<ResponseModel>\r\n{");
            foreach (var item in list)
            {
                Ufile.Append(item);
                Ufile.Append($"\r\n");
            }
            Ufile.Append($"}}\r\n}}");


            string updatefileName = "Update" + entityname + "Command.cs";
            string updatefilePath = Path.Combine(directoryPath, updatefileName);

            using (StreamWriter writer = new StreamWriter(updatefilePath))
            {
                writer.WriteLine(Ufile);
            }

            Dfile.Append($"using System.ComponentModel.DataAnnotations;\r\nusing MediatR;\r\nusing PharmacyApp.Domain.Model;\r\nusing PharmacyApp.Domain.ViewModels;\r\nnamespace PharmacyApp.Application.Commands." + entityname + "s\r\r {\r\n public class Delete" + entityname + "Command : IRequest<ResponseModel>\r\n{");
            for (int i = 0; i < 1; i++)
            {
                Dfile.Append(list.ToList()[i]);
                Dfile.Append($"\r\n");
            }
            Dfile.Append($"}}\r\n}}");
            string deletefileName = "Delete" + entityname + "Command.cs";
            string deletefilePath = Path.Combine(directoryPath, deletefileName);

            using (StreamWriter writer = new StreamWriter(deletefilePath))
            {
                writer.WriteLine(Dfile);
            }
            return "done";
        }

        private async Task<string> SaveCommandHandler(string entityName)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sfile = new StringBuilder();
            StringBuilder Ufile = new StringBuilder();
            StringBuilder Dfile = new StringBuilder();

            string directoryPath = string.Empty;

            sfile.Append($"using Mapster;\r\n");
            sfile.Append($"using MediatR;\r\n");
            sfile.Append($"using Microsoft.AspNetCore.Http;\r\n");
            sfile.Append($"using Microsoft.Extensions.Logging;\r\n");
            sfile.Append($"using PharmacyApp.Application.UnitOfWork;\r\n");
            sfile.Append($"using PharmacyApp.Domain.Entity;\r\n");
            sfile.Append($"using PharmacyApp.Domain.Model;\r\n");
            sfile.Append($"using PharmacyApp.Domain.ViewModels;\r\n");
            sfile.Append($"namespace PharmacyApp.Application.Commands." + entityName + "s\r\n");
            sfile.Append($"{{\r\n");
            sfile.Append($"    public class Create" + entityName + "Handler : IRequestHandler<Create" + entityName + "Command, ResponseModel<" + entityName + "VM>>\r\n");
            sfile.Append($"    {{\r\n");
            sfile.Append($"        private readonly IUnitofWork _unitofWork;\r\n");
            sfile.Append($"        private readonly ILogger<Create" + entityName + "Handler> _logger;\r\n");
            sfile.Append($"        private readonly IMediator _mediator;\r\n");
            sfile.Append($"        private readonly IHttpContextAccessor _httpContextAccessor;\r\n");
            sfile.Append($"\r\n");
            sfile.Append($"        public Create" + entityName + "Handler(IUnitofWork unitofWork, ILogger<Create" + entityName + "Handler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)\r\n");
            sfile.Append($"        {{\r\n");
            sfile.Append($"            _unitofWork = unitofWork;\r\n");
            sfile.Append($"            _logger = logger;\r\n");
            sfile.Append($"            _mediator = mediator;\r\n");
            sfile.Append($"            _httpContextAccessor = httpContextAccessor;\r\n");
            sfile.Append($"        }}\r\n");
            sfile.Append($"\r\n");
            sfile.Append($"        public async Task<ResponseModel<" + entityName + "VM>> Handle(Create" + entityName + "Command request, CancellationToken cancellationToken)\r\n");
            sfile.Append($"        {{\r\n");
            sfile.Append($"\r\n");
            sfile.Append($"            _logger.LogInformation(nameof(Handle), request);\r\n");
            sfile.Append($"            var response = new ResponseModel<" + entityName + "VM>();\r\n");
            sfile.Append($"//            var dept = await _unitofWork." + entityName + ".GetEntityAsync(a => a." + entityName + "Name == request." + entityName + "Name);\r\n");
            sfile.Append($"//            if (dept == null)\r\n");
            sfile.Append($"//            {{\r\n");
            sfile.Append($"//                var model = request.Adapt<" + entityName + ">();\r\n");
            sfile.Append($"//                var result = await _unitofWork." + entityName + ".Add(model);\r\n");
            sfile.Append($"//                if (result > 0)\r\n");
            sfile.Append($"//                {{\r\n");
            sfile.Append($"//                    var res = await _unitofWork." + entityName + ".GetById(result);\r\n");
            sfile.Append($"//                    response.Data = res.Adapt<" + entityName + "VM>();\r\n");
            sfile.Append($"//                    response.Succeeded=true;\r\n");
            sfile.Append($"//                    response.Message = \"Saved Successfully.\";\r\n");
            sfile.Append($"//                    return response;\r\n");
            sfile.Append($"//                }}\r\n");
            sfile.Append($"//                else\r\n");
            sfile.Append($"//                {{\r\n");
            sfile.Append($"//                    response.Succeeded=false;\r\n");
            sfile.Append($"//                    response.Message = \"Failed to save.\";\r\n");
            sfile.Append($"//                    return response;\r\n");
            sfile.Append($"//                }}\r\n");
            sfile.Append($"//            }}\r\n");
            sfile.Append($"//            else\r\n");
            sfile.Append($"//            {{\r\n");
            sfile.Append($"//                response.Succeeded=false;\r\n");
            sfile.Append($"//                response.Message = \"" + entityName + " Already Exists.\";\r\n");
            sfile.Append($"//                return response;\r\n");
            sfile.Append($"//            }}\r\n");
            sfile.Append($"             return response;\r\n");
            sfile.Append($"        }}\r\n");
            sfile.Append($"    }}\r\n");
            sfile.Append($"}}\r\n");

            directoryPath = "D:\\Working Project\\PharmacyApp\\src\\Core\\PharmacyApp.Application\\Commands\\" + entityName + "s";

            if (!Directory.Exists(directoryPath))
            {
                //If the directory does not exist, create it
                Directory.CreateDirectory(directoryPath);
            }
            string fileName = "Create" + entityName + "Handler.cs";
            string filePath = Path.Combine(directoryPath, fileName);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(sfile);
            }

            Ufile.Append($"using MediatR;\r\n");
            Ufile.Append($"using Microsoft.AspNetCore.Http;\r\n");
            Ufile.Append($"using Microsoft.Extensions.Logging;\r\n");
            Ufile.Append($"using PharmacyApp.Application.UnitOfWork;\r\n");
            Ufile.Append($"using PharmacyApp.Domain.Model;\r\n");
            Ufile.Append($"\r\n");
            Ufile.Append($"namespace PharmacyApp.Application.Commands." + entityName + "s\r\n");
            Ufile.Append($"{{\r\n");
            Ufile.Append($"    public class Update" + entityName + "Handler : IRequestHandler<Update" + entityName + "Command, ResponseModel>\r\n");
            Ufile.Append($"    {{\r\n");
            Ufile.Append($"        private readonly IUnitofWork _unitofWork;\r\n");
            Ufile.Append($"        private readonly ILogger<Update" + entityName + "Handler> _logger;\r\n");
            Ufile.Append($"        private readonly IHttpContextAccessor _httpContextAccessor;\r\n");
            Ufile.Append($"        public Update" + entityName + "Handler(IUnitofWork unitofWork, ILogger<Update" + entityName + "Handler> logger, IHttpContextAccessor httpContextAccessor)\r\n");
            Ufile.Append($"        {{\r\n");
            Ufile.Append($"            _unitofWork = unitofWork;\r\n");
            Ufile.Append($"            _logger = logger;\r\n");
            Ufile.Append($"            _httpContextAccessor = httpContextAccessor;\r\n");
            Ufile.Append($"        }}\r\n");
            Ufile.Append($"        public async Task<ResponseModel> Handle(Update" + entityName + "Command request, CancellationToken cancellationToken)\r\n");
            Ufile.Append($"        {{\r\n");
            Ufile.Append($"            _logger.LogInformation(nameof(Handle), request);\r\n");
            Ufile.Append($"//            var deptDuplicate = await _unitofWork." + entityName + ".GetEntityAsync(a => a." + entityName + "Name == request." + entityName + "Name && a." + entityName + "Id != request." + entityName + "Id);\r\n");
            Ufile.Append($"//            if (deptDuplicate != null)\r\n");
            Ufile.Append($"//            {{\r\n");
            Ufile.Append($"//                return new ResponseModel {{Message = \"" + entityName + " Already Exists\",Succeeded=false};\r\n");
            Ufile.Append($"//            }}\r\n");
            Ufile.Append($"//            var dept = await _unitofWork." + entityName + ".GetById(request." + entityName + "Id);\r\n");
            Ufile.Append($"//            if (dept != null && dept." + entityName + "Id > 0)\r\n");
            Ufile.Append($"//            {{\r\n");
            Ufile.Append($"//                dept." + entityName + "Name = request." + entityName + "Name;\r\n");
            Ufile.Append($"//                var result = await _unitofWork." + entityName + ".Update(dept);\r\n");
            Ufile.Append($"//                if (result)\r\n");
            Ufile.Append($"//                {{\r\n");
            Ufile.Append($"//                    return new ResponseModel {{Message = \"Updated Successfully\",Succeeded=true}};\r\n");
            Ufile.Append($"//                }}\r\n");
            Ufile.Append($"//            }}\r\n");
            Ufile.Append($"            return new ResponseModel {{Message = \"Failed to update\",Succeeded=false}};\r\n");
            Ufile.Append($"        }}\r\n");
            Ufile.Append($"    }}\r\n");
            Ufile.Append($"}}\r\n");



            string updatefileName = "Update" + entityName + "Handler.cs";
            string updatefilePath = Path.Combine(directoryPath, updatefileName);

            using (StreamWriter writer = new StreamWriter(updatefilePath))
            {
                writer.WriteLine(Ufile);
            }

            Dfile.Append($"using MediatR;\r\n");
            Dfile.Append($"using Microsoft.Extensions.Logging;\r\n");
            Dfile.Append($"using PharmacyApp.Application.UnitOfWork;\r\n");
            Dfile.Append($"using PharmacyApp.Domain.Model;\r\n");
            Dfile.Append($"using System;\r\n");
            Dfile.Append($"using System.Collections.Generic;\r\n");
            Dfile.Append($"using System.Linq;\r\n");
            Dfile.Append($"using System.Text;\r\n");
            Dfile.Append($"using System.Threading.Tasks;\r\n");
            Dfile.Append($"\r\n");
            Dfile.Append($"namespace PharmacyApp.Application.Commands." + entityName + "s\r\n");
            Dfile.Append($"{{\r\n");
            Dfile.Append($"    public class Delete" + entityName + "Handler : IRequestHandler<Delete" + entityName + "Command, ResponseModel>\r\n");
            Dfile.Append($"    {{\r\n");
            Dfile.Append($"        private readonly IUnitofWork _unitofWork;\r\n");
            Dfile.Append($"        private readonly ILogger<Delete" + entityName + "Handler> _logger;\r\n");
            Dfile.Append($"\r\n");
            Dfile.Append($"        public Delete" + entityName + "Handler(IUnitofWork unitofWork, ILogger<Delete" + entityName + "Handler> logger)\r\n");
            Dfile.Append($"        {{\r\n");
            Dfile.Append($"            _unitofWork = unitofWork;\r\n");
            Dfile.Append($"            _logger = logger;\r\n");
            Dfile.Append($"        }}\r\n");
            Dfile.Append($"\r\n");
            Dfile.Append($"        public async Task<ResponseModel> Handle(Delete" + entityName + "Command request, CancellationToken cancellationToken)\r\n");
            Dfile.Append($"        {{\r\n");
            Dfile.Append($"            _logger.LogInformation(nameof(Handle), request);\r\n");
            Dfile.Append($"//            var dept = await _unitofWork." + entityName + ".GetById(request." + entityName + "Id);\r\n");
            Dfile.Append($"//            if (dept != null && dept." + entityName + "Id > 0)\r\n");
            Dfile.Append($"//            {{\r\n");
            Dfile.Append($"//                var res = await _unitofWork." + entityName + ".Delete(dept);\r\n");
            Dfile.Append($"//                return new ResponseModel {{ Message = \"Delete Successfully\", Succeeded=true }};\r\n");
            Dfile.Append($"//            }}\r\n");
            Dfile.Append($"            return new ResponseModel {{ Message = \"" + entityName + " Not Found\", Succeeded=false };\r\n");
            Dfile.Append($"        }}\r\n");
            Dfile.Append($"    }}\r\n");
            Dfile.Append($"}}\r\n");

            string deletefileName = "Delete" + entityName + "Handler.cs";
            string deletefilePath = Path.Combine(directoryPath, deletefileName);

            using (StreamWriter writer = new StreamWriter(deletefilePath))
            {
                writer.WriteLine(Dfile);
            }
            return "done";
        }
        private async Task<string> SaveQueries(string entityname)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sfile = new StringBuilder();
            StringBuilder Ifile = new StringBuilder();
            sb.Append($"select  'public '+(case IS_NULLABLE when 'NO' then case DATA_TYPE when 'varchar'  then 'string' when 'text'  then 'string' when 'nvarchar' then 'string' when 'bit' then 'bool' when 'datetime' then 'DateTime' when 'date' then 'DateTime' else DATA_TYPE end when 'YES' then case DATA_TYPE when 'varchar'  then 'string' when 'text'  then 'string'  when 'nvarchar' then 'string' when 'bit' then 'bool?' when 'datetime' then 'DateTime?' when 'date' then 'DateTime?' else DATA_TYPE end end)+' '+ COLUMN_NAME + ' {{get; set;}}' as columnname,DATA_TYPE as datatype from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@entityname ");
            ///and COLUMN_NAME not in ('IsActive','CreatedBy','CreatedOn','ModifyBy','ModifyOn')
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("entityname", entityname);
            var list = await _DbConnection.QueryAsync<columns>(sb.ToString(), parameters, _DbTransaction);
            string directoryPath = string.Empty;

            sfile.Append($"using System.ComponentModel.DataAnnotations;\r\nusing MediatR;\r\nusing PharmacyApp.Domain.Model.Reponse;\r\nusing PharmacyApp.Domain.ViewModels;\r\nnamespace PharmacyApp.Application.Queries." + entityname + "s { public class Get" + entityname + "ByFilterQuery : PagingRquestModel, IRequest<PagingModel<" + entityname + "VM>> {");
            foreach (var item in list)
            {
                if (item.datatype == "int" || item.datatype == "string")
                {
                    sfile.Append(item.columnname);
                    sfile.Append($"\r\n");
                }
            }
            sfile.Append($"}}\r\n}}");
            directoryPath = "D:\\Working Project\\PharmacyApp\\src\\Core\\PharmacyApp.Application\\Queries\\" + entityname + "s";

            if (!Directory.Exists(directoryPath))
            {
                //If the directory does not exist, create it
                Directory.CreateDirectory(directoryPath);
            }
            string fileName = "Get" + entityname + "ByFilterQuery.cs";
            string filePath = Path.Combine(directoryPath, fileName);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(sfile);
            }

            Ifile.Append($"using MediatR;\r\nusing PharmacyApp.Domain.Model; \r\nusing PharmacyApp.Domain.ViewModels;\r\nnamespace PharmacyApp.Application.Queries." + entityname + "s    \r\n{        \r\npublic class Get" + entityname + "ByIdQuery : IRequest<ResponseModel<" + entityname + "VM>>     \r\n{");
            for (int i = 0; i < 1; i++)
            {
                Ifile.Append(list.ToList()[i].columnname);
                Ifile.Append($"\r\n");
            }
            Ifile.Append($"}}\r\n}}");

            string updatefileName = "Get" + entityname + "ByIdQuery.cs";
            string updatefilePath = Path.Combine(directoryPath, updatefileName);

            using (StreamWriter writer = new StreamWriter(updatefilePath))
            {
                writer.WriteLine(Ifile);
            }

            return "done";
        }

        private async Task<string> SaveQueriesHandler(string entityname)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sfile = new StringBuilder();
            StringBuilder Ufile = new StringBuilder();
            StringBuilder Dfile = new StringBuilder();

            string directoryPath = string.Empty;

            sfile.Append($"using MediatR;\r\n");
            sfile.Append($"using Microsoft.Extensions.Logging;\r\n");
            sfile.Append($"using PharmacyApp.Application.UnitOfWork;\r\n");
            sfile.Append($"using PharmacyApp.Domain.Model.Reponse;\r\n");
            sfile.Append($"using PharmacyApp.Domain.ViewModels;\r\n");
            sfile.Append($"namespace PharmacyApp.Application.Queries." + entityname + "s\r\n");
            sfile.Append($"{{\r\n");
            sfile.Append($"    public class Get" + entityname + "ByFilterHandler : IRequestHandler<Get" + entityname + "ByFilterQuery, PagingModel<" + entityname + "VM>>\r\n");
            sfile.Append($"    {{\r\n");
            sfile.Append($"        private readonly IUnitofWork _unitofWork;\r\n");
            sfile.Append($"        private readonly ILogger<Get" + entityname + "ByFilterHandler> _logger;\r\n");
            sfile.Append($"\r\n");
            sfile.Append($"        public Get" + entityname + "ByFilterHandler(IUnitofWork unitofWork, ILogger<Get" + entityname + "ByFilterHandler> logger)\r\n");
            sfile.Append($"        {{\r\n");
            sfile.Append($"            _unitofWork = unitofWork;\r\n");
            sfile.Append($"            _logger = logger;\r\n");
            sfile.Append($"        }}\r\n");
            sfile.Append($"\r\n");
            sfile.Append($"        public async Task<PagingModel<" + entityname + "VM>> Handle(Get" + entityname + "ByFilterQuery request, CancellationToken cancellationToken)\r\n");
            sfile.Append($"        {{\r\n");
            sfile.Append($"            _logger.LogInformation(nameof(Handle), request);\r\n");
            sfile.Append($"//            var result = await _unitofWork." + entityname + "s.GetByPaging(request);\r\n");
            sfile.Append($"//           return new PagingModel<" + entityname + "VM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);\r\n");
            sfile.Append($"            throw new NotImplementedException();\r\n");
            sfile.Append($"        }}\r\n");
            sfile.Append($"    }}\r\n");
            sfile.Append($"}}\r\n");


            directoryPath = "D:\\Working Project\\PharmacyApp\\src\\Core\\PharmacyApp.Application\\Queries\\" + entityname + "s";

            if (!Directory.Exists(directoryPath))
            {
                //If the directory does not exist, create it
                Directory.CreateDirectory(directoryPath);
            }
            string fileName = "Get" + entityname + "ByFilterHandler.cs";
            string filePath = Path.Combine(directoryPath, fileName);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(sfile);
            }

            Ufile.Append($"using Mapster;\r\n");
            Ufile.Append($"using MediatR;\r\n");
            Ufile.Append($"using Microsoft.Extensions.Logging;\r\n");
            Ufile.Append($"using PharmacyApp.Application.UnitOfWork;\r\n");
            Ufile.Append($"using PharmacyApp.Domain.Model;\r\n");
            Ufile.Append($"using PharmacyApp.Domain.ViewModels;\r\n");
            Ufile.Append($"\r\n");
            Ufile.Append($"namespace PharmacyApp.Application.Queries." + entityname + "s\r\n");
            Ufile.Append($"{{\r\n");
            Ufile.Append($"    public class Get" + entityname + "ByIdHandler : IRequestHandler<Get" + entityname + "ByIdQuery, ResponseModel<" + entityname + "VM>>\r\n");
            Ufile.Append($"    {{\r\n");
            Ufile.Append($"        private readonly IUnitofWork _unitofWork;\r\n");
            Ufile.Append($"        private readonly ILogger<Get" + entityname + "ByIdHandler> _logger;\r\n");
            Ufile.Append($"        private readonly IMediator _mediator;\r\n");
            Ufile.Append($"\r\n");
            Ufile.Append($"        public Get" + entityname + "ByIdHandler(IUnitofWork unitofWork, ILogger<Get" + entityname + "ByIdHandler> logger, IMediator mediator)\r\n");
            Ufile.Append($"        {{\r\n");
            Ufile.Append($"            _unitofWork = unitofWork;\r\n");
            Ufile.Append($"            _logger = logger;\r\n");
            Ufile.Append($"            _mediator = mediator;\r\n");
            Ufile.Append($"        }}\r\n");
            Ufile.Append($"\r\n");
            Ufile.Append($"        public async Task<ResponseModel<" + entityname + "VM>> Handle(Get" + entityname + "ByIdQuery request, CancellationToken cancellationToken)\r\n");
            Ufile.Append($"        {{\r\n");
            Ufile.Append($"            _logger.LogInformation(nameof(Handle), request);\r\n");
            Ufile.Append($"//            var depts = (await _unitofWork." + entityname + "s.GetById(request." + entityname + "Id)).Adapt<" + entityname + "VM>();\r\n");
            Ufile.Append($"//            return new ResponseModel<" + entityname + "VM> \r\n");
            Ufile.Append($"//            {{ \r\n");
            Ufile.Append($"//              Data = depts, Succeeded = depts != null ? true : false, Message = depts != null ? \"Record Found\":\"No Record Found\" \r\n");
            Ufile.Append($"//           }};\r\n");
            Ufile.Append($"            throw new NotImplementedException();\r\n");
            Ufile.Append($"        }}\r\n");
            Ufile.Append($"    }}\r\n");
            Ufile.Append($"}}\r\n");
            string updatefileName = "Get" + entityname + "ByIdHandler.cs";
            string updatefilePath = Path.Combine(directoryPath, updatefileName);

            using (StreamWriter writer = new StreamWriter(updatefilePath))
            {
                writer.WriteLine(Ufile);
            }

            return "done";
        }
    }
    public class tables
    {
        public string Name { get; set; }
    }

    public class columns
    {
        public string columnname { get; set; }
        public string datatype { get; set; }
    }
}
