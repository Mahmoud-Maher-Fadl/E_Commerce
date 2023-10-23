using Mapster;

namespace Application.Item.Dto;

public class ItemDto:IRegister
{
    public string ArabicName { get; set; }
    public string EnglishName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string CategoryId { get; set; }
    public string CategoryArabicName { get; set; }
    public string CategoryEnglishName { get; set; }
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Model.Item.Item, ItemDto>()
            .Map(dest => dest.CategoryArabicName, src => src.Category.ArabicName)
            .Map(dest => dest.CategoryEnglishName, src => src.Category.EnglishName);
    }
}