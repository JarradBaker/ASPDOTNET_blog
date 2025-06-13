using DOTNET_DIARIES.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using DOTNET_DIARIES.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DOTNET_DIARIES_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Add Identity services BEFORE building the app
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DOTNET_DIARIES_Context>();

// Configure Cloudinary
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();


var app = builder.Build();

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

app.MapRazorPages(); // This line is required for Identity UI pages (like Login)

async Task SeedAdminUserAsync(IServiceProvider services, IConfiguration config)
{
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    var adminSection = config.GetSection("AdminUser");
    string adminEmail = adminSection["Email"];
    string adminPassword = adminSection["Password"];
    string adminRole = adminSection["Role"];

    if (!await roleManager.RoleExistsAsync(adminRole))
        await roleManager.CreateAsync(new IdentityRole(adminRole));

    var user = await userManager.FindByEmailAsync(adminEmail);

    if (user == null)
    {
        // Try to find an existing admin user by role (in case the email changed)
        var adminUsers = await userManager.GetUsersInRoleAsync(adminRole);
        var oldAdmin = adminUsers.FirstOrDefault();
        if (oldAdmin != null)
        {
            oldAdmin.Email = adminEmail;
            oldAdmin.UserName = adminEmail;
            oldAdmin.EmailConfirmed = true;
            var updateResult = await userManager.UpdateAsync(oldAdmin);
            if (!updateResult.Succeeded)
                throw new Exception(string.Join("; ", updateResult.Errors.Select(e => e.Description)));
            user = oldAdmin;
        }
        else
        {
            user = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(user, adminPassword);
            if (!result.Succeeded)
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
        }
    }

    // Reset password if it does not match the configured one
    var passwordHasher = new PasswordHasher<IdentityUser>();
    var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, adminPassword);
    if (verificationResult == PasswordVerificationResult.Failed)
    {
        var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetResult = await userManager.ResetPasswordAsync(user, resetToken, adminPassword);
        if (!resetResult.Succeeded)
            throw new Exception(string.Join("; ", resetResult.Errors.Select(e => e.Description)));
    }

    if (!await userManager.IsInRoleAsync(user, adminRole))
        await userManager.AddToRoleAsync(user, adminRole);
}

// SEED ADMIN USER AND ROLE
using (var scope = app.Services.CreateScope())
{
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    await SeedAdminUserAsync(scope.ServiceProvider, config);
}

app.Run();
