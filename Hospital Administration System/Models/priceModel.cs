using System.Collections.Generic;

public class MealItem
{
    public string Name { get; set; }
    public string Category { get; set; } // "Breakfast" or "Lunch"
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public bool Selected { get; set; }
}

public class priceModel
{
    public List<MealItem> Meals { get; set; }
    public decimal? CurrentPrice { get; set; } = 0; // nullable with default
}

