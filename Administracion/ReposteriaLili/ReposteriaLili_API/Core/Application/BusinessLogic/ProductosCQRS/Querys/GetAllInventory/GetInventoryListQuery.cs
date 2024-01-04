using MediatR;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Querys.GetAllInventory
{
    public record GetInventoryListQuery : IRequest<DatabaseResponse>;
}
