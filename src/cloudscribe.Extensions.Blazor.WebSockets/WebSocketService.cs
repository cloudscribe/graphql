using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

//request
//{"id":"1","type":"start","payload":{"query":"subscription SiteUpdatedSubscription ($siteId : ID!) {\n  siteUpdated(id:$siteId) {\n    siteName,\n    companyName\n  }\n}","variables":{"siteId":"5961f387-accd-49dc-b962-44029d0803ae"},"operationName":"SiteUpdatedSubscription"}}

//response
//{"id":"1","type":"data","payload":{"data":{"siteUpdated":{"siteName":"GraphQL Demo","companyName":"My Company"}}}}

namespace cloudscribe.Extensions.Blazor.WebSockets
{
    public class WebSocketsService
    {
        public WebSocketsService()
        {

        }

        private List<string> _openSockets = new List<string>();

        public event Action<string> OnSocketOpened;
        public event Action<string> OnSocketClosed;
        public event Action<string, string> OnMessageRecieved;

        private void NotifyOnSocketOpened(string socketName) => OnSocketOpened?.Invoke(socketName);
        private void NotifyOnSocketClosed(string socketName) => OnSocketClosed?.Invoke(socketName);
        private void NotifyMessageRecieved(string socketName, string message) => OnMessageRecieved?.Invoke(socketName, message);

        [JSInvokable]
        public async Task JsSocketOpenCallback(string socketName)
        {
            _openSockets.Add(socketName);
            await JSRuntime.Current.InvokeAsync<object>("websocketInterop.logToConsole", $"{socketName} open");
            NotifyOnSocketOpened(socketName);
        }

        [JSInvokable]
        public async Task JsSocketClosedCallback(string socketName)
        {
            _openSockets.Remove(socketName);
            await JSRuntime.Current.InvokeAsync<object>("websocketInterop.logToConsole", $"{socketName} closed");
            NotifyOnSocketClosed(socketName);
        }

        [JSInvokable]
        public async Task JsSocketOnMessageCallback(string socketName, string message)
        {
            await JSRuntime.Current.InvokeAsync<object>("websocketInterop.logToConsole", $"{socketName} recieved message {message}");
            NotifyMessageRecieved(socketName, message);
        }

        public async Task CreateSocket(string socketName, string url, string[] protocols)
        {   
            await JSRuntime.Current.InvokeAsync<object>("websocketInterop.createSocket", new DotNetObjectRef(this), socketName, url, protocols);
        }

        public bool IsOpen(string sockletName)
        {
            return _openSockets.Contains(sockletName);
        }

        public async Task SendMessage(string socketName, string message)
        {
            if(IsOpen(socketName))
            {
                await JSRuntime.Current.InvokeAsync<object>("websocketInterop.sendMessage", socketName, message);
            }
            else
            {
                await JSRuntime.Current.InvokeAsync<object>("websocketInterop.logToConsole", $"could not send message {message} because socket {socketName} is not open");
            }
        }

        
    }
}
