const Messages = {
    data: () => ({
        connection: null,
        messages: [],
    }),
    methods: {
        addMessage(message) {
            this.messages.push(message);
        },
    },
    mounted() {
        this.connection = new signalR.HubConnectionBuilder().withUrl("/messagesHub").build();

        this.connection
            .on("ReceivedMessage", (message) => {
                this.addMessage(message);
            });

        this.connection.start()
            .then(() => console.log("Connected to messages hub."))
            .catch((err) => console.error(err.toString()));
    }
};

Vue.createApp(Messages).mount("#messages");