using Microsoft.OpenApi.Models;
using System.Reflection;
using Transversal.Util.BaseDBUp;
using Ejemplo.Models;
using Ejemplo.DBUp;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
ConfigurationManager configuration = builder.Configuration; 
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region SWAGGER
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
#endregion

#region BASE DE DATOS
 string conString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
 builder.Services.AddSingleton<string>(conString);
 #endregion

#region MIGRATION
bool migration = GetBoolDefaultFalse("DbMigration");
bool migrationdata = GetBoolDefaultFalse("DbMigrationData");
bool migrationdevelop = GetBoolDefaultFalse("DbMigrationDevelopment");
bool migrationstoredprocedure = GetBoolDefaultFalse("DbMigrationStoredProcedure");
string pattern = GetPattern("DbMigrationPattern");
string path = String.Format("{0}/Scripts", Directory.GetCurrentDirectory());
string migrationout = "Migracion No ejecutada";
if (migration)
{
    DBUpMSMigration m = new DBUpMSMigration(
        ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection")
        , path
        , pattern
        , null
        , migrationdata
        , migrationdevelop
        , migrationstoredprocedure
        , DBUpMSMigration.DataBaseType.SqlServer);

    ResultMigration r = m.GenerateMigration();
    migrationout = String.Format("Migration DB: {0}: {1}", r.IsValid ? "Up" : "Error", r.Result);
}
#endregion

#region EJEMPLOS

List<AutorModel> autoresEjemplo = new List<AutorModel>
{
    new AutorModel(){Id=1, Nombre="Pablo Neruda"},
    new AutorModel(){Id=2, Nombre="Javier Rebolledo"},
    new AutorModel(){Id=3, Nombre="Gabriel García Márquez"}
};

List<LibroModel> librosEjemplo = new List<LibroModel>
{
    new LibroModel(){Id=1, IdAutor=1, IdTipo=1, Nombre="Canto General", Resumen="Proyecto poético monumental» que aborda la historia de América Latina",Year=1950},
    new LibroModel(){Id=2, IdAutor=2, IdTipo=2, Nombre="El despertar de los cuervos", Resumen="El miedo acecha en cada una de las páginas que se van dejando atrás. Dolorosos testimonios de violencia y muerte, pero también emotivas crónicas de sobrevivencia",Year=2013},
    new LibroModel(){Id=3, IdAutor=3, IdTipo=3, Nombre="El coronel no tiene quien le escriba", Resumen="El coronel es un veterano de la Guerra de los Mil Días que malvive en una casa de una villa en la costa atlántica colombiana junto a su esposa, la cual sufre de asma.",Year=1961}
};

List<TipoModel> tiposEjemplo = new List<TipoModel>
{
    new TipoModel(){Id=1, Nombre="Poesía"},
    new TipoModel(){Id=2, Nombre="Crónica periodística"},
    new TipoModel(){Id=3, Nombre="Novela"}
};

builder.Services.AddSingleton<List<AutorModel>>(autoresEjemplo);
builder.Services.AddSingleton<List<LibroModel>>(librosEjemplo);
builder.Services.AddSingleton<List<TipoModel>>(tiposEjemplo);

#endregion

var app = builder.Build();
app.Logger.LogInformation("Starting Application");
app.Logger.LogInformation(migrationout);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

#region SWAGGER NO SOLO PARA DESARROLLO - SOLO PARA EL RAMO
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#region Utiles
bool GetBoolDefaultFalse(string ConfigSection) => Boolean.TryParse(configuration.GetSection(ConfigSection).Value, out bool resp) ? Boolean.Parse(configuration.GetSection(ConfigSection).Value) : resp;
string GetPattern(string ConfigSection) => string.IsNullOrEmpty(configuration.GetSection(ConfigSection).Value.ToString()) ? "" : configuration.GetSection(ConfigSection).Value.ToString();
#endregion