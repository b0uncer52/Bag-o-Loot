using System;
using System.Collections.Generic;
using Xunit;

namespace BagOLoot.Tests
{
    public class SantaShould
    {
        private Santa _santa;
        private string toyName;
        private int childId;
        public SantaShould()
        {   
            _santa = new Santa();
            toyName = "Skateboard";
            childId = 1;
        }  

        [Fact]
        public void AddAToyToChildsBag()
        {            
            int toyId = _santa.AddToyToBag(toyName, childId);
            Dictionary<int, string> toyList = _santa.GetToysInChildsBag(childId);
            Console.WriteLine("error 1: " + toyList[toyId] + ": " + toyName);

            Assert.Equal(toyList[toyId], toyName);
        }

        [Fact]
        public void RemoveToyFromChildsBag()
        {
            int toyId = _santa.AddToyToBag(toyName, childId);
            _santa.RemoveToyFromBag(toyId);
            Dictionary<int, string> toyList = _santa.GetToysInChildsBag(childId);
            
            Assert.True(!toyList.ContainsKey(toyId));
        }

        [Fact]
        public void ListChildrenReceivingAToy()
        {
            int toyId = _santa.AddToyToBag(toyName, childId);
            int toyId2 = _santa.AddToyToBag("Ball", 2);
            Dictionary<int, string> childrenWithToys = _santa.ChildrenToDeliver();
            Assert.Equal(childrenWithToys[2], "Sue");
            Assert.Equal(childrenWithToys[1], "Billy");
            Assert.True(!childrenWithToys.ContainsKey(3));
        }

        [Fact]
        public void ListAllToysByChild()
        {
            var kidsToys = _santa.GetToysInChildsBag(childId);

            Assert.IsType<Dictionary<int, string>>(kidsToys);
        }

        [Fact]
        public void DeliveryToysToChild()
        {
            bool delivered = _santa.DeliverToChild(childId);

            Assert.True(delivered);
        }
    }
}