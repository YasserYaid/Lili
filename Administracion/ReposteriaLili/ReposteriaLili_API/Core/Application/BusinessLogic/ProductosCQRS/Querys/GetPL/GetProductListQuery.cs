using MediatR;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.ProductosCQRS.Querys.GetPL
{
    //IMPORTANTE PASAR EL ID DE LA SUCURSAL SIMEPRE
    public record GetProductListQuery(int idSucursal, string categoria, string nombre) : IRequest<DatabaseResponse>;
}
