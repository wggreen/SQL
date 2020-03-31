using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DogWalkerAPI
{
    public class Walker
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string WalkerName { get; set; }

        [Required]
        public int NeighborhoodId { get; set; }

        public Neighborhood Neighborhood { get; set; }

        public List<Walk> Walks { get; set; }
    }
}
