using MediatR;
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrdenUpdateDTO UpdateOrdenDTO) : IRequest<DatabaseResponse>;
}
