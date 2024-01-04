using MediatR;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.ReqResModels;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.ClientesCQRS.Commands.CreateAccount
{
    public record CreateAccountCommand(AccountCreateDTO CreateAccountDTO) : IRequest<DatabaseResponse>;
}
