using Ecommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Persistence.EspecificClassicRepository
{
    public interface IProductClassicRepository : IClassicRepository<Product>
    {
        ///Aqui va los contratos de los metodos especificos personalizados
        Task<bool> RegisterImages(Product product);
    }
}
