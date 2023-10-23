using Domain.common;

namespace Domain.Model.Item;

public class Item:BaseEntity
{
    public string ArabicName { get; set; }
    public string EnglishName { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Category.Category Category { get; set; }
}