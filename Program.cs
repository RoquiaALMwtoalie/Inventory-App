using Inventory.Data;
using Inventory.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;  // إضافة هذه المكتبة للتعامل مع الملفات

var builder = WebApplication.CreateBuilder(args);

// إضافة الخدمات إلى الحاوية.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
var configuration = builder.Configuration;

var dbPath = Path.Combine(AppContext.BaseDirectory, "warehouse.db");
var connectionString = $"Data Source={dbPath}";

// تسجيل ApplicationDbContext مع SQLite باستخدام المسار الجديد
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// تسجيل ProductService 
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// تكوين الأنابيب حسب البيئة
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseDeveloperExceptionPage();  // إضافة هذا السطر للحصول على تفاصيل الأخطاء في بيئة الإنتاج
}
else
{
    app.UseDeveloperExceptionPage(); // تمكين صفحة الخطأ في بيئة التطوير
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// فتح المتصفح تلقائيًا عند بدء التطبيق
OpenBrowser("http://localhost:5000");

app.Run();

// الدالة لفتح المتصفح
void OpenBrowser(string url)
{
    try
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
    catch (Exception ex)
    {
        // معالجة الاستثناءات إذا لم يتمكن من فتح المتصفح
        Console.WriteLine("Error opening browser: " + ex.Message);
    }
}
