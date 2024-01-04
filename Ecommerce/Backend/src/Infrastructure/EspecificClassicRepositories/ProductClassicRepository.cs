using Ecommerce.Application.Persistence.EspecificClassicRepository;
using Ecommerce.Domain;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.EspecificClassicRepositories
{
    public class ProductClassicRepository : ClassicRepositoryBase<Product>, IProductClassicRepository
    {
        protected readonly EcommerceDbContext _context;
        public ProductClassicRepository(EcommerceDbContext context) : base(context)
        {
            _context = context;
        }
        ///Aqui va los contratos de los metodos especificos personalizados
        public async Task<bool> RegisterImages(Product product)
        {
            bool isInserted = false;
            //1)La query
            string sqlQuery = "INSERT INTO Images(Url, ProductId, CreatedDate, CreatedBy) " +
                              "VALUES (@Url, @ProductId, @CreatedDate, @CreatedBy);";
            //2)Los parametros
            SqlParameter[] parametersFirebase = new SqlParameter[] {
                new SqlParameter("@Url", product.ImagenProductoFirebaseUrl),
                new SqlParameter("@ProductId", product.Id),
                new SqlParameter("@CreatedDate", DateTime.UtcNow),
                new SqlParameter("@CreatedBy", "system")
            };

            SqlParameter[] parametersBarcode = new SqlParameter[] {
                new SqlParameter("@Url", product.ImagenCodigoBarrasUrl),
                new SqlParameter("@ProductId", product.Id),
                new SqlParameter("@CreatedDate", DateTime.UtcNow),
                new SqlParameter("@CreatedBy", "system")
            };

            // 3)La ejecucion con el resultado
            var affectedRows = await _context.Database.ExecuteSqlRawAsync(sqlQuery, parametersFirebase);
            if (affectedRows > 0)
            {
                var affectedRows2 = await _context.Database.ExecuteSqlRawAsync(sqlQuery, parametersBarcode);
                if (affectedRows2 > 0)
                {
                    isInserted = true;
                }
            }
            return isInserted;

        }

        public EcommerceDbContext EcommerceDbContext
        {
            get { return _context as EcommerceDbContext; }
        }


        /*var parameterId = new SqlParameter
        {
            ParameterName = "@carId",
            SqlDbType = SqlDbType.Int,
            Value = id,
        };

        var parameterName = new SqlParameter
        {
            ParameterName = "@petName",
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Output
        };

        _ = Context.Database
            .ExecuteSqlRaw("EXEC [dbo].[GetPetName] @carId, @petName OUTPUT",parameterId, parameterName);
            return (string) parameterName.Value;


          //2)Los parametros
            var urlParameterImageFirebase = new SqlParameter
            {
                ParameterName = "@Url",
                SqlDbType = SqlDbType.NVarChar,
                Size = 4000,
                Value = product.ImagenProductoFirebaseUrl
            };

            var urlParameterImageBarcode = new SqlParameter
            {
                ParameterName = "@Url",
                SqlDbType = SqlDbType.NVarChar,
                Size = 4000,
                Value = product.ImagenCodigoBarrasUrl
            };

            var parameterId = new SqlParameter
            {
                ParameterName = "@ProductId",
                SqlDbType = SqlDbType.Int,
                Value = product.Id
            };

            var parameterCreateDate = new SqlParameter
            {
                ParameterName = "@CreatedDate",
                SqlDbType = SqlDbType.DateTime,
                Value = DateTime.UtcNow
            };

            var parameterCreatedBy = new SqlParameter
            {
                ParameterName = "@CreatedBy",
                SqlDbType = SqlDbType.NVarChar,
                Size = -1,
                Value = "system"
            };

         */

    }
}
