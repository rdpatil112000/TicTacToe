using TicTacToe.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<
    IGameService,
    GameService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("react",
        p =>
        {
            p.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      
    app.UseSwaggerUI();    
    Console.WriteLine("Development");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseCors("react");

app.MapControllers();

app.Run();
