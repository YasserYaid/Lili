using MediatR;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetDMOL
{
    public record GetDeliveryManOrderListQuery(int IdEmpleado) : IRequest<DatabaseResponse>;
}
