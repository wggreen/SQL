using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalkerApp
{
    public class Owner
    {
        public int Id { get; set; }

        public string DogOwnerName { get; set; }

        public string DogOwnerAddress { get; set; }

        public int NeighborhoodId { get; set; }

        public string Phone { get; set; }

        public Neighborhood Neighborhood { get; set; }
    }
}
