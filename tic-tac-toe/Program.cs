using tic_tac_toe.Hubs;
using tic_tac_toe.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
        builder => {
            builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            //.AllowCredentials()
            .AllowAnyOrigin();
        });
});

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder => {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TicTacToeService>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.MapHub<TestHub>("/Test").RequireCors("any");



app.Run();
