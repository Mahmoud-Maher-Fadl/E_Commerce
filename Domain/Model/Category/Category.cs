using Domain.common;

namespace Domain.Model.Category;

public class Category:BaseEntity
{
    public string ArabicName { get; set; }
    public string EnglishName { get; set; }  
    public HashSet<Item.Item> Items = new HashSet<Item.Item>();
}