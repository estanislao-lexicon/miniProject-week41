using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace assetTracking;

class Program
{
    static void Main(string[] args)
    {
        List<Asset> trackerList = new List<Asset>();
        trackerList.Add(new Smartphone("Motorola", "X3", 200, DateTime.Now.AddMonths(-36 + 4), "USA"));
        trackerList.Add(new Smartphone("Motorola", "X3", 400, DateTime.Now.AddMonths(-36 + 5), "USA"));
        trackerList.Add(new Smartphone("Motorola", "X2", 400, DateTime.Now.AddMonths(-36 + 10), "USA"));
        trackerList.Add(new Smartphone("Samsung", "Galaxy 10", 4500, DateTime.Now.AddMonths(-36 + 6), "Sweden"));
        trackerList.Add(new Smartphone("Samsung", "Galaxy 10", 4500, DateTime.Now.AddMonths(-36 + 7), "Sweden"));
        trackerList.Add(new Smartphone("Sony", "XPeria 7", 3000, DateTime.Now.AddMonths(-36 + 4), "Sweden"));
        trackerList.Add(new Smartphone("Sony", "XPeria 7", 3000, DateTime.Now.AddMonths(-36 + 5), "Sweden"));
        trackerList.Add(new Smartphone("Siemens", "Brick", 220, DateTime.Now.AddMonths(-36 + 12), "Germany"));
        trackerList.Add(new Computer("Dell", "Desktop 900", 100, DateTime.Now.AddMonths(-38), "USA"));
        trackerList.Add(new Computer("Dell", "Desktop 900", 100, DateTime.Now.AddMonths(-37), "USA"));
        trackerList.Add(new Computer("Lenovo", "X100", 300, DateTime.Now.AddMonths(-36 + 1), "USA"));
        trackerList.Add(new Computer("Lenovo", "X200", 300, DateTime.Now.AddMonths(-36 + 4), "USA"));
        trackerList.Add(new Computer("Lenovo", "X300", 500, DateTime.Now.AddMonths(-36 + 9), "USA"));
        trackerList.Add(new Computer("Dell", "Optiplex 100", 1500, DateTime.Now.AddMonths(-36 + 7), "Sweden"));
        trackerList.Add(new Computer("Dell", "Optiplex 200", 1400, DateTime.Now.AddMonths(-36 + 8), "Sweden"));
        trackerList.Add(new Computer("Dell", "Optiplex 300", 1300, DateTime.Now.AddMonths(-36 + 9), "Sweden"));
        trackerList.Add(new Computer("Asus", "ROG 600", 1600, DateTime.Now.AddMonths(-36 + 14), "Germany"));
        trackerList.Add(new Computer("Asus", "ROG 500", 1200, DateTime.Now.AddMonths(-36 + 4), "Germany"));
        trackerList.Add(new Computer("Asus", "ROG 500", 1200, DateTime.Now.AddMonths(-36 + 3), "Germany"));
        trackerList.Add(new Computer("Asus", "ROG 500", 1300, DateTime.Now.AddMonths(-36 + 2), "Germany"));

        PrintList(trackerList);
        
        bool run = true;
        while(run == true)
        {
            System.Console.WriteLine("\nPress 'A' if you want to create a new Asset, 'P' to print assets list or 'Q' if you want to quit.");
            System.Console.Write(">");
            string input = Console.ReadLine().ToUpper();

            if(input == "A")
            {
                NewAsset(trackerList);
            }
            else if(input == "P")
            {
                PrintList(trackerList);
            }
            else
            {
                run = false;
            }
        }
    }

    public static void PrintList(List<Asset> trackerList)
    {
        DateTime dateNow = DateTime.Now;
        List<Asset> trackerSorted = trackerList
            .OrderBy(asset => asset.Office)
            .ThenBy(asset => asset.PurchaseDate)
            .ToList();

        Console.WriteLine("Office".PadRight(20) +  "Asset".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Price(USD)".PadRight(20) + "Price(local)".PadRight(20) + "Purchase Date".PadRight(20) + "\n");

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

            System.Console.WriteLine(asset.Office.PadRight(20)+ asset.GetType().Name.PadRight(20) + asset.Brand.PadRight(20) + asset.Model.PadRight(20) + asset.Price.ToString().PadRight(20) + asset.LocalPrice.ToString().PadRight(20) + asset.PurchaseDate.ToString("dd-MM-yyyy").PadRight(20));
        }
    }

    public static void NewAsset(List<Asset> trackerList)
    {            
        System.Console.WriteLine("Please enter the type of asset you want to create (Computer or Smartphone):");
        System.Console.Write(">");
        string type = Console.ReadLine();
        if (!string.IsNullOrEmpty(type))
        {
            type = char.ToUpper(type[0]) + type.Substring(1).ToLower();
        }

        System.Console.WriteLine($"Please enter the brand of the {type}:");
        System.Console.Write(">");
        string brand = Console.ReadLine();

        System.Console.WriteLine($"Please enter the model of the {type}:");
        System.Console.Write(">");
        string model = Console.ReadLine();

        System.Console.WriteLine($"Please enter the price in USD of the {type}:");
        System.Console.Write(">");
        double price = Convert.ToDouble(Console.ReadLine());

        while (!double.TryParse(Console.ReadLine(), out price) || price <= 0)
        {
            Console.WriteLine("Invalid price. Please enter a numeric value greater than 0:");
            Console.Write(">");
        }

        System.Console.WriteLine($"Please enter the purchase date of the {type}:");
        System.Console.Write(">");
        string purchaseDateString = Console.ReadLine();
        DateTime purchaseDate;

        if (!DateTime.TryParseExact(purchaseDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out purchaseDate))
        {
            Console.WriteLine("Invalid purchase date format. Asset creation aborted.");
            return;
        }

        System.Console.WriteLine($"Please enter the office where the {type} is located:");
        System.Console.Write(">");
        string office = Console.ReadLine();
        if (!string.IsNullOrEmpty(type))
        {
            office = char.ToUpper(office[0]) + office.Substring(1).ToLower();
        }


        string assetTypeName = $"assetTracking.{type}";

        try
        {
            Type assetType = Type.GetType(assetTypeName, throwOnError: true);
            Asset newAsset = (Asset)Activator.CreateInstance(assetType, brand, model, price, purchaseDate, office);
            trackerList.Add(newAsset);
            Console.WriteLine($"{type} asset created successfully!");
        }
        catch (TypeLoadException)
        {
            Console.WriteLine($"Asset type '{type}' is not recognized. Please ensure it is a valid class name.");
        }
        catch (MissingMethodException)
        {
            Console.WriteLine($"The class '{type}' does not have the expected constructor.");
        }
        catch (InvalidCastException)
        {
            Console.WriteLine($"The asset created is not of type Asset.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
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

    public Asset(string brand, string model, double price, DateTime purchaseDate, string office)
    {
        Brand = brand;
        Model = model;
        Price = price;
        PurchaseDate = purchaseDate;
        Office = office;

        SetLocalPrice();
    }

    public void SetLocalPrice()
    {
        // Exchange rates: add more in case of more Office created
        double SEK = 10.3937;
        double EUR = 0.9148;
        double GBP = 0.7658;
        
        if(Office == "USA")
        {
            LocalPrice = Price;
        }
        else if(Office == "Germany" || Office == "Spain" )
        {
             LocalPrice = Math.Round(Price * EUR, 2);
        }
        else if(Office == "Sweden")
        {
            LocalPrice = Math.Round(Price * SEK, 2);
        }
        else if(Office == "UK")
        {
            LocalPrice = Math.Round(Price * GBP, 2);
        }
        else
        {
            Console.WriteLine($"Office '{Office}' does not have a corresponding currency.");
        }
    }    
}

class Computer : Asset
{
    public Computer(string brand, string model, double price, DateTime purchaseDate, string office)
        : base(brand, model, price, purchaseDate, office)
    {
            
    }
}

class Smartphone : Asset
{   
    public Smartphone(string brand, string model, double price, DateTime purchaseDate, string office)
        : base(brand, model, price, purchaseDate, office)
    {
    }
}
