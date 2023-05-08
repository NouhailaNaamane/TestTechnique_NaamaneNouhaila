using AutoMapper;
using Business.Interfaces.Admin;
using Business.Interfaces.EmailInterfaces;
using Business.Interfaces.Identity;
using Business.Interfaces.TemplateInterface;
using Business.Services.Admin;
using Business.Services.AutoMapperProfiles;
using Business.Services.EmailServices;
using Business.Services.Identity;
using Business.Services.TemplateService;
using CvThèque.Extensions;
using Data.Access.Layer.Models;
using Data.Access.Layer.Repositories.Admin;
using Data.Access.Layer.Repositories.Candidature;
using Data.Access.Layer.Repositories.Offer;
using Data.Access.Layer.UnitOfWorks;
using Data.Transfer.Object.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add The DbContext with the connection string
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"))
);

//Add ASP.NET core Identity configuration
builder.Services.AddIdentity<Admin, IdentityRole<Guid>>((option) => {
    option.SignIn.RequireConfirmedEmail = false;
    option.User.RequireUniqueEmail = true;
    option.Password.RequiredLength = 8;
    option.Password.RequireUppercase = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireDigit = true;
    option.Password.RequireNonAlphanumeric = true;
    })
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

// Read app settings configuration from appSettings.json
builder.Configuration.AddJsonFile("appsettings.json");

//Configure the Authentication and JWT token expiration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Set the JWT options
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Jwt:Issuer"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Key")!))
        };
    });

// Register the DefaultSuperAdminConfiguration with the configuration system
builder.Services.Configure<DefaultUserConfiguration>(builder.Configuration.GetSection("DefaultUser"));

//Repositories DI
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<ICandidatureRepository, CandidatureRepository>();

//Unit of works DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services DI
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IAdminService, AdminService>();


builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);



var app = builder.Build();

// Set the default user and role
await app.ConfigureDefaultUserAndRoles();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
