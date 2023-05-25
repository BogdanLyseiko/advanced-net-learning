using Azure.Messaging.ServiceBus;
using QueueReceiverTask2;
using System.Text.Json;

ServiceBusClient client;
ServiceBusProcessor processor;

List<BasketItem> _basketItems = new()
{
    new BasketItem { CatalogItemId = 1, Name = "CItem1", OrderId = 1, Price = 2.3M },
    new BasketItem { CatalogItemId = 2, Name = "CItem2", OrderId = 2, Price = 5M }
};

// handle received messages
async Task MessageHandler(ProcessMessageEventArgs args)
{
    CatalogItem? catalogItem = JsonSerializer.Deserialize<CatalogItem>(args.Message.Body);

    if (catalogItem is not null)
    {
        List<BasketItem> basketItemsToUpdate = _basketItems.Where(x => x.CatalogItemId == catalogItem.Id).ToList();

        foreach (BasketItem? item in basketItemsToUpdate)
        {
            item.Price = catalogItem.Price;
            item.Name = catalogItem.Name;
        }

        Console.WriteLine("BasketItems updated:");
        foreach (BasketItem item in _basketItems)
        {
            Console.WriteLine($"OrderId: {item.OrderId}; Name: {item.Name}; Price: {item.Price}");
        }
    }

    await args.CompleteMessageAsync(args.Message);
}

Task ErrorHandler(ProcessErrorEventArgs args)
{
    //Instead of show error in console we can log it using logger
    Console.WriteLine(args.Exception.ToString(), "Message handler encountered an exception");
    Console.WriteLine($"- ErrorSource: {args.ErrorSource}");
    Console.WriteLine($"- Entity Path: {args.EntityPath}");
    Console.WriteLine($"- FullyQualifiedNamespace: {args.FullyQualifiedNamespace}");

    return Task.CompletedTask;
}

ServiceBusClientOptions clientOptions = new()
{
    TransportType = ServiceBusTransportType.AmqpWebSockets
};
client = new ServiceBusClient("Replace-with-connection-string", clientOptions);

processor = client.CreateProcessor("queue-1", new ServiceBusProcessorOptions());

try
{
    processor.ProcessMessageAsync += MessageHandler;
    processor.ProcessErrorAsync += ErrorHandler;

    await processor.StartProcessingAsync();

    Console.WriteLine("Press any key to end the processing");
    Console.ReadKey();

    // stop processing 
    Console.WriteLine("\nStopping the receiver...");
    await processor.StopProcessingAsync();
    Console.WriteLine("Stopped receiving messages");
}
finally
{
    await processor.DisposeAsync();
    await client.DisposeAsync();
}