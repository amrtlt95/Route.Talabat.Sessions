using Route.Talabat.Core.Entities.Basket;
using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } = null!;

        public List<BasketItemDto> Items { get; set; } = null!;
    }
}
