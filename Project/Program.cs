using Microsoft.EntityFrameworkCore;
using Project;

var builder = WebApplication.CreateBuilder(args);

//�������� ������ ����������� �� ����� ������������
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

//��������� �������� U_NetContext � �������� ������� � ����������
builder.Services.AddDbContext<U_NetContext>(options => options.UseSqlServer(connection));

// ��������� ������� � ���������.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// ��������� �������� HTTP-��������.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // �������� HSTS �� ��������� � 30 ����. �� ������ �������� ��� ��� ������� ���������, ��. https://aka.ms/aspnetcore-hsts.
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
