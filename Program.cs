using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Liaison base de données
builder.Services.AddDbContext<ApplicationDbContext>
    (options =>options.UseNpgsql(builder.Configuration.GetConnectionString("ApplicationDbContext")));

//Ajouter repository
// Récupérer l'assembly de l'application
var assembly = Assembly.GetExecutingAssembly();

// Récupérer tous les types de classe non abstraits dans l'assembly
var serviceTypes = assembly.GetTypes()
    .Where(type => type.IsClass && !type.IsAbstract && !type.IsGenericTypeDefinition);

// Enregistrer tous les services trouvés dans le conteneur DI
foreach (var serviceType in serviceTypes)
{
    // Ici, vous pouvez spécifier la portée du service selon vos besoins (Scoped, Transient, Singleton)
    builder.Services.AddScoped(serviceType);
}



//builder.Services.AddScoped<Import>();
//builder.Services.AddTransient<Import>(); // Utilisez builder.Services au lieu de services


// Configure the HTTP request pipeline.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MadaImmo.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
