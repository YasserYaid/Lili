using AutoMapper;
using MediatR;
using ReposteriaLili_API.Core.Application.Contratos.Internal.IRepositorios;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio.Constantes;
using ReposteriaLili_API.Core.Dominio;

namespace ReposteriaLili_API.Core.Application.BusinessLogic.SucursalesCQRS.Commands.CreateBranch
{
    public class CreateBranchCommandHanlder : IRequestHandler<CreateBranchCommand, DatabaseResponse>
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        protected DatabaseResponse _dataBaseResponse;

        public CreateBranchCommandHanlder(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _dataBaseResponse = new DatabaseResponse();
        }

        public async Task<DatabaseResponse> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _dataBaseResponse.LimpiarValores();
                if (await _unidadTrabajo.sucursalRepo.Obtener(suc => suc.NombreComercial.ToLower() == request.CreateSucursalDTO.NombreComercial.ToLower()) != null)
                {
                    _dataBaseResponse.ControlCode = Constantes.CU11_CODIGO_NOMBRE_COMERCIAL_REPETIDO;
                    _dataBaseResponse.ControlMessage = Constantes.CU11_MENSAJE_NOMBRE_COMERCIAL_REPETIDO;
                }
                else
                {
                    Sucursal sucursal = _mapper.Map<Sucursal>(request.CreateSucursalDTO);
                    await _unidadTrabajo.sucursalRepo.Registrar(sucursal);
                    _dataBaseResponse.isExitoso = true;
                    _dataBaseResponse.ControlCode = Constantes.CU11_CODIGO_SUCURSAL_REGISTRADA_SATISFATORIAMENTE;
                    _dataBaseResponse.ControlMessage = Constantes.CU11_MENSAJE_SUCURSAL_REGISTRADA_SATISFATORIAMENTE;
                    _dataBaseResponse.ErrorMessages = null;
                    _dataBaseResponse.Resultado = sucursal;
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
