using MediatR;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Querys.GetDMList
{
    //Obtiene todos los empleados que son repartidores y no hace falta pasar el id
    public record GetDeliveryManListQuery : IRequest<DatabaseResponse>;
}
