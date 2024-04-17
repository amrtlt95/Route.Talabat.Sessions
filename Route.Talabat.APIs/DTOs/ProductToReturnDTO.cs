﻿using Route.Talabat.Core.Entities.Product;

namespace Route.Talabat.APIs.DTOs
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        public string Brand { get; set; } = null!;

        public int CategoryId { get; set; }
        public string Category { get; set; } = null!;

    }
}
