using System.Text;
using BankApplication.Infrastructure.AuthService.EmailService;
using BankApplication.Infrastructure.AuthService.JwtTokenService;
using BankApplication.Infrastructure.AuthService.UserControlService;
using BankApplication.Infrastructure.CardService;
using BankApplication.Infrastructure.ImageService;
using BankApplication.Infrastructure.ProfileService;
using BankApplication.Infrastructure.TransferService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer =  builder.Configuration.GetSection("Jwt:Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:TokenKey").Value))
            
        });
builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

//DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppDbConnection")));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);



builder.Services.AddTransient<ICardRepository, CardRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<IProfileRepository, ProfileRepository>();
builder.Services.AddTransient<ICardSampleRepository, CardSampleRepository>();
builder.Services.AddTransient<IOperationRepository, OperationRepository>();
builder.Services.AddTransient<ITransferService, TransferService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IFileUploadService, FileUploadService>();
builder.Services.AddTransient<IUserControlService, UserControlService>();

builder.Services.AddScoped(sp => SessionEmailToken.GetEmailToken(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();   
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["jwt"];
    if (!string.IsNullOrEmpty(token))
        context.Request.Headers.Add("Authorization", "Bearer " + token);
 
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "cardsFilter",
    pattern: "{controller=Cards}/{action=Index}/{type?}");

app.MapControllerRoute(
    name: "transfer",
    pattern: "{controller=Transfer}/{action=Replenish}/{id?}");

app.MapControllerRoute(
    name: "transfer",
    pattern: "{controller=Transfer}/{action=Transfer}/{id?}");

app.MapControllerRoute(
    name: "transfer",
    pattern: "{controller=Transfer}/{action=History}/{id?}");

app.Run();
