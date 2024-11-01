
using FamilySchedule.Models.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Agregar soporte para sesi�n
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Establecer el tiempo de expiraci�n de la sesi�n
    options.Cookie.HttpOnly = true; // Asegura que la cookie solo sea accesible por HTTP
    options.Cookie.IsEssential = true; // Es esencial para que la sesi�n funcione correctamente
});

// Configurar la conexi�n a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});

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

// Habilitar el uso de la sesi�n antes de la autorizaci�n
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Evento}/{action=Index}/{id?}");

app.Run();