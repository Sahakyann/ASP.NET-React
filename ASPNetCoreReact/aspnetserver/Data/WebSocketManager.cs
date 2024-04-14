using System.Net.WebSockets;
using System.Text;

namespace aspnetserver.Data
{
    public class WebSocketManager
    {
        private static readonly List<WebSocket> connections = new List<WebSocket>();

        public static async Task HandleConnection(WebSocket webSocket, HttpContext context, CancellationToken cancellationToken)
        {
            Console.WriteLine("WebSocket connection attempt received.");
            connections.Add(webSocket);
            try
            {
                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult result;
                do
                {
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                    if (result.MessageType == WebSocketMessageType.Text && !result.CloseStatus.HasValue)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        Console.WriteLine($"Message received: {message}");
                        await BroadcastMessage(message, cancellationToken);
                    }
                } while (!result.CloseStatus.HasValue);

                await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket Error: {ex.Message}");
            }
            finally
            {
                connections.Remove(webSocket);
            }
        }

        private static async Task BroadcastMessage(string message, CancellationToken cancellationToken)
        {
            foreach (var socket in connections.Where(s => s.State == WebSocketState.Open))
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                await socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, cancellationToken);
            }
        }
    }
}
