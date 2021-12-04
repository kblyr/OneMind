var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new() { Title = "OneMind", Version = "v1" });
    })
    .AddMediatR(typeof(Program));

var app = builder.Build();

app.UseHttpsRedirection();

app
    .UseSwagger()
    .UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "OneMind v1");
    });

app.MapUserEndpoints();

app.Run();