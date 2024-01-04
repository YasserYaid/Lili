using MediatR;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.CreateOrder
{
    public record CreateOrderCommand(CartOrderCreateDTO cartOrderCreateDTO) : IRequest<DatabaseResponse>;
}
