using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;
using System.Windows.Controls;

namespace WPFChatClient
{
    public partial class MainWindow : Window
    {
        HubConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            LoadEmojis();
        }

        // Sự kiện nút "Kết nối"
        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show("Vui lòng nhập tên trước!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtServerIP.Text))
            {
                MessageBox.Show("Vui lòng nhập IP Server!");
                return;
            }

            try
            {
                // Lấy IP từ TextBox
                string serverIP = txtServerIP.Text.Trim();
                string serverUrl = $"https://{serverIP}:7078/chathub";

                // Tạo kết nối đến Server với IP động
                connection = new HubConnectionBuilder()
                    .WithUrl(serverUrl, options =>
                    {
                        // Bỏ qua lỗi SSL certificate khi test trên LAN
                        options.HttpMessageHandlerFactory = (handler) =>
                        {
                            if (handler is System.Net.Http.HttpClientHandler clientHandler)
                            {
                                clientHandler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                            }
                            return handler;
                        };
                    })
                    .WithAutomaticReconnect()
                    .Build();

                // Lắng nghe sự kiện "ReceiveMessage" từ Server gửi về
                connection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    // Vì SignalR chạy thread ngầm, phải dùng Dispatcher để update giao diện
                    this.Dispatcher.Invoke(() =>
                    {
                        var fullMessage = $"{user}: {message}";
                        lstChat.Items.Add(fullMessage);

                        // Tự động cuộn xuống tin nhắn mới nhất
                        lstChat.ScrollIntoView(fullMessage);
                    });
                });

                // Kết nối
                await connection.StartAsync();
                lstChat.Items.Add($"--- Đã kết nối thành công tới {serverIP}! ---");

                // Mở khóa nút Gửi và khóa nút Kết nối
                btnSend.IsEnabled = true;
                btnConnect.IsEnabled = false;
                txtUser.IsEnabled = false;
                txtServerIP.IsEnabled = false;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}\n\nHãy kiểm tra:\n- Server đã chạy chưa?\n- IP có đúng không?\n- Firewall có chặn không?");
            }
        }

        // Sự kiện nút "Gửi"
        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Gửi tin nhắn lên Server
                await connection.InvokeAsync("SendMessage", txtUser.Text, txtMessage.Text);

                // Xóa ô nhập tin nhắn
                txtMessage.Text = "";
                txtMessage.Focus();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Lỗi gửi tin: {ex.Message}");
            }
        }

        private void LoadEmojis()
        {
            // Đây là danh sách các Emoji phổ biến (bạn có thể copy thêm từ web vào chuỗi này)
            string[] emojiList = new string[]
            {
            "😀", "😁", "😂", "🤣", "😃", "😄", "😅", "😆", "😉", "😊",
            "😋", "😎", "😍", "😘", "🥰", "😗", "😙", "😚", "🙂", "🤗",
            "🤩", "🤔", "🤨", "😐", "😑", "😶", "🙄", "😏", "😣", "😥",
            "😮", "🤐", "😯", "😪", "😫", "🥱", "😌", "😛", "😜", "😝",
            "🤤", "😒", "😓", "😔", "😕", "🙃", "🤑", "😲", "☹️", "🙁",
            "😖", "😞", "😟", "😤", "😢", "😭", "😦", "😧", "😨", "😩",
            "🤯", "😬", "😮‍💨", "😰", "😱", "🤪", "😵", "😵‍💫", "🥴", "😠" , 
            "🤬", "😷", "🤒", "🤕", "🤢", "🤮", "🤧", "😇", "🤠", "🤡",
            "🤥", "🤫", "🤭", "🧐", "🤓", "😈", "👿",
            "👽", "🤖", "💩", "😺", "😸", "😹", "😻", "😼", "😽", "🙀",
            "👍", "👎", "👊", "✊", "🤛", "🤜", "🤞", "✌️", "🤟", "🤘",
            "👌", "👈", "👉", "👆", "👇", "☝️", "✋", "🤚", "🖐", "🖖",
            "👋", "🤙", "💪", "❤️", "🧡", "💛", "💚", "💙", "💜", "🖤", "💔"
            };

            // Xóa các item cũ (nếu có)
            emojiGrid.Children.Clear();

            foreach (var emoji in emojiList)
            {
                // Tạo một Button mới cho mỗi Emoji
                var button = new Button();
                button.Content = emoji;
                button.Style = (Style)this.FindResource("EmojiButtonStyle");

                // Gán sự kiện Click
                button.Click += AddEmoji_Click;

                // Thêm vào UniformGrid
                emojiGrid.Children.Add(button);
            }
        }

        // Sự kiện khi bấm nút mặt cười ☺
        private void btnEmoji_Click(object sender, RoutedEventArgs e)
        {
            // Mở/đóng popup
            popEmoji.IsOpen = !popEmoji.IsOpen;
        }

        // Sự kiện khi chọn một emoji
        private void AddEmoji_Click(object sender, RoutedEventArgs e)
        {
            // Lấy emoji từ Content của Button
            if (sender is Button button)
            {
                string emoji = button.Content.ToString();

                // Thêm vào ô nhập tin nhắn
                txtMessage.Text += emoji;

                // Đưa con trỏ về cuối dòng để viết tiếp
                txtMessage.CaretIndex = txtMessage.Text.Length;

                // Đóng popup
                popEmoji.IsOpen = false;

                // Focus lại vào ô nhập liệu
                txtMessage.Focus();
            }
        }
    }
}