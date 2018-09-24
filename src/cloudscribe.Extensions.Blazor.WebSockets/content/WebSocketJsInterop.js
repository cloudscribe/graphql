window.websocketInterop = {

    logToConsole: function (someDotNetObject) {
        console.log(someDotNetObject);
    },

    _socketset: {

    },

    createSocket: function (dotNetService, socketName, url, protocols) {

        var socketWrapper = function (dotNetService, url, protocalls) {
            var theSocket = new WebSocket(url, protocalls);
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
                        dotNetService.invokeMethodAsync('JsSocketOnMessageCallback', socketName, msg.payload);
                        break;

                }
 
            };

        };

        _socketset[socketName] = socketWrapper;
    },

    sendMessage: function (socketName, message) {
        var socket = _socketset[name];
        if (socket) {
            socket.send(message);
        } else {
            console.log("no socket found for " + socketName);
        }
    },

    closeSocket(dotNetService, socketName) {
        var socket = _socketset[name];
        if (socket) {
            socket.close();
            dotNetService.invokeMethodAsync('JsSocketClosedCallback', socketName);
        } else {
            console.log("can't close, no socket found for " + socketName);
        }
    }


};
