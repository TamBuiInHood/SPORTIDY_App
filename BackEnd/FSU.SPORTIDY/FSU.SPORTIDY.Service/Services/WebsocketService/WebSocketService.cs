using System.Net.WebSockets;
using System.Text;
using FSU.SPORTIDY.Service.BusinessModel.MeetingBsModels;
using Newtonsoft.Json;

public class WebSocketService
{
    private static List<WebSocket> clients = new List<WebSocket>();

    // Accepts a new client connection and adds to the list
    public async Task AddClient(WebSocket webSocket)
    {
        clients.Add(webSocket);
        await HandleWebSocketChat(webSocket);
    }

    // Handles incoming WebSocket requests
    public async Task HandleWebSocketChat(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            var clientMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Client: {clientMessage}");

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        clients.Remove(webSocket);
        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }

    // Broadcast a new comment to all connected clients
    public async Task BroadcastNewComment(CommentInMeetingModel comment)
    {
        var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(comment));

        foreach (var client in clients)
        {
            if (client.State == WebSocketState.Open)
            {
                await client.SendAsync(new ArraySegment<byte>(message), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
