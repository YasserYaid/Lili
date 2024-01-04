using AutoMapper;
using MediatR;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.DTO.ClientesDTOs;
using ReposteriaLili_API.Core.Application.DTO.DireccionesDTOs;
using ReposteriaLili_API.Core.Application.DTO.TarjetasDTOs;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.ClientesCQRS.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public CreateAccountCommandHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();
                if (await _unidadTrabajo.clienteRepo.Obtener(cli => cli.Correo.ToLower() == request.CreateAccountDTO.ClienteCreateDto.Correo.ToLower()) != null)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU02_CU10_CODIGO_CORREO_REPETIDO;
                    _dataBaseResponse.ControlMessage = Constantes.CU02_CU10_MENSAJE_CORREO_REPETIDO;
                }
                else if (await _unidadTrabajo.clienteRepo.Obtener(cli => cli.Telefono.ToLower() == request.CreateAccountDTO.ClienteCreateDto.Telefono.ToLower()) != null)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU02_CU10_CODIGO_TELEFONO_REPETIDO;
                    _dataBaseResponse.ControlMessage = Constantes.CU02_CU10_MENSAJE_TELEFONO_REPETIDO;
                }
                else if(await _unidadTrabajo.clienteRepo.Obtener(cli => cli.UserName.ToLower() == request.CreateAccountDTO.ClienteCreateDto.UserName.ToLower()) != null)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU02_CU10_CODIGO_USERNAME_REPETIDO;
                    _dataBaseResponse.ControlMessage = Constantes.CU02_CU10_MENSAJE_USERNAME_REPETIDO;
                }
                else
                {
                    Cliente cliente = _mapper.Map<Cliente>(request.CreateAccountDTO.ClienteCreateDto);
                    Direccion direccion = _mapper.Map<Direccion>(request.CreateAccountDTO.DireccionCreateDto);
                    Tarjeta tarjeta = _mapper.Map<Tarjeta>(request.CreateAccountDTO.TarjetaCreateDTO);

                    cliente = await _unidadTrabajo.clienteRepo.RegistrarRetornandoConID(cliente);
                    direccion.IdCliente = cliente.IdCliente;
                    tarjeta.IdCliente = cliente.IdCliente;

                    direccion = await _unidadTrabajo.direccionRepo.RegistrarRetornandoConID(direccion);
                    tarjeta = await _unidadTrabajo.tarjetaRepo.RegistrarRetornandoConID(tarjeta);

                    AccountResponseDTO accountResponse = new()
                    {
                        ClienteResponseDto = _mapper.Map<ClienteResponseDTO>(cliente),
                        DireccionResponseDto = _mapper.Map<DireccionResponseDTO>(direccion),
                        TarjetaResponseDTO = _mapper.Map<TarjetaResponseDTO>(tarjeta)
                    };

                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU02_CODIGO_CLIENTE_REGISTRADO_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU02_MENSAJE_CLIENTE_REGISTRADO_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = accountResponse;
                }
                return _dataBaseResponse;
            }
            catch (Exception ex)
            {
                _dataBaseResponse.isExitoso = false;
                _dataBaseResponse.ControlCode = Constantes.CODIGO_DATA_BASE_ERROR;
                _dataBaseResponse.ControlMessage = Constantes.MENSAJE_DATA_BASE_ERROR;
                _dataBaseResponse.ErrorMessages = new List<string>() { ex.ToString() };
                _dataBaseResponse.Resultado = null;
                return _dataBaseResponse;
            }
        }
    }
}
