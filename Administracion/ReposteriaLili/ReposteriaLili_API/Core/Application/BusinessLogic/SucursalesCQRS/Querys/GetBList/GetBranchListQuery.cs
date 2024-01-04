using MediatR;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.SucursalesCQRS.Querys.GetBList
{
    public record GetBranchListQuery : IRequest<DatabaseResponse>; 
}
