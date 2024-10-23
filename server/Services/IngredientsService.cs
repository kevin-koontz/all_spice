

namespace all_spice.Services;

public class IngredientsService
{
  public IngredientsService(IngredientsRepository repository, RecipesService recipesService)
  {
    _repository = repository;
    _recipesService = recipesService;
  }
  private readonly IngredientsRepository _repository;
  private readonly RecipesService _recipesService;

  internal Ingredient CreateIngredient(Ingredient ingredientData, Account userInfo)
  {

    Recipe recipe = _recipesService.GetRecipeById(ingredientData.RecipeId);

    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception("Cannot add ingredients to a recipe you did not create!");
    }

    Ingredient ingredient = _repository.CreateIngredient(ingredientData);
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
  {
    List<Ingredient> ingredients = _repository.GetIngredientsByRecipe(recipeId);
    return ingredients;
  }

  private Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repository.GetIngredientById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception($"Invalid ingredient id: {ingredientId}");
    }
    return ingredient;
  }

  internal void DeleteIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    Recipe recipe = _recipesService.GetRecipeById(ingredient.RecipeId);

    if (recipe.CreatorId != userId)
    {
      throw new Exception($"Cannot delete ingredients to a recipe you did not create!");
    }

    if (ingredient.RecipeId != recipe.Id)
    {
      throw new Exception($" {ingredientId} does NOT belong to this recipe! Delete request denied!!!");
    }

    _repository.DeleteIngredient(ingredientId);
  }
}