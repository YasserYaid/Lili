using MediatR;
using ReposteriaLili_API.Core.Application.DTO.EmpleadosDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Commands.CreateEmployee
{
    public record CreateEmployeeCommand(EmpleadoCreateDTO CreateEmpleadoDTO) : IRequest<DatabaseResponse>;
}
