namespace API_TEST.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Product_Name { get; set; }
        public string Description { get; set; }
        public decimal Freight { get; set; }
        public decimal Custom { get; set; }
        public decimal Repair { get; set; }
        public decimal PriceSell { get; set; }
        public decimal PricePurchase { get; set; }
    }
}