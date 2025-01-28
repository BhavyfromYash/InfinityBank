// global using BankingSystem.Repository;
// global using BankingSystem.Services;

// var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddHttpContextAccessor();
// builder.Services.AddControllers();
// builder.Services.AddDistributedMemoryCache();
// builder.Services.AddSession(options =>
// {
//     options.IdleTimeout = TimeSpan.FromMinutes(30);
//     options.Cookie.HttpOnly = true;
//     options.Cookie.IsEssential = true;
// });
// builder.Services.AddScoped<IUserService, UserRepository>();
// builder.Services.AddScoped<IAccountService, AccountRepository>();
// builder.Services.AddScoped<IManagerService, ManagerRepository>();
// builder.Services.AddScoped<ICustomerService, CustomerRepository>();
// builder.Services.AddScoped<IFundTransferService, FundTransferRepository>();

// builder.Services.AddCors(setup =>
// {
//     setup.AddPolicy(
//         "default",
//         options =>
//         {
//             options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().AllowCredentials();
//         }
//     );
// });

// // Add services to the container.
// builder.Services.AddControllersWithViews();
// var connectionString =
//     builder.Configuration.GetConnectionString("MyConstr")
//     ?? throw new InvalidOperationException("Connection string 'AppDbContext Connection' not found");
// builder.Services.AddDbContext<BankDbContext>(options => options.UseSqlServer(connectionString));

// //Add the services to authenticate a user using tokens.. This code is to be written
// //for the application that authenticates/authorizes a request. Not for Issuing a token
// builder
//     .Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(c =>
//     {
//         c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//         {
//             ValidateAudience = true,
//             ValidateIssuer = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration.GetValue<string>("Jwt:issuer"),
//             ValidAudience = builder.Configuration.GetValue<string>("Jwt:audience"),
//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:secret"))
//             ),
//         };
//     });

// // Add services to the container.

// builder.Services.AddControllers();

// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseCors(options =>
//     options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials()
// );
// app.UseHttpsRedirection();

// app.UseRouting();

// app.UseSession();

// app.UseAuthentication();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();



global using BankingSystem.Repository;
global using BankingSystem.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IUserService, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountRepository>();
builder.Services.AddScoped<IManagerService, ManagerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerRepository>();
builder.Services.AddScoped<IFundTransferService, FundTransferRepository>();
// builder.Services.AddScoped<IEmailService, EmailRepository>();
// Configure CORS
builder.Services.AddCors(setup =>
{
    setup.AddPolicy(
        "AllowOrigin",
        options =>
        {
            options.WithOrigins("http://localhost:4200")  // Replace with your actual frontend URL
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        }
    );
});

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString =
    builder.Configuration.GetConnectionString("MyConstr")
    ?? throw new InvalidOperationException("Connection string 'AppDbContext Connection' not found");
builder.Services.AddDbContext<BankDbContext>(options => options.UseSqlServer(connectionString));

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(c =>
    {
        c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Jwt:issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Jwt:audience"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:secret"))
            ),
        };
    });

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin"); // Apply the CORS policy

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
