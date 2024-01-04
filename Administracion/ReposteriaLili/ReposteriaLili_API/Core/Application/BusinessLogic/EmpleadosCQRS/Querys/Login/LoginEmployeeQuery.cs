using MediatR;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Querys.Login
{
    public record LoginEmployeeQuery(LoginEmpleadoRequestDTO LoginEmpleadoDTO, string secretKey) : IRequest<DatabaseResponse>;
}
