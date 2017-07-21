using System;
using System.Collections.Generic;

namespace BagOLoot
{
    public class Menus
    {
        ChildRegistry registry;
        Santa santa;

        public Menus()
        {
            registry = new ChildRegistry();
            santa = new Santa();
        }
        public void MainMenu()
        {
            Console.WriteLine ("WELCOME TO THE BAG O' LOOT SYSTEM");
            Console.WriteLine ("*********************************");
            Console.WriteLine ("1. Register a Child");
            Console.WriteLine ("2. Assign toy to a child");
            Console.WriteLine ("3. Revoke toy from child");
            Console.WriteLine ("4. Review child's toy list");
            Console.WriteLine ("5. Child toy delivery complete");
            Console.WriteLine ("6. Yuletime Delivery Report");
            Console.WriteLine ("7. Exit Program");
            Console.Write("> ");

            int choice;
            Int32.TryParse (Console.ReadLine(), out choice);

            if (choice == 1) {
                RegisterChildMenu();
            }else if (choice == 2) {
                AssignToyMenu();
            }else if (choice == 3) {
                RevokeToyMenu();
            }else if (choice == 4) {
                ReviewChildsListMenu();
            }else if (choice == 5) {
                CompleteDeliveryMenu();
            }else if (choice == 6) {
                DeliveryReportMenu();
            }else if (choice == 7) {
                
            }else {
                Console.WriteLine("Invalid Entry, try again.");
                Console.Clear();
                MainMenu();
            }
        }

        private void DeliveryReportMenu()
        {
            List<(int, string, string)> report = santa.DeliveryReport();
            int i = 0;
            int c = 0;
            foreach((int id, string name, string toy) in report)
            {
                if(i != id){
                    Console.WriteLine(name);
                    i = id;
                    c = 1;
                }
                Console.WriteLine($"{c}. {toy}");
                c++;
            }
            Console.WriteLine("Press any key to return to main menu");
            Console.Write("> ");
            Console.ReadLine();
            Console.Clear();
            MainMenu();
        }

        private void CompleteDeliveryMenu()
        {
            Console.Clear();
            Console.WriteLine("Which child had all of their toys delivered?");
            Dictionary<int, string> kids = registry.GetChildren();
            int c = 0;
            List<int> kidKeys = new List<int>();
            foreach(int k in kids.Keys)
            {
                c++;
                kidKeys.Add(k);
                Console.WriteLine($"{c}. {kids[k]}");
            }
            Console.Write("> ");
            int choice;
            Int32.TryParse (Console.ReadLine(), out choice);
            choice = kidKeys[choice - 1];
            santa.DeliverToChild(choice);
            Console.Clear();
            MainMenu();
        }

        private void ReviewChildsListMenu()
        {
            Console.Clear();
            Console.WriteLine("View Bag o' Loot for which child?");
            Dictionary<int, string> kids = registry.GetChildren();
            int c = 0;
            List<int> kidKeys = new List<int>();
            foreach(int k in kids.Keys)
            {
                c++;
                kidKeys.Add(k);
                Console.WriteLine($"{c}. {kids[k]}");
            }
            Console.Write("> ");
            int choice;
            Int32.TryParse (Console.ReadLine(), out choice);
            choice = kidKeys[choice - 1];
            Console.WriteLine($"{kids[choice]}'s Bag o' Loot");
            Dictionary<int, string> toys = santa.GetToysInChildsBag(choice);
            c = 0;
            List<int> toyKeys = new List<int>();
            foreach(int k in toys.Keys)
            {
                c++;
                toyKeys.Add(k);
                Console.WriteLine($"{c}. {toys[k]}");
            }
            Console.WriteLine("Press any key to return to main menu");
            Console.Write("> ");
            Console.ReadLine();
            Console.Clear();
            MainMenu();
        }

        private void RevokeToyMenu()
        {
            Console.Clear();
            Console.WriteLine("Revoke toy from which child?");
            Dictionary<int, string> kids = registry.GetChildren();
            int c = 0;
            List<int> kidKeys = new List<int>();
            foreach(int k in kids.Keys)
            {
                c++;
                kidKeys.Add(k);
                Console.WriteLine($"{c}. {kids[k]}");
            }
            Console.Write("> ");
            int choice;
            Int32.TryParse (Console.ReadLine(), out choice);
            choice = kidKeys[choice - 1];
            Console.WriteLine($"Choose toy to revoke from {kids[choice]}'s Bag o' Loot");
            Dictionary<int, string> toys = santa.GetToysInChildsBag(choice);
            c = 0;
            List<int> toyKeys = new List<int>();
            foreach(int k in toys.Keys)
            {
                c++;
                toyKeys.Add(k);
                Console.WriteLine($"{c}. {toys[k]}");
            }
            Console.Write("> ");
            Int32.TryParse (Console.ReadLine(), out choice);
            choice = toyKeys[choice - 1];
            santa.RemoveToyFromBag(choice);
            Console.Clear();
            MainMenu();
        }

        public void RegisterChildMenu()
        {
            Console.WriteLine("Enter child's name: ");
            Console.Write("> ");
            string newName = Console.ReadLine().ToString();
            bool success = registry.AddChild(newName);
            Console.Clear();
            if(success){
                Console.WriteLine($"{newName} successfully added to registry");
            } else {
                Console.WriteLine("Register child unsuccessful");
            }
            MainMenu();
        }

        public void AssignToyMenu()
        {
            Console.Clear();
            Console.WriteLine("Assign toy to which child?");
            Dictionary<int, string> kids = registry.GetChildren();
            int c = 0;
            List<int> keys = new List<int>();
            foreach(int k in kids.Keys)
            {
                c++;
                keys.Add(k);
                Console.WriteLine($"{c}. {kids[k]}");
            }
            Console.Write("> ");
            int choice;
            Int32.TryParse (Console.ReadLine(), out choice);
            choice = keys[choice - 1];
            Console.WriteLine($"Enter toy to add to {kids[choice]}'s Bag o' Loot ");
            Console.Write("> ");
            string toyName = Console.ReadLine().ToString();
            santa.AddToyToBag(toyName, choice);
            Console.Clear();
            Console.WriteLine($"{toyName} successfully added to {kids[choice]}'s bag");
            MainMenu();
        }
    }
}