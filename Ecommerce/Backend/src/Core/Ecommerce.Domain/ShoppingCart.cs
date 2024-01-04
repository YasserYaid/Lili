using Ecommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain;

public class ShoppingCart : BaseDomainModel {

    public Guid? ShoppingCartMasterId {get;set;}

    public virtual ICollection<ShoppingCartItem>? ShoppingCartItems {get;set;}
    public Client? Client { get; set; }
    [Column("ClienId")]
    public int? ClientId { get; set; }
}