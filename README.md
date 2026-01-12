# WPF Chat Application

Ứng dụng chat real-time sử dụng WPF (Windows Presentation Foundation) và SignalR.

## 📋 Mô tả

Đây là ứng dụng chat đơn giản cho phép nhiều người dùng kết nối và trò chuyện với nhau trong thời gian thực. Ứng dụng sử dụng SignalR để xử lý kết nối real-time giữa client và server.

## 🚀 Tính năng

- ✅ Kết nối real-time với SignalR
- ✅ Gửi và nhận tin nhắn ngay lập tức
- ✅ Emoji picker với hơn 100 biểu tượng cảm xúc
- ✅ Giao diện thân thiện, dễ sử dụng
- ✅ Tự động cuộn đến tin nhắn mới nhất
- ✅ Tự động kết nối lại khi mất kết nối

## 🛠️ Công nghệ sử dụng

- **Frontend**: WPF (Windows Presentation Foundation)
- **Backend**: ASP.NET Core
- **Real-time Communication**: SignalR
- **.NET Version**: .NET 6.0 trở lên

## 📦 Cài đặt

### Yêu cầu hệ thống
- Windows 10/11
- .NET 6.0 SDK trở lên
- Visual Studio 2022 (hoặc Visual Studio Code)

### Các bước cài đặt

1. **Clone repository**
```bash
git clone <repository-url>
cd ChatBox
```

2. **Restore packages**
```bash
dotnet restore
```

3. **Chạy Server**
```bash
cd ChatServer
dotnet run
```
Server sẽ chạy tại `https://localhost:7078`

4. **Chạy Client** (mở terminal mới)
```bash
cd WPFChatClient
dotnet run
```

## 📖 Hướng dẫn sử dụng

1. **Khởi động ứng dụng**
   - Chạy ChatServer trước
   - Sau đó chạy WPFChatClient (có thể mở nhiều instance)

2. **Kết nối**
   - Nhập tên của bạn vào ô "Tên bạn"
   - Click nút "Kết nối Server"

3. **Gửi tin nhắn**
   - Nhập nội dung tin nhắn vào ô nhập liệu
   - Click nút "Gửi" hoặc nhấn Enter

4. **Sử dụng Emoji**
   - Click vào nút ☺ để mở bảng emoji
   - Chọn emoji muốn sử dụng
   - Emoji sẽ được thêm vào tin nhắn

## 📁 Cấu trúc dự án

```
ChatBox/
├── WPFChatClient/          # WPF Client Application
│   ├── MainWindow.xaml     # Giao diện chính
│   ├── MainWindow.xaml.cs  # Logic xử lý
│   ├── App.xaml           # Application resources
│   └── App.xaml.cs        # Application startup
├── ChatServer/            # ASP.NET Core Server (nếu có)
│   └── ...
└── README.md             # File này
```

## 🎨 Giao diện

### Màn hình chính
- **Header**: Nhập tên và nút kết nối
- **Chat Area**: Hiển thị lịch sử tin nhắn
- **Input Area**: Nút emoji, ô nhập tin, nút gửi

### Emoji Picker
- Hiển thị dạng lưới 10 cột
- Hơn 100 emoji đa dạng
- Hiệu ứng hover khi di chuột
- Tự động đóng sau khi chọn

## 🔧 Cấu hình

### Thay đổi địa chỉ Server

Trong file `MainWindow.xaml.cs`, dòng 17:
```csharp
connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7078/chathub")  // Thay đổi URL tại đây
    .WithAutomaticReconnect()
    .Build();
```

### Tùy chỉnh Emoji

Trong file `MainWindow.xaml.cs`, phương thức `LoadEmojis()`:
```csharp
string[] emojiList = new string[]
{
    // Thêm hoặc xóa emoji tại đây
    "😀", "😁", "😂", ...
};
```

### Tùy chỉnh giao diện Emoji Grid

Trong file `MainWindow.xaml`, dòng 39:
```xml
<UniformGrid x:Name="emojiGrid" Columns="10"/>  <!-- Thay đổi số cột -->
```

Trong `Window.Resources`:
```xml
<Style x:Key="EmojiButtonStyle" TargetType="Button">
    <Setter Property="Width" Value="40"/>      <!-- Kích thước button -->
    <Setter Property="Height" Value="40"/>
    <Setter Property="FontSize" Value="20"/>   <!-- Kích thước emoji -->
    ...
</Style>
```

## 🐛 Xử lý lỗi thường gặp

### Lỗi: "Không thể kết nối đến Server"
- Kiểm tra Server đã chạy chưa
- Xác nhận port đúng (7078)
- Kiểm tra firewall/antivirus

### Lỗi: "Emoji không hiển thị"
- Đảm bảo font hệ thống hỗ trợ emoji
- Windows 10/11 thường hỗ trợ tốt emoji

### Lỗi: "SSL Certificate"
- Chạy: `dotnet dev-certs https --trust`

## 📝 Ghi chú

- Ứng dụng này được phát triển cho mục đích học tập
- Server phải chạy trước khi client kết nối
- Có thể mở nhiều client cùng lúc để test

## 👨‍💻 Tác giả

Dự án Lab1 - PRN222 - Spring 2026

## 📄 License

Dự án này được tạo cho mục đích học tập tại FPT University.

---

**Chúc bạn code vui vẻ! 🚀**
