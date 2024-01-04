using AutoMapper;
using ReposteriaLili_Front.Models.Dominio;
using ReposteriaLili_Front.Models.Dominio.JoinViewModels;
using ReposteriaLili_Front.Models.DTO;
using ReposteriaLili_Front.Models.DTO.ClientesDTOs;
using ReposteriaLili_Front.Models.DTO.DireccionesDTOs;
using ReposteriaLili_Front.Models.DTO.EmpleadosDTOs;
using ReposteriaLili_Front.Models.DTO.OrdenesDTOs;
using ReposteriaLili_Front.Models.DTO.ProductosDTOs;
using ReposteriaLili_Front.Models.DTO.SucursalesDTOs;
using ReposteriaLili_Front.Models.DTO.TarjetasDTOs;
using ReposteriaLili_Front.Models.ReqResModels;
using ReposteriaLili_Front.Models.ViewModels;

namespace ReposteriaLili_Front.Models.Mapping
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
            
            CreateMap<OrdenResponseDTO, OrdenUpdateDTO>().ReverseMap();

            CreateMap<ClienteJVM, ClienteResponseDTO>();
            CreateMap<ClienteJVM, TarjetaResponseDTO>();
            CreateMap<ClienteJVM, DireccionResponseDTO>();
            CreateMap<ProductoJVM, ProductoResponseDTO>();
            CreateMap<ProductoJVM, SucursalResponseDTO>();
            CreateMap<OrdenClienteJVM, CustomerOrderProductResponseDTO>();
            CreateMap<OrdenEmpleadoJVM, DeliveryManOrderResponseDTO>();

            CreateMap<SucursalResponseDTO, SucursalViewModel>();

            CreateMap<SucursalViewModel, SucursalCreateDTO>();


        }
    }
}
