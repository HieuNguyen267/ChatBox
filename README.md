# WPF Chat Application

Ứng dụng chat real-time sử dụng WPF (Windows Presentation Foundation) và SignalR.

## 📋 Mô tả

Đây là ứng dụng chat đơn giản cho phép nhiều người dùng kết nối và trò chuyện với nhau trong thời gian thực. Ứng dụng sử dụng SignalR để xử lý kết nối real-time giữa client và server.

## 🚀 Tính năng

- ✅ Kết nối real-time với SignalR
- ✅ Gửi và nhận tin nhắn ngay lập tức
- ✅ **Hỗ trợ chat qua mạng LAN (WiFi)** - Chat với bạn bè cùng WiFi
- ✅ Nhập IP Server động - Kết nối đến bất kỳ server nào
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

### 🏠 Chat trên cùng một máy (localhost)

1. **Khởi động Server**
   ```bash
   cd ChatBox
   dotnet run
   ```
   Server sẽ hiển thị IP của bạn:
   ```
   🚀 Chat Server đang chạy!
   📍 Local: https://localhost:7078
   📍 LAN IP: https://192.168.1.100:7078
   ```

2. **Khởi động Client**
   - Mở WPFChatClient
   - Để IP Server là `localhost`
   - Nhập tên và click "Kết nối Server"

### 🌐 Chat qua mạng LAN (cùng WiFi)

#### Người Host (chạy Server):

1. **Chạy Server**
   ```bash
   cd ChatBox
   dotnet run
   ```

2. **Lấy IP của máy bạn**
   - Server sẽ tự động hiển thị IP LAN (ví dụ: `192.168.1.100`)
   - Hoặc mở CMD/PowerShell và gõ: `ipconfig`
   - Tìm "IPv4 Address" trong phần WiFi adapter

3. **Chia sẻ IP với bạn bè**
   - Ví dụ: `192.168.1.100`

4. **Cấu hình Firewall**
   - Mở Windows Defender Firewall
   - Cho phép port 7078 (xem hướng dẫn chi tiết bên dưới)

#### Người tham gia (Client):

1. **Mở WPFChatClient**

2. **Nhập IP Server**
   - Nhập IP mà người host đã chia sẻ (ví dụ: `192.168.1.100`)
   - Nhập tên của bạn

3. **Kết nối**
   - Click "Kết nối Server"
   - Bắt đầu chat!

### 💬 Gửi tin nhắn

- Nhập nội dung tin nhắn vào ô nhập liệu
- Click nút "Gửi" hoặc nhấn Enter

### 😊 Sử dụng Emoji

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

### Cấu hình Firewall cho LAN

Để cho phép các máy khác kết nối đến Server của bạn qua LAN:

#### Windows Firewall:

1. **Mở Windows Defender Firewall**
   - Tìm kiếm "Windows Defender Firewall" trong Start Menu
   - Click "Advanced settings"

2. **Tạo Inbound Rule**
   - Click "Inbound Rules" → "New Rule..."
   - Chọn "Port" → Next
   - Chọn "TCP", nhập port: `7078` → Next
   - Chọn "Allow the connection" → Next
   - Chọn tất cả (Domain, Private, Public) → Next
   - Đặt tên: "ChatBox Server" → Finish

3. **Hoặc dùng PowerShell (chạy as Administrator)**
   ```powershell
   New-NetFirewallRule -DisplayName "ChatBox Server" -Direction Inbound -LocalPort 7078 -Protocol TCP -Action Allow
   ```

### Thay đổi Port

Nếu muốn đổi port khác (ví dụ: 5000):

**Server** - File `Program.cs`:
```csharp
serverOptions.ListenAnyIP(5000, listenOptions =>  // Đổi 7078 thành 5000
```

**Client** - Nhập IP với port mới:
```
192.168.1.100:5000
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

**Nguyên nhân và giải pháp:**

1. **Server chưa chạy**
   - Kiểm tra Server đã chạy chưa
   - Xem terminal có hiển thị "Chat Server đang chạy!" không

2. **IP sai**
   - Kiểm tra lại IP đã nhập đúng chưa
   - Thử ping IP: `ping 192.168.1.100`
   - Nếu ping không được → IP sai hoặc không cùng mạng

3. **Firewall chặn**
   - Tắt tạm thời Firewall để test
   - Nếu được → tạo rule cho port 7078 (xem phần Cấu hình Firewall)

4. **Không cùng WiFi**
   - Đảm bảo cả 2 máy kết nối cùng một WiFi
   - Không thể kết nối nếu một máy dùng WiFi, máy kia dùng dây

5. **Antivirus chặn**
   - Tạm thời tắt antivirus để test
   - Thêm exception cho ứng dụng

### Lỗi: "SSL Certificate" / "The SSL connection could not be established"

**Giải pháp:**

1. **Trust development certificate**
   ```bash
   dotnet dev-certs https --trust
   ```

2. **Nếu vẫn lỗi, tạo lại certificate**
   ```bash
   dotnet dev-certs https --clean
   dotnet dev-certs https --trust
   ```

3. **Khởi động lại cả Server và Client**

### Lỗi: "Emoji không hiển thị"
- Đảm bảo font hệ thống hỗ trợ emoji
- Windows 10/11 thường hỗ trợ tốt emoji
- Cập nhật Windows lên phiên bản mới nhất

### Lỗi: "Address already in use" (Port đã được sử dụng)
- Port 7078 đang được ứng dụng khác sử dụng
- Tắt ứng dụng đó hoặc đổi port (xem phần Cấu hình)

## 📝 Ghi chú

- Ứng dụng này được phát triển cho mục đích học tập
- Server phải chạy trước khi client kết nối
- Có thể mở nhiều client cùng lúc để test
- **LAN Chat**: Chỉ hoạt động khi cùng mạng WiFi/LAN
- **Internet Chat**: Cần cấu hình Port Forwarding trên router (nâng cao)

## 🎯 Các tình huống sử dụng

### 1. Test một mình trên máy
- IP Server: `localhost`
- Mở nhiều Client để test

### 2. Chat với bạn trong phòng (cùng WiFi)
- Người host chạy Server
- Người khác nhập IP của host
- Bắt đầu chat!

### 3. Chat trong công ty/trường học (cùng LAN)
- Tương tự như trường hợp 2
- Có thể có nhiều người tham gia

## 🔐 Bảo mật

⚠️ **Lưu ý bảo mật:**
- Ứng dụng này dùng để học tập, không mã hóa tin nhắn
- Không gửi thông tin nhạy cảm
- SSL certificate là self-signed (chỉ dùng cho development)
- Trong môi trường production cần:
  - SSL certificate hợp lệ
  - Authentication/Authorization
  - Mã hóa tin nhắn
  - Rate limiting

## 👨‍💻 Tác giả

Dự án Lab1 - PRN222 - Spring 2026

## 📄 License

Dự án này được tạo cho mục đích học tập tại FPT University.

---

**Chúc bạn code vui vẻ! 🚀**
