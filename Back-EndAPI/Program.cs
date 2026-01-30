var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Dependency Injection Scope for Initial Setup
// Dependency Injection means: your class does NOT create the things it depends on
// — they are given to it instead.
// Create a temporary DI scope so scoped services can be resolved
using (var scope = app.Services.CreateScope())
{
    // Resolve IConfiguration from dependency injection
    // This gives access to appsettings.json, environment vars, secrets, etc.
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

    // Read the connection string named "DefaultConnection"
    var connString = config.GetConnectionString("DefaultConnection");

    // Create a PostgreSQL connection using the Npgsql provider
    // 'using var' ensures the connection is closed and disposed automatically
    using var conn = new Npgsql.NpgsqlConnection(connString);

    // Open the database connection
    conn.Open();

    // Simple confirmation that the connection succeeded
    Console.WriteLine("Connected to Postgres!");
}

app.UseCors();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.jason", "My API V1");
        c.RoutePrefix = string.Empty;
    
    });
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
