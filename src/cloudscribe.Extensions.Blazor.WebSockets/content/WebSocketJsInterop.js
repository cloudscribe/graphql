window.websocketInterop = {

    logToConsole: function (someDotNetObject) {
        console.log(someDotNetObject);
    },

    _socketset: {},

    createSocket: function (dotNetService, socketName, url, protocols) {

        var socketWrapper = function (dotNetService, url, protocols) {
            var theSocket = new WebSocket(url, protocols);
            theSocket.onopen = function (event) {
                dotNetService.invokeMethodAsync('JsSocketOpenCallback', socketName);
            };
            theSocket.onmessage = function (event) {
                console.log(event.data);
                var msg = JSON.parse(event.data);
                switch (msg.type) {
                    case "start":

                        break;
                    case "data":
                        dotNetService.invokeMethodAsync('JsSocketOnMessageCallback', socketName, JSON.stringify(msg.payload));
                        break;
                }
            };
            theSocket.onerror = function (event) {
                console.error("WebSocket error observed:", event);

            };

            return theSocket;
        };

        window.websocketInterop._socketset[socketName] = socketWrapper(dotNetService, url, protocols);
    },

    sendMessage: function (socketName, message) {
        var socket = window.websocketInterop._socketset[socketName];
        if (socket) {
            socket.send(message);
        } else {
            console.log("no socket found for " + socketName);
        }
    },

    closeSocket(dotNetService, socketName) {
        var socket = window.websocketInterop._socketset[socketName];
        if (socket) {
            socket.close();
            dotNetService.invokeMethodAsync('JsSocketClosedCallback', socketName);
        } else {
            console.log("can't close, no socket found for " + socketName);
        }
    }


};
