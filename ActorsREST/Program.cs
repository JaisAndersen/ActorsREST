using ActorRepositoryLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy =>
        {
            policy.WithOrigins("https://google.dk");
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
        });
});

builder.Services.AddControllers();

builder.Services.AddSingleton<ActorsRepositoryList>(new ActorsRepositoryList());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
