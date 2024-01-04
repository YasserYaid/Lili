using ReposteriaLili_Front.Models.Mapping;
using ReposteriaLili_Front.Services;
using ReposteriaLili_Front.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddHttpClient<IReposteriaService, ReposteriaService>();

builder.Services.AddScoped<IReposteriaService, ReposteriaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Clientes}/{controller=Cliente}/{action=ListarProductos}/{id?}");

app.Run();
