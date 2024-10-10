using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Common;

namespace assetTracking;

class Program
{
    static void Main(string[] args)
    {
        List<Asset> tracker = new List<Asset>();
        tracker.Add(new Smartphone("Motorola", "X3", 200, DateTime.Now.AddMonths(-36 + 4), "USA"));
        tracker.Add(new Smartphone("Motorola", "X3", 400, DateTime.Now.AddMonths(-36 + 5), "USA"));
        tracker.Add(new Smartphone("Motorola", "X2", 400, DateTime.Now.AddMonths(-36 + 10), "USA"));
        tracker.Add(new Smartphone("Samsung", "Galaxy 10", 4500, DateTime.Now.AddMonths(-36 + 6), "Sweden"));
        tracker.Add(new Smartphone("Samsung", "Galaxy 10", 4500, DateTime.Now.AddMonths(-36 + 7), "Sweden"));
        tracker.Add(new Smartphone("Sony", "XPeria 7", 3000, DateTime.Now.AddMonths(-36 + 4), "Sweden"));
        tracker.Add(new Smartphone("Sony", "XPeria 7", 3000, DateTime.Now.AddMonths(-36 + 5), "Sweden"));
        tracker.Add(new Smartphone("Siemens", "Brick", 220, DateTime.Now.AddMonths(-36 + 12), "Germany"));
        tracker.Add(new Computer("Dell", "Desktop 900", 100, DateTime.Now.AddMonths(-38), "USA"));
        tracker.Add(new Computer("Dell", "Desktop 900", 100, DateTime.Now.AddMonths(-37), "USA"));
        tracker.Add(new Computer("Lenovo", "X100", 300, DateTime.Now.AddMonths(-36 + 1), "USA"));
        tracker.Add(new Computer("Lenovo", "X200", 300, DateTime.Now.AddMonths(-36 + 4), "USA"));
        tracker.Add(new Computer("Lenovo", "X300", 500, DateTime.Now.AddMonths(-36 + 9), "USA"));
        tracker.Add(new Computer("Dell", "Optiplex 100", 1500, DateTime.Now.AddMonths(-36 + 7), "Sweden"));
        tracker.Add(new Computer("Dell", "Optiplex 200", 1400, DateTime.Now.AddMonths(-36 + 8), "Sweden"));
        tracker.Add(new Computer("Dell", "Optiplex 300", 1300, DateTime.Now.AddMonths(-36 + 9), "Sweden"));
        tracker.Add(new Computer("Asus", "ROG 600", 1600, DateTime.Now.AddMonths(-36 + 14), "Germany"));
        tracker.Add(new Computer("Asus", "ROG 500", 1200, DateTime.Now.AddMonths(-36 + 4), "Germany"));
        tracker.Add(new Computer("Asus", "ROG 500", 1200, DateTime.Now.AddMonths(-36 + 3), "Germany"));
        tracker.Add(new Computer("Asus", "ROG 500", 1300, DateTime.Now.AddMonths(-36 + 2), "Germany"));


        DateTime dateNow = DateTime.Now;
        List<Asset> trackerSorted = tracker
            .OrderBy(asset => asset.Office)
            .ThenBy(asset => asset.PurchaseDate)
            .ToList();

        Console.WriteLine("Office".PadRight(20) +  "Asset".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Price(USD)".PadRight(20) + "Price(local)".PadRight(20) + "Purchase Date".PadRight(20));

        foreach(Asset asset in trackerSorted)        
        {
           
            var timeDifference = dateNow.Subtract(asset.PurchaseDate);
            int experied = 1095 - timeDifference.Days;

            if(experied <= 90)
            {
                Console.ForegroundColor = ConsoleColor.Red;                
            } else if (experied <= 120)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;                
            }
            else
            { 
                Console.ForegroundColor = ConsoleColor.Gray;                
            }

            System.Console.WriteLine(asset.Office.PadRight(20)+ asset.GetType().Name.PadRight(20) + asset.Brand.PadRight(20) + asset.Model.PadRight(20) + asset.Price.ToString().PadRight(20) + asset.PurchaseDate.ToString("dd-MM-yyyy").PadRight(20));

        }
    }
}

interface IThing
{    
    public string Brand { get; set; }
    public string Model { get; set; }
    public double Price { get; set; }
    public DateTime PurchaseDate { get; set; }    
    public string Office { get; set; }
}

class Asset : IThing
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public double Price { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string Office { get; set; }

    public double LocalPrice { get; private set; }

    static async SetLocalPrice(string office)
    {
        string url = "https://v6.exchangerate-api.com/v6/0c6d0f2a976701ca77279360/latest/USD";

        if(office == "USA")
        {
            LocalPrice = Price;
        }
        else
        {
            try
            {
                // Make the API request
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();  // Throw if not a success code.

                // Get the response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var jsonResponse = JsonSerializer.Deserialize<ExchangeRateResponse>(responseBody, options);

                // Access exchange rates (example for EUR)
                decimal usdToEurRate = jsonResponse.ConversionRates["EUR"];

                Console.WriteLine($"USD to EUR: {usdToEurRate}");
            }
            catch (HttpRequestException e)
            {
                // Handle potential HTTP request exceptions
                Console.WriteLine("\nException caught!");
                Console.WriteLine($"Message :{e.Message} ");
            }
        }
    }
}

class Computer : Asset
{
    public Computer(string brand, string model, double price, DateTime purchaseDate, string office)
    {
        Brand = brand;
        Model = model;
        Price = price;
        PurchaseDate = purchaseDate;
        Office = office;        
    }
}

class Smartphone : Asset
{   
    public Smartphone(string brand, string model, double price, DateTime purchaseDate, string office)
    {
        Brand = brand;
        Model = model;
        Price = price;
        PurchaseDate = purchaseDate;
        Office = office;
    }
}
