using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Volcano.Services.Sub.Chatbot.Chatbot;

ITelegramBotClient botClient;
int period = 3000;

try
{
    var chatbotToken = "6203590098:AAFp03fgAbv46c7He82VCK_LClI9TVdCfKY";
    Timer t = new(Running, null, 0, period);
    botClient = new TelegramBotClient(chatbotToken);
    var me = await botClient.GetMeAsync();

    Console.WriteLine($"Chatbot for Telegram!\n\tId: {me.Id}\n\tBot name:{me.FirstName}!");
    using var cts = new CancellationTokenSource();

    // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
    ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };
    botClient.StartReceiving(Bot_OnMessage, HandleErrorAsync, receiverOptions, cts.Token);

    Console.WriteLine($"Start listening for @{me.Username}");
    Console.ReadLine();

    // Send cancellation request to stop bot
    cts.Cancel();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    Console.WriteLine("Press any key to exit");
    Console.ReadKey();
}
static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}
static async Task Bot_OnMessage(ITelegramBotClient botClient, Update e, CancellationToken cancellationToken)
{
    if (e?.Message?.Text != null)
    {
        string content;
        string message;

        if (e.Message.Text.StartsWith("/otp"))
        {
            string username = e.Message.Text.Replace("/otp", "");
            message = await BotMessage.GetOTP(username.Trim());
            content = "call command /otp";
            WriteConsole(username, content);

            try
            {
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: message, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        else if (e.Message.Text.StartsWith("/start"))
        {
            message = BotMessage.StartChat();

            try
            {
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: message, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        else if (e.Message.Text.StartsWith("/help"))
        {
            message = BotMessage.HelpChat();

            try
            {
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: message, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        else if (e.Message.Text.StartsWith("/idgroup"))
        {
            message = e.Message.Chat.Id.ToString();

            try
            {
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: message, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        else if (e.Message.Text.StartsWith("/chat"))
        {
            string chatContent = e.Message.Text.Replace("/chat", "");
            message = await BotMessage.GetChatContent(chatContent.Trim());
            content = "call command /chat";
            WriteConsole(chatContent, content);

            try
            {
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: message, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        else
        {
            string msg = e.Message.Text;

            try
            {
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: msg, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
static void Running(object o)
{
    // Keep alive app
    Console.WriteLine("Listen: " + DateTime.Now);
}
static void WriteConsole(string username, string content)
{
    Console.WriteLine(DateTime.Now + " | " + username + " - " + content);
}
