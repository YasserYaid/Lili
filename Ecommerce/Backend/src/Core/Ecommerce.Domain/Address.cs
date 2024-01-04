using Ecommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain;

public class Address : BaseDomainModel {
    
    public string? Direccion { get; set; }
    public string? Ciudad { get; set; }
    public string? Departamento { get; set; }
    public string? CodigoPostal { get; set; }
    public string? Colonia { get; set; }
    public string? Estado {  get; set; }
    public string? Latitud { get; set; }
    public string? Longitud { get; set; }
    public string? Municipio { get; set; }
    public string? Username {get;set;}   
    public string? Pais { get; set; }
    public Client? Client { get; set; }
    [Column("ClienId")]
    public int? ClientId { get; set; }
}