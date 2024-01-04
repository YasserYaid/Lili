namespace Ecommerce.Application.Features.ShoppingCarts.Vms;

public class ShoppingCartVm
{
    public string? ShoppingCartId {get;set;}

    public List<ShoppingCartItemVm>? ShoppingCartItems {get;set;}

    public decimal Total {
        get {
            return

                    Math.Round(ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad));

        }


        set {}

    }


    public int Cantidad 
    {
        get {  return ShoppingCartItems!.Sum(x => x.Cantidad); }
        set {}
    }

    public decimal SubTotal
    {
        get { return Math.Round( ShoppingCartItems!.Sum(x => x.Precio * x.Cantidad)); }
    }

    public decimal Impuesto
    {
        get { 
            return 0; 
            }
        set{}
    }   

    public decimal PrecioEnvio
    {
        get{
             return 0;
        }

        set{}
    }   

}