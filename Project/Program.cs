using Microsoft.EntityFrameworkCore;
using Project;

var builder = WebApplication.CreateBuilder(args);

//получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

//добовляем контекст U_NetContext в качестве сервиса в приложение
builder.Services.AddDbContext<U_NetContext>(options => options.UseSqlServer(connection));

// Добавляем сервисы в контейнер.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Настройте конвейер HTTP-запросов.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Значение HSTS по умолчанию — 30 дней. Вы можете изменить это для рабочих сценариев, см. https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
