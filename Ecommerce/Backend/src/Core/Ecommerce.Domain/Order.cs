using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain;

public class Order : BaseDomainModel {

    public Order() {}

    public Order(
        string compradorNombre,
        string compradorEmail,
        OrderAddress orderAddress,
        decimal subtotal,
        decimal total,
        decimal impuesto,
        decimal precioEnvio
    ){
        CompradorNombre = compradorNombre;
        CompradorUsername = compradorEmail;
        OrderAddress = orderAddress;
        Subtotal = subtotal;
        Total = total;
        Impuesto = impuesto;
        PrecioEnvio = precioEnvio;
    }

    public string? CompradorNombre { get; set; }

    public string? CompradorUsername {get;set;}

    public OrderAddress? OrderAddress { get; set; }

    public IReadOnlyList<OrderItem>? OrderItems {get;set;}

    [Column(TypeName ="decimal(10,2)")]
    public decimal Subtotal { get; set; }

    public OrderStatus Status {get;set;} = OrderStatus.Pending;

    [Column(TypeName ="decimal(10,2)")]
    public decimal Total {get;set;}

    [Column(TypeName ="decimal(10,2)")]
    public decimal Impuesto {get;set;}

    [Column(TypeName ="decimal(10,2)")]
    public decimal PrecioEnvio {get;set;}
   
    public string? PaymentIntentId {get;set;}

    public string? ClientSecret {get;set;}

    public string? StripeApiKey {get;set;}

    [Column(TypeName = "NVARCHAR(2000)")]
    public string? DescripcionIncidenteCliente { get; set; }
    
    [Column(TypeName = "NVARCHAR(2000)")]
    public string? DescripcionIncidenteRepartidor { get; set; }

    [Column(TypeName = "NVARCHAR(2000)")]
    public string? ImagenIncidenteClienteUrl { get; set; }

    [Column(TypeName = "NVARCHAR(2000)")]
    public string? ImagenIncidenteRepartidorUrl { get; set; }

    public DateTime? FechaEntrega { get; set; }
    public DateTime? FechaSolicitud { get; set; }
    public int? NumeroFolio { get; set; }
    public TimeSpan? TiempoEstimadoEntrega { get; set; }
    public TimeSpan? TiempoTardoEntrega { get; set; }
    public Client? Client { get; set; }   
    [Column("ClienteId")]
    public int? ClientId { get; set; }
    public Employee? Employee { get;}
    [Column("EmpleadoId")]
    public int? EmployeeId { get; set; }
}
