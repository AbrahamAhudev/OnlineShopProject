﻿namespace OnlineShopProject.Server.DTOs
{
    public class ProductDTO
    {

        public string Name { get; set; }

        public string? Description { get; set; }

        public int? UserId {  get; set; } 

        public decimal Price { get; set; }

        public IFormFile? Image { get; set; }
    }
}
