



namespace all_spice.Repositories;

public class RecipesRepository
{
  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO
    recipes(title, instructions, img, category, creatorId)
    VALUES(@Title, @Instructions, @Img, @Category, @CreatorId);

    SELECT
    recipes.*,
    accounts.*
    FROM recipes
    JOIN accounts ON recipes.creatorId = accounts.Id
    WHERE recipes.id = LAST_INSERT_ID();";

    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, recipeData).FirstOrDefault();
    return recipe;

  }

  internal List<Recipe> GetAllRecipes()
  {
    string sql = @"
    SELECT
    recipes.*,
    accounts.*
    FROM recipes
    JOIN accounts ON recipes.creatorId = accounts.id;";

    List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, JoinCreatorToRecipe).ToList();
    return recipes;
  }

  private Recipe JoinCreatorToRecipe(Recipe recipe, Profile profile)
  {
    recipe.Creator = profile;
    return recipe;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
    SELECT
    recipes.*,
    accounts.*
    FROM recipes
    JOIN accounts ON recipes.creatorId = accounts.id
    WHERE recipes.id = @recipeId;";

    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, JoinCreatorToRecipe, new { recipeId }).FirstOrDefault();
    return recipe;
  }

  internal void UpdateRecipe(Recipe recipe)
  {
    throw new NotImplementedException();
  }
}