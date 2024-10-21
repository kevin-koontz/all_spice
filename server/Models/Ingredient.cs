using System.ComponentModel.DataAnnotations;

namespace all_spice.Models;

public class Ingredient
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  [MaxLength(255)]
  public string Name { get; set; }

  [MaxLength(255)]
  public string Quantity { get; set; }

  public int RecipeId { get; set; }
}
