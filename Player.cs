using System;
using System.Linq;
using System.Collections.Generic;

namespace stanger_things
{

    class Player
    {

        public string Name;

        public int Health;

        public List<Item> PlayerItems = new List<Item>();

        public Player(string name, int health)
        {
            this.Name = name;
            this.Health = health;

        }


        public void PrintItems()
        {
            if (PlayerItems.Count > 0)
            {
                Console.WriteLine("\n");
                Console.WriteLine("-------You have--------");
                foreach (Item i in PlayerItems)
                {
                    Console.WriteLine(i.Name);
                }
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("\n");
                Console.WriteLine("-------You have--------");
                Console.WriteLine("No items");

            }

        }
        public void TakeDamage()
        {
            Console.WriteLine("\n");
            this.Health = this.Health - 5;
            Console.WriteLine("---You took 5 damage for being in the Upsidedown!---");
            Console.WriteLine("\n");

        }







    }






}