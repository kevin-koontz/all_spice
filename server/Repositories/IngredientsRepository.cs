


namespace all_spice.Repositories;

public class IngredientsRepository
{
  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO
    ingredients(name, quantity, recipeId)
    VALUES(@Name, @Quantity, @RecipeId);

    SELECT * FROM ingredients WHERE id = LAST_INSERT_ID();";

    Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
  {
    string sql = @"
    SELECT
    ingredients.*,
    recipes.*
    FROM
    ingredients
    JOIN recipes ON recipes.id = ingredients.recipeId
    WHERE ingredients.recipeId = @recipeId;";

    List<Ingredient> ingredients = _db.Query<Ingredient, Recipe, Ingredient>(sql, (ingredient, recipe) =>
    {
      recipe.Id = ingredient.RecipeId;
      return ingredient;
    }, new { recipeId }).ToList();
    return ingredients;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    throw new NotImplementedException();
  }
}