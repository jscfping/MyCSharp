








const chatHubCon = {
    _connection: null,
    startAsync: async function (userName, listeners) {
        if (!userName) throw new Error("no userName!");
        this._connection = new signalR.HubConnectionBuilder()
            .withUrl(`../chatHub?user=${userName}`)
            .build();

        listeners.forEach(listener => {
            this._connection.on(listener.name, listener.callback);
        });

        await this._connection.start()
            .then(() => {

            })
            .catch(e => {
                console.error(e);
            });
    },
    sendAsync: async function (msg) {
        await this._connection.invoke("SendMessageAsync", msg)
    }
};







const vm = new Vue({
    el: "#app",
    data: {
        userName: "",
        message: "",
        isStartChat: false,
        messageToSend: ""
    },
    mounted: async function () {

    },
    methods: {
        startChatAsync: async function () {
            if (!this.userName) {
                alert("no user name");
                return;
            }
            await chatHubCon.startAsync(this.userName, [
                { name: "SystemBroadcast", callback: this.onSystemBroadcast },
                { name: "SomeoneSays", callback: this.onSomeoneSays }
            ]);
            this.isStartChat = true;
        },
        onSystemBroadcast: function (time, msg) {
            this.updateMessage(`[${time}][system] ${msg}\n`);
        },
        onSomeoneSays: function (time, aUser, msg) {
            this.updateMessage(`[${time}] ${aUser.name}[${aUser.connectionId}]:${msg}\n`);
        },
        sendAsync() {
            try {
                if (!this.messageToSend) throw new Error("nothing to say");
                chatHubCon.sendAsync(this.messageToSend);
                this.messageToSend = "";
            }
            catch (e) {
                console.error(e);
                alert(e.message);
            }
        },
        updateMessage(message){
            this.message += message;
            this.$refs.message.scrollTop = this.$refs.message.scrollHeight;
        }
        
    }
});


