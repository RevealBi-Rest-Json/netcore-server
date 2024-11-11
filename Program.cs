using Reveal.Sdk;
using RevealSdk.Server.Reveal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddReveal(builder =>
{
    builder
        .AddAuthenticationProvider<AuthenticationProvider>()
        .AddDataSourceProvider<DataSourceProvider>()
        .AddUserContextProvider<UserContextProvider>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
      builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    );
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();