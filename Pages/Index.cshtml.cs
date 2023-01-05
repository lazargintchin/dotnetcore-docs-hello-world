using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;

namespace dotnetcoresample.Pages;

public class IndexModel : PageModel
{

    public string OSVersion { get { return RuntimeInformation.OSDescription; }  }
    
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        placeMessageOnQueue();
    }
    
    public void placeMessageOnQueue()
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=lazarstorage;AccountKey=XsQnGVw4M2b7IosvmPhUZVRrUl8To+TDB0ioirQyv6U7TxsKkSaABN3SMmBZOfbtbfA8VCeUYaM1+AStUoYkww==;EndpointSuffix=core.windows.net");
        CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
        CloudQueue queue = queueClient.GetQueueReference("lazarq");

        string messageContent = "Demo message for Sam";
        CloudQueueMessage message = new CloudQueueMessage(messageContent);
        queue.AddMessage(message);
    }
}
