using InheritanceIssueDemo.Components;
using InheritanceIssueDemo.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents();

builder.Services.AddScoped<IBooksRepository, BooksRepository>();

string connectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection") ?? throw new Exception(message: "Connection string is missing");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;

    using (AppDbContext context = services.GetRequiredService<AppDbContext>())
        if (!context.Books.Any())
        {
            context.Books.AddRange(Book.Create(title: "Winnie ther Pooh", numberOfPages: 125),
                                   Book.Create(title: "Pro ASP.NET Core Identity", numberOfPages: 725),
                                   Book.Create(title: "Pro ASP.NET Core MVC 2", numberOfPages: 725),
                                   Book.Create(title: "C# 10 in a Nutshell", numberOfPages: 568));

            context.SaveChanges();
        }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorHandlingPath: "/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
