using ChatBox;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Server lắng nghe trên tất cả network interfaces (cho phép kết nối từ LAN)
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(7078, listenOptions =>
    {
        listenOptions.UseHttps(); // Sử dụng HTTPS
    });
});

// Add services to the container.
builder.Services.AddSignalR();

// Thêm CORS để cho phép kết nối từ các máy khác
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

// Sử dụng CORS
app.UseCors("AllowAll");

app.MapHub<ChatHub>("/chathub");

// In ra IP của Server khi khởi động
var localIP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
    .AddressList
    .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

Console.WriteLine("===========================================");
Console.WriteLine("Chat Server đang chạy!");
Console.WriteLine($"Local: https://localhost:7078");
Console.WriteLine($"LAN IP: https://{localIP}:7078");
Console.WriteLine("===========================================");
Console.WriteLine("Chia sẻ IP LAN với bạn bè để họ kết nối!");
Console.WriteLine("===========================================\n");

app.Run();
