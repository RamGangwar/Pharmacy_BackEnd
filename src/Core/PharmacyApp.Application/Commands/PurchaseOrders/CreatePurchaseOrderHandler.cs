using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
using System.ComponentModel.Design;
using System.Linq.Expressions;

namespace PharmacyApp.Application.Commands.PurchaseOrders
{
    public class CreatePurchaseOrderHandler : IRequestHandler<CreatePurchaseOrderCommand, ResponseModel<PurchaseOrderVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreatePurchaseOrderHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreatePurchaseOrderHandler(IUnitofWork unitofWork, ILogger<CreatePurchaseOrderHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<PurchaseOrderVM>> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<PurchaseOrderVM>();
            if (request.Detail != null && request.Detail.Count() > 0)
            {
                try
                {
                    int maxvalue = await _unitofWork.PurchaseOrder.GetMaxValue(d => d.PurchaseId, x => x.PurchaseNumber, "PO00");
                    int Maxnumber = 0;
                    if (maxvalue > 0)
                    {
                        Maxnumber = maxvalue;
                    }
                    int maxnum = Maxnumber == 0 ? (0 + 1) : (Maxnumber + 1);
                    var POnumber = "PO00" + maxnum.ToString("00000");
                    _unitofWork.BeginDbTransaction();
                    var model = request.Adapt<PurchaseOrder>();
                    model.PurchaseNumber = POnumber;
                    var result = await _unitofWork.PurchaseOrder.Add(model);
                    if (result > 0)
                    {
                        foreach (var item in request.Detail)
                        {
                            item.PurchaseId = result;
                            var detailRes = await _mediator.Send(item);
                        }
                        _unitofWork.Commit();
                        var res = await _unitofWork.PurchaseOrder.GetById(result);
                        response.Data = res.Adapt<PurchaseOrderVM>();
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
                catch (Exception ex)
                {
                    _unitofWork.Rollback();
                    response.Succeeded = false;
                    response.Message = ex.Message.ToString();
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
    }
}

