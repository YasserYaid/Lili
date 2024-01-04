using AutoMapper;
using MediatR;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.DTO.EmpleadosDTOs;
using Microsoft.IdentityModel.Tokens;
using ReposteriaLili_API.Core.Application.DTO.ClientesDTOs;
using ReposteriaLili_API.Core.Application.DTO.DireccionesDTOs;
using ReposteriaLili_API.Core.Application.DTO.TarjetasDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.EmpleadosCQRS.Querys.Login
{
    public class LoginEmployeeQueryHandler : IRequestHandler<LoginEmployeeQuery, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;
        private EmpleadoResponseDTO empleadoResponsedto;
        private LoginEmpleadoResponseDTO loginResponseDTO;


        public LoginEmployeeQueryHandler(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(LoginEmployeeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();

                Empleado? empleadoInfo = _unidadTrabajo.empleadoRepo.ObtenerTodos().Result.FirstOrDefault(
                    emp => emp.UserName.ToLower() == request.LoginEmpleadoDTO.UserName.ToLower() && emp.Contrasena.ToLower() == request.LoginEmpleadoDTO.Password.ToLower());


                if (empleadoInfo != null)
                {
                    MapearInformacion(empleadoInfo);
                    loginResponseDTO = GenerarRespuesta(GenerarToken(request.secretKey, empleadoResponsedto.IdEmpleado));
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

        private void MapearInformacion(Empleado? empleado)
        {
            empleadoResponsedto = _mapper.Map<EmpleadoResponseDTO>(empleado);
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

        private LoginEmpleadoResponseDTO? GenerarRespuesta(string token)
        {
            LoginEmpleadoResponseDTO loginResponse = null;

            if (empleadoResponsedto != null)
            {
                loginResponse = new LoginEmpleadoResponseDTO()
                {
                    EmpleadoResponseDTO = empleadoResponsedto,
                    Token = token
                };
            }
            return loginResponse;
        }

    }
}
