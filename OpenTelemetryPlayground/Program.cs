using OpenTelemetryPlayground;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfigureOpenTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Default SSL cert is issued to 'localhost' which conflicts with docker internal name 'opentelemetryplayground'.
// This missmatch causes SSL error when prometheus connects to the target, so I turn it off for now
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();