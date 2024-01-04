using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.DTO.ClientesDTOs;
using ReposteriaLili_API.Core.Application.DTO.DireccionesDTOs;
using ReposteriaLili_API.Core.Application.DTO.TarjetasDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.ClientesCQRS.Querys.Login
{
    public class LoginClienteQueryHandler : IRequestHandler<LoginClienteQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;
        private ClienteResponseDTO cliente;
        private TarjetaResponseDTO tarjeta;
        private DireccionResponseDTO direccion;
        private LoginClienteResponseDTO loginResponseDTO;

        public LoginClienteQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(LoginClienteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();

                ClienteJVM? clienteFoundInfo = _unidadTrabajo.viewSesionClienteRepo.ObtenerTodos().Result.FirstOrDefault(
                    cli => cli.Telefono.ToLower() == request.LoginClienteDTO.Telefono.ToLower() && cli.Contrasena.ToLower() == request.LoginClienteDTO.Password.ToLower());


                if (clienteFoundInfo != null)
                {
                    MapearInformacion(clienteFoundInfo);
                    loginResponseDTO = GenerarRespuesta(GenerarToken(request.secretKey, cliente.IdCliente));
                }

                if (loginResponseDTO != null)
                {
                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU01_CODIGO_LOGIN_SATISFATORIO;
                    _dataBaseResponse.ControlMessage = Constantes.CU01_MENSAJE_LOGIN_SATISFATORIO;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = loginResponseDTO;
                }
                else
                {
                    _dataBaseResponse.isExitoso = false;
                    _dataBaseResponse.ControlCode = Constantes.CU01_CODIGO_LOGIN_FALLIDO;
                    _dataBaseResponse.ControlMessage = Constantes.CU01_MENSAJE_LOGIN_FALLIDO;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = null;
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

        private void MapearInformacion(ClienteJVM? informacion)
        {
            cliente = _mapper.Map<ClienteResponseDTO>(informacion);
            tarjeta = _mapper.Map<TarjetaResponseDTO>(informacion);
            direccion = _mapper.Map<DireccionResponseDTO>(informacion);
        }

        private static string GenerarToken(string secretKey, int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString()),
                    new Claim(ClaimTypes.Role, "Cliente")
                    //                    new Claim(ClaimTypes.NameIdentifier, cliente.IdClientte.ToString())
                    //                    new Claim(ClaimTypes.OtherPhone, cliente.Telefono),
                    //                    new Claim(ClaimTypes.Email, cliente.Correo)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private LoginClienteResponseDTO? GenerarRespuesta(string token)
        {
            LoginClienteResponseDTO loginResponse = null;

            if (cliente != null && tarjeta != null && direccion != null)
            {
                loginResponse = new LoginClienteResponseDTO()
                {
                    ClienteResponseDTO = cliente,
                    TarjetaResponseDTO = tarjeta,
                    DireccionResponseDTO = direccion,
                    Token = token
                };
            }
            return loginResponse;
        }

    }
}
