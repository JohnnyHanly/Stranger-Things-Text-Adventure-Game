using System;
using System.Collections.Generic;

namespace stanger_things
{
    class Program
    {
        static void Main(string[] args)
        {
            bool gameOver = false;
            int round = 0;
            bool upsideDown = false;
            Location currentLocation = new Location(null, null);
            List<Location> allLocations = new List<Location>();
            List<Item> allItems = new List<Item>();
            List<String> userOptions = new List<String>();
            userOptions.Add("View Connected Locations");
            userOptions.Add("Move to Anoter Location");
            userOptions.Add("View Location Items");
            userOptions.Add("Pick Up Location Item");
            userOptions.Add("View My Items");
            userOptions.Add("Drop Item");
            userOptions.Add("Move to the Upside Down");


            Console.WriteLine("--------Welcome to Harkins, Indiana you little adventurer!--------");
            Console.WriteLine("Before you stary your journey, tell us your name!");
            string name = Console.ReadLine().Trim();
            //creating the player and enemy
            Player player = new Player(name, 100);
            Player demigorgon = new Player("DEMIGORGON", 100);

            //initializing the Lab
            Item houseKey = new Item("House Key");
            Item glasses = new Item("Barb's Glasses");
            Location laboratory = new Location("The Laboratory", null);
            laboratory.locationItems.Add(houseKey);
            laboratory.upsideItems.Add(glasses);
            allLocations.Add(laboratory);
            allItems.Add(houseKey);
            allItems.Add(glasses);


            //intitializing Mike's house
            Item baseballBat = new Item("Baseball Bat");
            Item christmasLights = new Item("Christmas Lights");
            Location mikesHouse = new Location("Mikes House", houseKey);
            mikesHouse.locationItems.Add(baseballBat);
            mikesHouse.upsideItems.Add(christmasLights);
            allLocations.Add(mikesHouse);
            allItems.Add(baseballBat);
            allItems.Add(christmasLights);

            //initializing the High School
            Item demigorgonStuff = new Item("Demigorgon Stuff");
            Location highSchool = new Location("Hawkins High School", baseballBat);
            highSchool.upsideItems.Add(demigorgonStuff);
            allLocations.Add(highSchool);
            allItems.Add(demigorgonStuff);

            //initializing the Demigorgon Lair
            Item demigorgonHeart = new Item("Demigorgon Heart");
            Location demigorgonLair = new Location("Demigorgon Lair", demigorgonStuff);
            demigorgonLair.locationItems.Add(demigorgonHeart);
            allLocations.Add(demigorgonLair);
            allItems.Add(demigorgonHeart);

            //connecting the locations
            laboratory.connectedLocations.Add(mikesHouse);
            laboratory.upsideLocations.Add(highSchool);
            laboratory.upsideLocations.Add(demigorgonLair);

            mikesHouse.connectedLocations.Add(laboratory);
            mikesHouse.connectedLocations.Add(highSchool);

            highSchool.connectedLocations.Add(mikesHouse);
            highSchool.upsideLocations.Add(laboratory);

            demigorgonLair.upsideLocations.Add(laboratory);

            Console.WriteLine($"Your adventure begins! Good Luck,{player.Name}!");
            currentLocation = laboratory;
            while (!gameOver)
            {
                if (player.Health <= 0)
                {
                    gameOver = true;
                    Console.WriteLine("YOU DIED. GAME OVER!");
                }
                round++;
                Console.WriteLine($"--------Round {round}--------");
                Console.WriteLine($"You are currently in {currentLocation.Name} with {player.Health} health");
                Console.WriteLine($"What would you like to?");
                Console.WriteLine("\n");
                Console.WriteLine("----------------------------------------------");
                int optionsCount = 7;
                int selected = 0;
                bool done = false;

                while (!done)
                {
                    for (int i = 0; i < optionsCount; i++)
                    {
                        if (selected == i)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("> ");
                        }
                        else
                        {
                            Console.Write("  ");
                        }

                        Console.WriteLine($"{userOptions[i]}");

                        Console.ResetColor();
                    }

                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            selected = Math.Max(0, selected - 1);
                            break;
                        case ConsoleKey.DownArrow:
                            selected = Math.Min(optionsCount - 1, selected + 1);
                            break;
                        case ConsoleKey.Enter:
                            done = true;
                            break;
                    }

                    if (!done)
                        Console.CursorTop = Console.CursorTop - optionsCount;
                }
                selected = selected + 1;
                Console.WriteLine("----------------------------------------------");

                string switchString = selected.ToString();
                switch (switchString)
                {

                    case "1":
                        if (!upsideDown)
                        {
                            currentLocation.PrintConnectedLocations();

                        }
                        else
                        {
                            currentLocation.PrintUpsideLocations();
                            player.TakeDamage();

                        }
                        break;
                    case "2":

                        if (!upsideDown)
                        {
                            currentLocation = ChangeLocations(currentLocation.connectedLocations, currentLocation, player);

                        }
                        else
                        {

                            currentLocation = ChangeLocations(currentLocation.upsideLocations, currentLocation, player);
                            player.TakeDamage();
                        }

                        break;

                    case "3":
                        if (!upsideDown)
                        {
                            Console.WriteLine($"\n-----Items in {currentLocation.Name}-----");
                            currentLocation.PrintItems();
                            Console.WriteLine("\n");
                        }
                        else

                        {

                            Console.WriteLine($"\n-----Items in {currentLocation.Name}-----");
                            currentLocation.PrintUpsideItems();
                            player.TakeDamage();
                            Console.WriteLine("\n");

                        }

                        break;
                    case "4":

                        if (player.PlayerItems.Count < 2)
                        {

                            if (!upsideDown)
                            {
                                Item itemToPickup = new Item();
                                itemToPickup = PickUpItem(currentLocation.locationItems);

                                if (itemToPickup != null)
                                {
                                    player.PlayerItems.Add(itemToPickup);
                                    currentLocation.locationItems.Remove(itemToPickup);
                                    if (itemToPickup.Name == "Demigorgon Heart")
                                    {
                                        Console.WriteLine($"\nVICTORY! YOU BRUTALLY MURDERED THE DEMIGORGON!");
                                        gameOver = true;
                                    }

                                }

                            }
                            else
                            {
                                Item itemToPickup = new Item();
                                itemToPickup = PickUpItem(currentLocation.upsideItems);

                                if (itemToPickup != null)
                                {
                                    player.PlayerItems.Add(itemToPickup);
                                    currentLocation.upsideItems.Remove(itemToPickup);

                                    if (itemToPickup.Name == "Demigorgon Heart")
                                    {
                                        Console.WriteLine($"\nVICTORY! YOU BRUTALLY MURDERED THE DEMIGORGON!");
                                        gameOver = true;
                                    }
                                }
                                player.TakeDamage();

                            }
                        }
                        else
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("You can only carry 2 items at once. Drop something!");
                        }

                        break;

                    case "5":


                        if (!upsideDown)
                        {
                            if (player.PlayerItems.Count > 0)
                                player.PrintItems();
                        }
                        else
                        {
                            player.PrintItems();
                            player.TakeDamage();
                        }
                        break;
                    case "6":

                        if (!upsideDown)
                        {
                            Item itemToDrop = new Item();
                            itemToDrop = DropItem(player.PlayerItems, player, currentLocation);

                            if (itemToDrop != null)
                            {
                                player.PlayerItems.Remove(itemToDrop);
                                currentLocation.locationItems.Add(itemToDrop);

                            }
                        }
                        else
                        {
                            Item itemToDrop = new Item();
                            itemToDrop = DropItem(player.PlayerItems, player, currentLocation);
                            if (itemToDrop != null)
                            {
                                player.PlayerItems.Remove(itemToDrop);
                                currentLocation.upsideItems.Add(itemToDrop);
                            }
                        }
                        break;

                    case "7":

                        if (upsideDown)
                        {
                            upsideDown = false;
                            Console.WriteLine("\nXXXXXXXX  You have left the UpsideDown  XXXXXXXX\n");
                        }
                        else
                        {
                            upsideDown = true;
                            Console.WriteLine("\nXXXXXXXX  YOU HAVE ENTERED THE UPSIDE DOWN  XXXXXXXX");
                            Console.WriteLine("YOU NOW TAKE 5 DAMAGE EVERY TURN\n");
                        }
                        break;
                }
            }
        }
        public static Location ChangeLocations(List<Location> locations, Location currentLocation, Player player)
        {
            Console.WriteLine("--------Choose the location you wish to travel to--------");

            String menuItem = DisplayLocationList(locations);

            bool hasKey = false;
            foreach (Location i in locations)
            {
                if (menuItem == i.Name)
                {
                    foreach (Item j in player.PlayerItems)
                    {
                        if (j == i.UnlockItem)
                        {
                            currentLocation = i;
                            hasKey = true;
                            Console.WriteLine($"\nYou unlocked {currentLocation.Name} with your {currentLocation.UnlockItem.Name} and have traveled there\n");
                        }
                        if (i.UnlockItem == null)
                        {
                            hasKey = true;
                            currentLocation = i;

                            Console.WriteLine($"\nYou traveled back to The Laboratory\n");

                        }

                    }
                    if (!hasKey)
                    {
                        Console.WriteLine($"\n-------You don't have the correct item to enter {menuItem}--------\n");

                    }
                }
                else if (!hasKey)
                {
                    Console.WriteLine("\n--------Location not found.--------\n");
                }
            }
            return currentLocation;
        }
        public static Item PickUpItem(List<Item> LocationItems)
        {
            {
                Item itemToPickup = new Item();
                bool itemFound = false;
                Console.WriteLine("--------Choose an Item to Pick up--------");

                string menuItem = DisplayItemList(LocationItems);
                foreach (Item i in LocationItems)
                {
                    if (i.Name == menuItem)
                    {
                        itemToPickup = i;
                        itemFound = true;
                        Console.WriteLine($"\n--------You have picked up {i.Name}--------\n");
                    }

                }
                if (!itemFound)
                {
                    return null;
                }
                else
                {
                    return itemToPickup;
                }
            }
        }
        public static Item DropItem(List<Item> items, Player player, Location currentLocation)
        {

            Console.WriteLine("\n--------Choose an Item to Drop--------");

            string menuItem = DisplayItemList(items);
            bool dropItemfound = false;

            Item drop = new Item();
            foreach (Item i in player.PlayerItems)
            {
                if (i.Name == menuItem)
                {
                    dropItemfound = true;
                    drop = i;
                    Console.WriteLine($"\n--------You have removed {i.Name} at {currentLocation.Name}--------\n");
                }
            }
            if (!dropItemfound)
            {
                Console.WriteLine("--------Item not found--------");
                return null;
            }
            else
            {
                return drop;
            }
        }
        public static string DisplayLocationList(List<Location> list)
        {
            int connectedLocationsCount = list.Count;
            int locationSelected = 0;
            bool locationsDone = false;

            while (!locationsDone)
            {
                for (int i = 0; i < connectedLocationsCount; i++)
                {
                    if (locationSelected == i)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }

                    Console.WriteLine($"{list[i].Name}");

                    Console.ResetColor();
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        locationSelected = Math.Max(0, locationSelected - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        locationSelected = Math.Min(connectedLocationsCount - 1, locationSelected + 1);
                        break;
                    case ConsoleKey.Enter:
                        locationsDone = true;
                        break;
                }

                if (!locationsDone)
                    Console.CursorTop = Console.CursorTop - connectedLocationsCount;
            }
            return list[locationSelected].Name;
        }
        public static string DisplayItemList(List<Item> list)

        {
            int itemsCount = list.Count;
            int locationItemSelected = 0;
            bool locationsItemsDone = false;
            Console.WriteLine("\n");
            if (list.Count <= 0)
            {
                Console.WriteLine("--------No items found--------");
                Console.WriteLine("\n");

                return null;
            }
            while (!locationsItemsDone)
            {
                for (int i = 0; i < itemsCount; i++)
                {
                    if (locationItemSelected == i)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }

                    Console.WriteLine($"{list[i].Name}");

                    Console.ResetColor();
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        locationItemSelected = Math.Max(0, locationItemSelected - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        locationItemSelected = Math.Min(itemsCount - 1, locationItemSelected + 1);
                        break;
                    case ConsoleKey.Enter:
                        locationsItemsDone = true;
                        break;
                }

                if (!locationsItemsDone)
                {
                    Console.CursorTop = Console.CursorTop - itemsCount;
                }

            }
            return list[locationItemSelected].Name;

        }
    }
}
