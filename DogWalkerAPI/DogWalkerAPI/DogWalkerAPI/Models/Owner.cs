using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalkerAPI
{
    public class Owner
    {
        public int Id { get; set; }

        public string DogOwnerName { get; set; }

        public string DogOwnerAddress { get; set; }

        public int NeighborhoodId { get; set; }

        public Neighborhood Neighborhood { get; set; }

        public string Phone { get; set; }

        public List<Dog> Dogs { get; set; }

    }
}
