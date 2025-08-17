using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using piy.attendance.app.contract.Interfaces;
using piy.attendance.app.Security;
using piy.attendance.app.Services;

namespace piy.attendance.app;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        // EF Core (Sqlite default; replace with SQL Server if needed)
        var cs = config.GetConnectionString("Default") ?? "Data Source=piy.attendance.db";
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(cs));
        // For SQL Server, use: opt.UseSqlServer(cs)

        // Services
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IClassService, ClassService>();
        services.AddScoped<ILectureService, LectureService>();
        services.AddScoped<IAttendanceService, AttendanceService>();

        // Security / QR
        services.AddSingleton<IQrTokenProvider, QrTokenProvider>();

        return services;
    }
}
