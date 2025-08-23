using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.Commands.SalesDetails;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.SalesHeaders
{
    public class CreateSalesHeaderHandler : IRequestHandler<CreateSalesHeaderCommand, ResponseModel<SalesHeaderVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreateSalesHeaderHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateSalesHeaderHandler(IUnitofWork unitofWork, ILogger<CreateSalesHeaderHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<SalesHeaderVM>> Handle(CreateSalesHeaderCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<SalesHeaderVM>();
            try
            {
                if (request.DetailList != null && request.DetailList.Count() > 0)
                {
                    int maxvalue = await _unitofWork.SalesHeader.GetMaxValue(d => d.HeaderId, x => x.HeaderNumber, "INV00");
                    int Maxnumber = 0;
                    if (maxvalue > 0)
                    {
                        Maxnumber = maxvalue;
                    }
                    int maxnum = Maxnumber == 0 ? (0 + 1) : (Maxnumber + 1);
                    var invNO = "INV00" + maxnum.ToString("00000");
                    _unitofWork.BeginDbTransaction();
                    var model = request.Adapt<SalesHeader>();
                    model.HeaderNumber = invNO;
                    var result = await _unitofWork.SalesHeader.Add(model);
                    if (result > 0)
                    {
                        foreach (var item in request.DetailList)
                        {
                            item.HeaderId = result;
                            var detailRes = await _mediator.Send(item);
                        }
                        _unitofWork.Commit();
                        var res = await _unitofWork.SalesHeader.GetById(result);
                        response.Data = res.Adapt<SalesHeaderVM>();
                        response.Succeeded = true;
                        response.Message = "Saved Successfully.";
                        return response;
                    }
                    else
                    {
                        _unitofWork.Rollback();
                        response.Succeeded = false;
                        response.Message = "Failed to save.";
                        return response;
                    }
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "Please add atleast a medicine in list.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                _unitofWork.Rollback();
                response.Succeeded = false;
                response.Message = ex.Message.ToString();
                return response;
            }

        }
    }
}

