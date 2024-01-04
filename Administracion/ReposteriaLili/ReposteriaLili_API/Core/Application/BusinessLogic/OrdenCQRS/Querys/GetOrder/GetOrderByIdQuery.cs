using MediatR;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetOrder
{
    public record GetOrderByIdQuery(int IdOrder) : IRequest<DatabaseResponse>;

}
