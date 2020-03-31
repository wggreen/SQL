﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DogWalkerAPI
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string DogOwnerName { get; set; }

        [Required]
        public string DogOwnerAddress { get; set; }

        [Required]
        public int NeighborhoodId { get; set; }

        public Neighborhood Neighborhood { get; set; }

        [RegularExpression("^[01]?[- .]?\\(?[2-9]\\d{2}\\)?[- .]?\\d{3}[- .]?\\d{4}$",
        ErrorMessage = "Phone is required and must be properly formatted.")]
        public string Phone { get; set; }

        public List<Dog> Dogs { get; set; }

    }
}
