using MediatR;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.OrdenCQRS.Querys.GetCO
{
    public record GetCustomerOrderQuery (int IdOrder)  : IRequest<DatabaseResponse>;
}
