using MediatR;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Querys.GetEmployeeList
{
    public record GetEmployeeListQuery : IRequest<DatabaseResponse>;
}
