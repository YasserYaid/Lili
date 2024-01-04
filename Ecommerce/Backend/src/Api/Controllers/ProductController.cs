using System.Drawing;
using System.Net;
using BarcodeStandard;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Commands.DeleteProduct;
using Ecommerce.Application.Features.Products.Commands.UpdateProduct;
using Ecommerce.Application.Features.Products.Queries.GetProductById;
using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Application.Features.Products.Queries.PaginationProducts;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.ProductsClassic.Commands;
using Ecommerce.Application.Features.ProductsClassic.Commands.RegisterProduct;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain;
using Ecommerce.Infrastructure.ImageFirebase;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SkiaSharp;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private IMediator _mediator;
    private IManageImageService _manageImageService;
    private ManageImageFirebaseService _manageImageFirebaseService;


    public ProductController(IMediator mediator, IManageImageService manageImageService)
    {
        _mediator = mediator;
        _manageImageService = manageImageService;
        _manageImageFirebaseService = new ManageImageFirebaseService();
    }

    [AllowAnonymous]
    [HttpGet("list", Name = "GetProductList")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<ProductVm>>> GetProductList()
    {
        var query = new GetProductListQuery();
        var productos = await _mediator.Send(query);
       return Ok(productos);
    }

    [AllowAnonymous]
    [HttpGet("pagination", Name = "PaginationProduct")]
    [ProducesResponseType(typeof(PaginationVm<ProductVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<ProductVm>>> PaginationProduct(
        [FromQuery] PaginationProductsQuery paginationProductsQuery
    )
    {
        paginationProductsQuery.Status = ProductStatus.Activo;
        var paginationProduct = await _mediator.Send(paginationProductsQuery);
        return Ok(paginationProduct);
    }


    [Authorize(Roles = Role.ADMIN)]
    [HttpGet("paginationAdmin", Name = "PaginationProductAdmin")]
    [ProducesResponseType(typeof(PaginationVm<ProductVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<ProductVm>>> PaginationAdmin(
        [FromQuery] PaginationProductsQuery paginationProductsQuery
    )
    {
        var paginationProduct = await _mediator.Send(paginationProductsQuery);
        return Ok(paginationProduct);
    }

    
    [AllowAnonymous]
    [HttpGet("{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVm>> GetProductById(int id)
    {
        var query = new GetProductByIdQuery(id);
        return Ok(await _mediator.Send(query));
    }


    [Authorize(Roles = Role.ADMIN)]
    [HttpPost("create", Name = "CreateProduct")]
    [ProducesResponseType( (int)HttpStatusCode.OK )]
    public async Task<ActionResult<ProductVm>> CreateProduct([FromForm] CreateProductCommand request)
    {
        var listFotoUrls = new List<CreateProductImageCommand>();

        if(request.Fotos is not null)
        {
            foreach(var foto in request.Fotos)
            {
                var streamImage = foto.OpenReadStream();
                var name = foto.Name + Guid.NewGuid().ToString();
                var resultImage = await _manageImageFirebaseService.UploadImage(streamImage, name, "PRODUCT");

                var fotoCommand = new CreateProductImageCommand
                {
                    PublicCode = "a1",
                    Url = resultImage
                };

                listFotoUrls.Add(fotoCommand);
            }
            request.ImageUrls = listFotoUrls;
        }

        return await _mediator.Send(request);

    }


    [Authorize(Roles = Role.ADMIN)]
    [HttpPut("update", Name = "UpdateProduct")]
    [ProducesResponseType( (int)HttpStatusCode.OK )]
    public async Task<ActionResult<ProductVm>> UpdateProduct([FromForm] UpdateProductCommand request)
    {
        var listFotoUrls = new List<CreateProductImageCommand>();

        if(request.Fotos is not null)
        {
            foreach(var foto in request.Fotos)
            {
                var streamImage = foto.OpenReadStream();
                var name = foto.Name + Guid.NewGuid().ToString();
                var resultImage = await _manageImageFirebaseService.UploadImage(streamImage, name, "PRODUCT");

                var fotoCommand = new CreateProductImageCommand
                {
                    PublicCode = "a1",
                    Url = resultImage
                };

                listFotoUrls.Add(fotoCommand);
            }
            request.ImageUrls = listFotoUrls;
        }

        return await _mediator.Send(request);

    }




    [Authorize(Roles = Role.ADMIN)]
    [HttpDelete("status/{id}", Name = "UpdateStatusProduct")]
    [ProducesResponseType( (int)HttpStatusCode.OK )]
    public async Task<ActionResult<ProductVm>> UpdateStatusProduct(int id)
    {
        var request = new DeleteProductCommand(id);
        return await _mediator.Send(request);
    }



    [AllowAnonymous]
    [HttpPost("register", Name = "RegisterProduct")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductoVM>> RegisterProduct([FromForm] RegisterProductCommand request)
    {

        if (request.Foto is not null)
        {
            //long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            DateTimeOffset timeNow = (DateTimeOffset)DateTime.UtcNow;
            var nameProductUrl = "Product-" + request.Nombre + timeNow.ToUnixTimeMilliseconds().ToString();
            request.CodigoBarras = request.Nombre + timeNow.ToUnixTimeMilliseconds().ToString();
            var nameBarcodeUrl = "Barcode-" + request.CodigoBarras;

            var streamImage = request.Foto.OpenReadStream();
            request.ImagenProductoFirebaseUrl = await _manageImageFirebaseService.UploadImage(streamImage, nameProductUrl, "PRODUCT");            

            Barcode barcodeData = new Barcode();
            barcodeData.IncludeLabel = true;
            barcodeData.Alignment = AlignmentPositions.Center;
            var barcodeDataImage = barcodeData.Encode(BarcodeStandard.Type.Code128, request.CodigoBarras, 1200, 600);

            var barcodeDataStream = barcodeDataImage.Encode(SKEncodedImageFormat.Png, 100).AsStream();                        
            request.ImagenCodigoBarrasUrl = await _manageImageFirebaseService.UploadImage(barcodeDataStream, nameBarcodeUrl, "BARCODE");

            return await _mediator.Send(request);
        }

        else 
            return BadRequest("NO SE PUEDE REGISTRAR UN PRODUCTO SIN IMAGEN");
    }

}