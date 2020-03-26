using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalkerAPI
{
    public class Walker
    {
        public int Id { get; set; }

        public string WalkerName { get; set; }

        public int NeighborhoodId { get; set; }

        public Neighborhood Neighborhood { get; set; }

        public List<Walk> Walks { get; set; }
    }
}
