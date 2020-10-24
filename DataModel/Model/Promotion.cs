namespace DataModel.Model
{
    using System;

    public class Promotion
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int ProductId { get; set; }

        public float Discount { get; set; }
    }
}
