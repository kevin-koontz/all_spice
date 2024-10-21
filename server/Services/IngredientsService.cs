
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
}