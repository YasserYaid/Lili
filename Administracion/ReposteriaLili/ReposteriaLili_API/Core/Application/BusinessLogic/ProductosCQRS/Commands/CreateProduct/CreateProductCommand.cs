using MediatR;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Commands.CreateProduct
{
    public record CreateProductCommand(ProductoCreateDTO productoCreateDTO) : IRequest<DatabaseResponse>;
}
