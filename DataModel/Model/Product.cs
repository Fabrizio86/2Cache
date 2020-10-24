namespace DataModel.Model
{
    using System.Collections.Generic;

    public class Product
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int Quantity { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public ICollection<Promotion> Promotions { get; set; }

    }
}
