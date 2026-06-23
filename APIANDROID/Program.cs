using APIANDROID_DATA;
using APIANDROID_DATA.Services;
using DbConnection = APIANDROID_DATA.DbConnection;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DbConnection>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build(); // <-- Build siempre al final de los services

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
