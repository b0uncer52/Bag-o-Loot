using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace BagOLoot
{
    public class Santa
    {
        private List<Toy> bag;

        public Santa()
        {
            bag = new List<Toy>();
        }
        public int AddToyToBag(string toy, int childId)
        {
            Toy newToy = new Toy(childId, toy);
            bag.Add(newToy);
            int toyId = bag.LastIndexOf(newToy);
            return toyId;
        }

        public Dictionary<int, string> GetToysInChildsBag(int childId)
        {
            Dictionary<int, string> childToys = new Dictionary<int, string>();
            foreach(Toy t in bag)
            {   
                if(t.Child == childId)
                {
                    childToys.Add(bag.IndexOf(t), t.Name);
                }
            }
            return childToys;
        }

        public void RemoveToyFromBag(int toyId)
        {
            bag.RemoveAt(toyId);
        }
        
        public List<int> ChildrenToDeliver()
        {
            List<int> children = new List<int>();
            foreach(Toy t in bag)
            {
                if(!children.Contains(t.Child)){
                    children.Add(t.Child);
                }
            }
            return children;
        }
    }

    public class Toy
    {
        private string name;
        private int child;

        public int Child
        {
            get{
                return child;
            }
        }
        public string Name
        {
            get{
                return name;
            }
        }

        public Toy (int childId, string toyName)
        {
            name = toyName;
            child = childId;
        }

    }
}