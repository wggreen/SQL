using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogWalkerAPI
{
    public class Dog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string DogName { get; set; }

        [Required]
        public int DogOwnerId { get; set; }

        public Owner DogOwner { get; set; }

        public string Breed { get; set; }

        public string Notes { get; set; }
    }
}
