using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;

namespace HackathonVitalsConsoleApp
{

    class Program
    {
        static EventHubClient eventHubClient;
        static string EventHubConnectionString = "HostName=babyMonitorIoT.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=eAn9d+SwphW2DIuB2gXFGqqZlzrZN3I25GogXoLeL38=";
        //private const string EventHubName = "babyvitals-workitems";
        private static DeviceClient deviceClient;
        static string json;
       


        static void Main(string[] args)
        {
            //This section contains the vitals for the baby. For the moment these are dummy values accurate values will be set later.

            int temperature;
            int heartbeat;
            Vitals vital;


            //serviceClient = ServiceClient.CreateFromConnectionString(EventHubConnectionString);
            deviceClient = DeviceClient.CreateFromConnectionString(EventHubConnectionString, "babymonitordevice");


           do
            {
                //Create value for json files

                Console.WriteLine("Please Enter the baby's temperature: ");
                temperature = int.Parse(Console.ReadLine());

                Console.WriteLine("Please Enter the baby's heartbeat: ");
                heartbeat = int.Parse(Console.ReadLine());

                //Object Declaration
                vital = new Vitals();


                //Set Vitals
                vital.setTemperature(temperature);
                vital.setHeartBeat(heartbeat);

                //List of Vitals class to generate Json file
                List<Vitals> vitals = new List<Vitals>();


                vitals.Add(new Vitals()
                {
                    Temperature = vital.getTemperature(),
                    Heartbeat = vital.getHeartBeat()

                });

                json = JsonConvert.SerializeObject(vitals.ToArray());


                //Console.WriteLine(temperature);
                //Console.WriteLine(heartbeat);


                using (StreamWriter file = File.CreateText(@"C:\Users\raquinta\source\repos\HackathonVitalsConsoleApp\HackathonVitalsConsoleApp\json file\vitals.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, vitals);
                }

                Console.WriteLine("JSON File has been generated");

                try
                {

                    SendDeviceToCloudMessageAsync().Wait();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.ReadLine();
                }

                 Console.ReadLine();

                //system pause

            } while (temperature != -1);
          
        }

        private async static Task SendDeviceToCloudMessageAsync()
        {
           
            var commandMessage = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(json));
            await deviceClient.SendEventAsync(commandMessage);
         
            
            //await Task.Delay(1000);
            Console.WriteLine("Message has been sent");
            Console.ReadLine();
            
        }


    }






}
