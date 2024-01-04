using MediatR;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetOL
{
    public record GetOrderListQuery : IRequest<DatabaseResponse>;
}
