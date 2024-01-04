using MediatR;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.DeleteOrder
{
    public record DeleteOrderCommand(int idOrden) : IRequest<DatabaseResponse>;
}
