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
            var childrenWithToys = _santa.ChildrenToDeliver();
            Assert.Contains(1, childrenWithToys);
            Assert.Contains(2, childrenWithToys);
            Assert.DoesNotContain(3, childrenWithToys);
        }

        [Fact]
        public void ListAllToysByChild()
        {
            var kidsToys = _santa.GetToysInChildsBag(childId);

            Assert.IsType<Dictionary<int, string>>(kidsToys);
        }
        
    }
}
/* 
Must be able to list all children who are getting a toy.
Must be able to list all toys for a given child's name.
Must be able to set the delivered property of a child, which defaults to false to true. */