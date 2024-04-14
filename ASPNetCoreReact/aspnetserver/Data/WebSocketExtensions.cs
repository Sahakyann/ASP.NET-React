using System.Net.WebSockets;

namespace aspnetserver.Data
{
    public static class WebSocketExtensions
    {
        public static void MapWebSocket(this IEndpointRouteBuilder app, string path, Func<WebSocket, HttpContext, CancellationToken, Task> webSocketHandler)
        {

            app.MapGet(path, async context =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await webSocketHandler(webSocket, context, context.RequestAborted);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("WebSocket requests only.");
                }
            });
        }
    }
}
