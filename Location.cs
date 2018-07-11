using System;
using System.Linq;
using System.Collections.Generic;

namespace stanger_things
{

    class Location
    {

        public string Name;

        public Item UnlockItem = new Item();
        public List<Item> locationItems = new List<Item>();
        public List<Item> upsideItems = new List<Item>();

        public List<Location> connectedLocations = new List<Location>();
        public List<Location> upsideLocations = new List<Location>();
        Item labKey = new Item();
        public Location()
        {
            this.Name = "The Laboratory";
            this.UnlockItem = labKey;
        }


        public Location(string name, Item unlockItem)
        {
            this.Name = name;
            this.UnlockItem = unlockItem;
        }

        public void PrintItems()
        {

            if (locationItems.Count > 0)
            {

                foreach (Item i in locationItems)
                {
                    Console.WriteLine(i.Name);
                }
            }
            else
            {
                Console.WriteLine("---NOTHING---");
            }

        }
        public void PrintUpsideItems()
        {
            if (upsideItems.Count > 0)
            {

                foreach (Item i in upsideItems)
                {
                    Console.WriteLine(i.Name);
                }
            }
            else
            {
                Console.WriteLine("---NOTHING---");
            }
        }
        public void PrintConnectedLocations()
        {
            Console.WriteLine("\n");

            Console.WriteLine("--------Connected Locations:-------- ");
            foreach (Location i in connectedLocations)
            {

                Console.WriteLine(i.Name);
            }
            Console.WriteLine("\n");
        }


        public void PrintUpsideLocations()
        {
            Console.WriteLine("\n");

            Console.WriteLine("--------Connected Locations-------- ");
            foreach (Location i in upsideLocations)
            {
                Console.WriteLine(i.Name);
            }

        }

    }






}