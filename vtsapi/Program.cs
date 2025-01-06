using vahangpsapi.Context;
using vahangpsapi.Interfaces;
using vahangpsapi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<JwtContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200", "http://localhost:9902", "http://103.89.44.59:9902", "http://10.10.44.59:9902", "http://localhost:7501", "http://103.109.7.173:7501", "http://localhost:7603", "http://103.109.7.173:7603")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IDealerService, DealerService>();

builder.Services.AddTransient<IVisitorService, VisitorService>();
builder.Services.AddTransient<ICommonService, CommonService>();
builder.Services.AddTransient<IDashboardService, DashboardService>();
builder.Services.AddTransient<IDeviceService, DeviceService>();
builder.Services.AddTransient<IIdProofTypeService, IdProofTypeService>();

builder.Services.AddTransient<IBackendService, BackendService>();
builder.Services.AddTransient<IStateService, StateService>();
builder.Services.AddTransient<IManagerService, ManagerService>();
builder.Services.AddTransient<IRTOService, RTOService>();

builder.Services.AddTransient<IManufacturerService ,ManufacturerService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddTransient<IAuthorityService, AuthorityService>();

builder.Services.AddTransient<IDeviceMasterService, DeviceMasterService>();

builder.Services.AddTransient<ICityService, CityService>();

builder.Services.AddTransient<ISimDataService, SimDataService>();

builder.Services.AddTransient<IDistributorService, DistributorService>();

builder.Services.AddTransient<ISalesManagerService, SalesManagerService>();




builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Uploads")),
    RequestPath = new PathString("/Uploads")
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
