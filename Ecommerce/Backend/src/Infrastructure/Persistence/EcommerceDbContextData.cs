using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ecommerce.Infrastructure.Persistence;


public class EcommerceDbContextData
{
    public static async Task LoadDataAsync(
        EcommerceDbContext context,
        UserManager<Usuario> usuarioManager,
        RoleManager<IdentityRole> roleManager,
        ILoggerFactory loggerFactory
    )
    {
        try
        {
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                await roleManager.CreateAsync(new IdentityRole(Role.USER));
                await roleManager.CreateAsync(new IdentityRole(Role.DELIVERYMAN));
                await roleManager.CreateAsync(new IdentityRole(Role.SALESMANAGER));
            }

            if (!usuarioManager.Users.Any())
            {
                var usuarioAdmin = new Usuario
                {
                    Nombre = "Carmen",
                    Apellido = "Perez",
                    Email = "CarmenPerez@gmail.com",
                    UserName = "CarmenPerez",
                    Telefono = "2282103356",
                    AvatarUrl = "https://tecolotito.elsiglodetorreon.com.mx/cdn-cgi/image/format=auto,width=1024/i/2023/03/1665686.jpeg",
                };
                await usuarioManager.CreateAsync(usuarioAdmin, "*P0ll1t0*");
                await usuarioManager.AddToRoleAsync(usuarioAdmin, Role.ADMIN);

                var usuario = new Usuario
                {
                    Nombre = "Lili",
                    Apellido = "Perez",
                    Email = "LiliPerez@gmail.com",
                    UserName = "LiliPerez",
                    Telefono = "1234567890",
                    AvatarUrl = "https://ca-times.brightspotcdn.com/dims4/default/08ab82a/2147483647/strip/true/crop/2836x2000+0+0/resize/1200x846!/format/webp/quality/75/?url=https%3A%2F%2Fcalifornia-times-brightspot.s3.amazonaws.com%2Fdc%2F61%2Fc19d543f43a8b6634e43ae19d377%2Ffilm-superman-11403.jpg",
                };
                await usuarioManager.CreateAsync(usuario, "*L1l1*");
                await usuarioManager.AddToRoleAsync(usuario, Role.USER);

            }

            if(!context.Categories!.Any())
            {
                var categoryData = File.ReadAllText("../Infrastructure/Data/category.json");
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                await context.Categories!.AddRangeAsync(categories!);
                await context.SaveChangesAsync();
            }

            if (!context.Products!.Any())
            {
                var productData = File.ReadAllText("../Infrastructure/Data/product.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productData);
                await context.Products!.AddRangeAsync(products!);
                await context.SaveChangesAsync();
            }

            if (!context.Images!.Any())
            {
                var imageData = File.ReadAllText("../Infrastructure/Data/image.json");
                var imagenes = JsonConvert.DeserializeObject<List<Image>>(imageData);
                await context.Images!.AddRangeAsync(imagenes!);
                await context.SaveChangesAsync();
            }

            if (!context.Reviews!.Any())
            {
                var reviewData = File.ReadAllText("../Infrastructure/Data/review.json");
                var reviews = JsonConvert.DeserializeObject<List<Review>>(reviewData);
                await context.Reviews!.AddRangeAsync(reviews!);
                await context.SaveChangesAsync();
            }


            if (!context.Countries!.Any())
            {
                var countryData = File.ReadAllText("../Infrastructure/Data/countries.json");
                var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);
                await context.Countries!.AddRangeAsync(countries!);
                await context.SaveChangesAsync();
            }


            if (!context.StatesByConutry!.Any())
            {
                var statesData = File.ReadAllText("../Infrastructure/Data/countryStates.json");
                var states = JsonConvert.DeserializeObject<List<State>>(statesData);
                await context.StatesByConutry!.AddRangeAsync(states!);
                await context.SaveChangesAsync();
            }

            if (!context.Branches!.Any())
            {
                var branchesData = File.ReadAllText("../Infrastructure/Data/branch.json");
                var branches = JsonConvert.DeserializeObject<List<Branch>>(branchesData);
                await context.Branches!.AddRangeAsync(branches!);
                await context.SaveChangesAsync();
            }
        }
        catch(Exception e)
        {
            var logger = loggerFactory.CreateLogger<EcommerceDbContextData>();
            logger.LogError(e.Message);
        }

    }
    
}
