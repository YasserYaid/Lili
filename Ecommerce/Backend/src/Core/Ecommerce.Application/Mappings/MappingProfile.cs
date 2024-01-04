using AutoMapper;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.BranchesClassic;
using Ecommerce.Application.Features.BranchesClassic.Commands;
using Ecommerce.Application.Features.Categories.Vms;
using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Features.CountryStates.Vms;
using Ecommerce.Application.Features.Images.Queries.Vms;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Commands.UpdateProduct;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.ProductsClassic.Commands;
using Ecommerce.Application.Features.ProductsClassic.Commands.RegisterProduct;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Features.ShoppingCarts.Vms;
using Ecommerce.Domain;

namespace Ecommerce.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Origen : DOMAIN - Destino : ViewModel
        CreateMap<Image, ImageVm>();
        CreateMap<Review, ReviewVm>();
        CreateMap<Country, CountryVm>();
        CreateMap<State, CountryStatesVm>();
        CreateMap<Category, CategoryVm>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<CreateProductImageCommand, Image>();
        CreateMap<UpdateProductCommand, Product>();
        CreateMap<ShoppingCartItem, ShoppingCartItemVm>();
        CreateMap<ShoppingCartItemVm, ShoppingCartItem>();
        CreateMap<Address, AddressVm>();
        CreateMap<Order, OrderVm>();
        CreateMap<OrderItem, OrderItemVm>();
        CreateMap<OrderAddress, AddressVm>();
        CreateMap<Branch, BranchVM>();
        CreateMap<Product, ProductoVM>();

        CreateMap<Product, ProductVm>()
            .ForMember(p => p.CategoryNombre, x => x.MapFrom(a => a.Category!.Nombre))
            .ForMember(p => p.NumeroReviews, x => x.MapFrom(a => a.Reviews == null ? 0 : a.Reviews.Count));

        CreateMap<ShoppingCart, ShoppingCartVm>()
            .ForMember(p => p.ShoppingCartId, x => x.MapFrom(a => a.ShoppingCartMasterId));

        //Extras probando
        //Origen : ViewModel - Destino : Domain
        CreateMap<BranchVM, Branch>();
        //Origen : MediatR request - Destino : Domain
        CreateMap<RegisterBranchCommand, Branch>();
        CreateMap<RegisterProductCommand, Product>();


        CreateMap<RegisterProductCommand, ProductoVM>();


    }
}