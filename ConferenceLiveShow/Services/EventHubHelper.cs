using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using ConferenceLiveShow.Models;
using Newtonsoft.Json;
using ConferenceLiveShow.Services;
namespace ConferenceLiveShow.Services
{

    public class EventHubHelper
    {
        private static EventHubClient eventHubClient;
        private const string EhConnectionString = "Endpoint=sb://cameraevents.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=8v+I9/igry17pSbqKAbUaH8y2cppxJ4BmB9Z7xa5D1c=";
        private const string EhEntityPath = "cameraidentifyvalue";
        public EventHubHelper()
        {
            
        }
        public async Task SendMessagesToEventHub(CustomFaceEmojiModel customFaceEmojiModel)
        {
            try
            {
                var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
                {
                    EntityPath = EhEntityPath
                };
                eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
                var data = JsonConvert.SerializeObject(customFaceEmojiModel);
                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(data))); //send msg to event hub 
                await eventHubClient.CloseAsync();
            }
            catch (Exception ex)
            {
                ShowErrorHelper.ShowDialog(ex.Message);
            }
            await eventHubClient.CloseAsync();
        }
    }
}
