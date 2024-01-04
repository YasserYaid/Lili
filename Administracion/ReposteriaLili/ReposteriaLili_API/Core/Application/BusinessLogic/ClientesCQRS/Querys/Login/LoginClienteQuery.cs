using MediatR;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.ClientesCQRS.Querys.Login
{
    public record LoginClienteQuery(LoginClienteRequestDTO LoginClienteDTO, string? secretKey) : IRequest<DatabaseResponse>;
}
