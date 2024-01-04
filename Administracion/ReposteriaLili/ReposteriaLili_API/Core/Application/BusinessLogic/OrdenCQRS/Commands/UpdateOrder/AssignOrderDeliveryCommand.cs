using MediatR;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Commands.UpdateOrder
{
    public record AssignOrderDeliveryCommand(AssignDeliveryRequestDTO assignDeliveryRequest) : IRequest<DatabaseResponse>;
}
