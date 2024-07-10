using EgwarancjeAPI;
using EgwarancjeAPI.Services.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMessagesService, MessagesService>();

builder.Services.AddCors(options => options.AddPolicy(name: "All",
    policy =>
    {
        policy.WithOrigins("http://0.0.0.0:5190", "https://0.0.0.0:7007")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
    }));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LocalDatabaseContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.EnableTryItOutByDefault();
    });
}

app.UseCors("All");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
