var builder = WebApplication.CreateBuilder(args);

// Thêm DbContext vào DI container
builder.Services.AddDbContext<FlightDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container
builder.Services.AddControllers();

// Thêm các dịch vụ cho các lớp business logic
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
 app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

