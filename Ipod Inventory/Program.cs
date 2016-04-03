using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipod_Inventory
{
    public class Inventory
    {
        public Inventory(string countryName, int storeCapacity, int unitPrice)
        {
            this.CountryName = countryName;
            this.AvailableStock = storeCapacity;
            this.UnitPrice = unitPrice;
        }
        public string CountryName
        {
            set; get;
        }
        public int AvailableStock
        {
            set; get;
        }
        public int UnitPrice { set; get; }

        public void ResetStock()
        {
            this.AvailableStock = 100;
        }
        public int GetTotalCost(int numberOfUnits, string countryName)
        {
            int totalCost = 0;
            try
            {
                if (countryName.Equals(this.CountryName))
                {
                    totalCost = numberOfUnits * this.UnitPrice;
                }
                else
                    totalCost = numberOfUnits * this.UnitPrice + ((numberOfUnits / 10) * 400);
            }
            catch (ArithmeticException arithExec) { Console.WriteLine(arithExec.Message); }
            return totalCost;
        }

    }
    public class Order
    {
        public string OrderLocation { set; get; }
        public Inventory Brazil { set; get; }
        public Inventory Argentina { set; get; }
        public int NumberOfUnits { set; get; }

        public void New(int numberOfUnits, string location)
        {
            try
            {
                this.NumberOfUnits = numberOfUnits;
                this.OrderLocation = location;
                if (!(location.Equals(this.Argentina.CountryName) || location.Equals(this.Brazil.CountryName)))
                {
                    Console.WriteLine("Enter Correct Location (Country)");
                    New(numberOfUnits, Console.ReadLine());
                }
                if (Brazil != null)
                    Brazil.ResetStock();
                if (Argentina != null)
                    Argentina.ResetStock();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public int GenerateBill()
        {
            int brazilTempPrice = 0;
            int argentinaTempPrice = 0;
            int remainingStock = 0;
            try
            {
                if (this.NumberOfUnits >= (Argentina.AvailableStock + Brazil.AvailableStock))
                    return 0;

                if (this.NumberOfUnits > 100)
                {
                    remainingStock = NumberOfUnits - 100;
                    brazilTempPrice = Brazil.GetTotalCost(100, this.OrderLocation) + Argentina.GetTotalCost(remainingStock, this.OrderLocation);
                    argentinaTempPrice = Argentina.GetTotalCost(100, this.OrderLocation) + Brazil.GetTotalCost(remainingStock, this.OrderLocation);
                }
                else
                {
                    brazilTempPrice = Brazil.GetTotalCost(NumberOfUnits, this.OrderLocation);
                    argentinaTempPrice = Argentina.GetTotalCost(NumberOfUnits, this.OrderLocation);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            if (brazilTempPrice > argentinaTempPrice)
                return argentinaTempPrice;
            else
                return brazilTempPrice;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            string orderloc = string.Empty;
            Order myNewOrder = null;
            try
            {
                myNewOrder = new Order();
                myNewOrder.Brazil = new Inventory("Brazil", 100, 100);
                myNewOrder.Argentina = new Inventory("Argentina", 100, 50);
                do
                {
                    Console.Clear();
                    Console.WriteLine("Enter number of units to be purchased: - ");
                    num = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter your address(Country): - ");
                    orderloc = Console.ReadLine();

                    myNewOrder.New(num, orderloc);
                    if (myNewOrder.GenerateBill() != 0)
                        Console.WriteLine("Total Cost = " + myNewOrder.GenerateBill());
                    else
                        Console.WriteLine("Out of Stock!!!!");
                    Console.WriteLine("Press 1 to continue and X for Exit.");
                    orderloc = Console.ReadLine();
                } while (orderloc.Equals("1"));
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
