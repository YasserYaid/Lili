using AutoMapper;
using ReposteriaLili_API.Core.Application.DTO;
using ReposteriaLili_API.Core.Application.DTO.ClientesDTOs;
using ReposteriaLili_API.Core.Application.DTO.DireccionesDTOs;
using ReposteriaLili_API.Core.Application.DTO.EmpleadosDTOs;
using ReposteriaLili_API.Core.Application.DTO.OrdenesDTOs;
using ReposteriaLili_API.Core.Application.DTO.ProductosDTOs;
using ReposteriaLili_API.Core.Application.DTO.SucursalesDTOs;
using ReposteriaLili_API.Core.Application.DTO.TarjetasDTOs;
using ReposteriaLili_API.Core.Application.ReqResModels;
using ReposteriaLili_API.Core.Dominio;
using ReposteriaLili_API.Core.Dominio.JoinViewModels;

namespace ReposteriaLili_API.Core.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Empleado, EmpleadoResponseDTO>();//Origen >>> Destino (de Empleado a EmpleadoResponseDTO)
            CreateMap<EmpleadoResponseDTO, Empleado>();

            CreateMap<Empleado, EmpleadoCreateDTO>().ReverseMap();//Origen >>> Destino y con reversemap se indica viceversa y Destino >>> Origen
            CreateMap<Cliente, ClienteCreateDTO>().ReverseMap();
            CreateMap<Direccion, DireccionCreateDTO>().ReverseMap();
            CreateMap<Tarjeta, TarjetaCreateDTO>().ReverseMap();
            CreateMap<Sucursal, SucursalCreateDTO>().ReverseMap();
            CreateMap<Sucursal, SucursalResponseDTO>().ReverseMap();
            CreateMap<Cliente, ClienteResponseDTO>().ReverseMap();
            CreateMap<Tarjeta, TarjetaResponseDTO>().ReverseMap();
            CreateMap<Direccion, DireccionResponseDTO>().ReverseMap();
            CreateMap<Orden, OrdenResponseDTO>().ReverseMap();
            CreateMap<Producto, ProductoCreateDTO>().ReverseMap();
            CreateMap<Producto, ProductoResponseDTO>().ReverseMap();
            CreateMap<Orden, OrdenUpdateDTO>().ReverseMap();


            CreateMap<DatabaseResponse, APIResponse>();
            CreateMap<ClienteJVM, ClienteResponseDTO>();
            CreateMap<ClienteJVM, TarjetaResponseDTO>();
            CreateMap<ClienteJVM, DireccionResponseDTO>();
            CreateMap<ProductoJVM, ProductoResponseDTO>();
            CreateMap<ProductoJVM, SucursalResponseDTO>();
            CreateMap<OrdenClienteJVM, CustomerOrderProductResponseDTO>();
            CreateMap<OrdenEmpleadoJVM, DeliveryManOrderResponseDTO>();


        }
    }
}
