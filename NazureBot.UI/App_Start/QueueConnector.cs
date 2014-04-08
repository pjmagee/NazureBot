namespace NazureBot.UI
{
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using Microsoft.WindowsAzure;

    /// <summary>
    /// The queue connector.
    /// </summary>
    public static class QueueConnector
    {
        /// <summary>
        /// The client.
        /// </summary>
        private static QueueClient client;

        /// <summary>
        /// The queue name.
        /// </summary>
        private const string QueueName = "ProcessingQueue";

        /// <summary>
        /// The client.
        /// </summary>
        public static QueueClient Client
        {
            get
            {
                return client;
            }
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        public static void Initialize()
        {
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;

            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            namespaceManager.CreateSubscription()

            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            var messagingFactory = MessagingFactory.Create(namespaceManager.Address, namespaceManager.Settings.TokenProvider);

            client = messagingFactory.CreateQueueClient(QueueName);
        }
    }
}