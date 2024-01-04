using AutoMapper;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.BranchesClassic;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.ProductsClassic.Commands.RegisterProduct
{
    public class RegisterProductCommandHandler : IRequestHandler<RegisterProductCommand, ProductoVM>
    {
        private readonly IClassicUnitOfWork _classicUnitOfWork;
        private readonly IMapper _mapper;

        public RegisterProductCommandHandler (IClassicUnitOfWork classicUnitOfWork, IMapper mapper)
        {
            _classicUnitOfWork = classicUnitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductoVM> Handle(RegisterProductCommand request, CancellationToken cancellationToken)
        {
            bool isImagesRegister = false;
            var productToRegister = _mapper.Map<Product>(request);
            productToRegister.Rating = 5;
            productToRegister.Status = ProductStatus.Activo;
            var productRegistered = await _classicUnitOfWork.productClassicRepository.AddAsync(productToRegister);
            Console.WriteLine("El id del producto es : " + productRegistered.Id);
            if (productRegistered.Id > 0)
                isImagesRegister = await _classicUnitOfWork.productClassicRepository.RegisterImages(productRegistered);
            else
                throw new DatabaseException("EL PRODUCTO NO PUDO SER REGISTRADO");
            if (isImagesRegister)
                return _mapper.Map<ProductoVM>(productRegistered);
            else
                throw new DatabaseException("LAS IMAGENES NO PUDIERON SER REGISTRADAS");
        }
    }
}
