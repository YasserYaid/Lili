using MediatR;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetCOL
{
    public record GetCustomerOrderListQuery(int IdCliente) : IRequest<DatabaseResponse>;
}
