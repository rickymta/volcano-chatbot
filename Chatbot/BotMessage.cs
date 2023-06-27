namespace Volcano.Services.Sub.Chatbot.Chatbot;
internal class BotMessage
{
    private static readonly string SystemName = "Volcano Chatbot";
    private static readonly string NewLine = Environment.NewLine;
    private static readonly string BaseUrl = "http://localhost:6886/api/";
    private static readonly string Key = "27964bdc-5718d02-53c051be-36b67c2687-89c9512adghz4bb6fc5c3";
    public static string StartChat()
    {
        string message = "Xin chào! Tôi là chatbot của hệ thống " + SystemName + NewLine + NewLine;
        message += "Sử dụng các lệnh dưới đây để thao tác:" + NewLine + NewLine;
        message += "/help - hiển thị ra trợ giúp" + NewLine + NewLine;
        message += "Cần trợ giúp khác vui lòng liên hệ quản trị viên!";
        return message;
    }
    public static string HelpChat()
    {
        string message = "Sử dụng các lệnh dưới đây để thao tác:" + NewLine + NewLine;
        message += "Trợ giúp:" + NewLine;
        message += "/help - hiển thị ra trợ giúp" + NewLine + NewLine;
        message += "Cần trợ giúp khác vui lòng liên hệ quản trị viên!";
        return message;
    }
    public static async Task<string> GetOTP(string otp)
    {
        if (string.IsNullOrEmpty(otp))
        {
            return "Otp không được để trống";
        }
        else
        {
            return otp;
        }
    }
    public static async Task<string> GetChatContent(string content)
    {
        return content;
    }
}