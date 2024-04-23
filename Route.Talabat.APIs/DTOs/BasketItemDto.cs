﻿using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;

        [Required]
        public string PictureUrl { get; set; } = null!;

        [Required]
        [Range(0.1,double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public string Brand { get; set; } = null!;

        [Required]
        public string Category { get; set; } = null!;
    }
}