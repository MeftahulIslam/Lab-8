using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ReverseGeoCoding_Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            string latInput, longInput;
            float latitude, longitude;
            bool islatNum, isLongNum;

            Console.WriteLine("Reverse Geo Coding");
            Console.WriteLine("Please Enter Latitude: ");
            latInput = Console.ReadLine();
            Console.WriteLine("Please Enter Longitude");
            longInput = Console.ReadLine();
            islatNum = float.TryParse(latInput, out latitude);
            isLongNum = float.TryParse(longInput, out longitude);
            if (islatNum && isLongNum )
            {
                latitude = float.Parse(latInput);
                longitude = float.Parse(longInput);
                if(latitude >= 0 && longitude >= 0)
                {
                    RootObject rootObject = getAddress(latitude, longitude);
                    Console.WriteLine("Full Address " + rootObject.display_name);
                }
                else
                {
                    Console.WriteLine("Please provide valid Latitude and Longitude");
                }

            }
            else
            {
                Console.WriteLine("Please provide valid Latitude and Longitude");
            }
            
            
            Console.ReadLine();
        }

        public static RootObject getAddress(double lat, double lon)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            webClient.Headers.Add("Referer", "http://www.microsoft.com");
            var jsonData = webClient.DownloadData("http://nominatim.openstreetmap.org/reverse?format=json&lat=" + lat + "&lon=" + lon);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));
            RootObject rootObject = (RootObject)ser.ReadObject(new MemoryStream(jsonData));
            return rootObject;
        }
    }
}
