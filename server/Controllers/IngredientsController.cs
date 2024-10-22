namespace all_spice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider, RecipesService recipesService)
  {
    _ingredientsService = ingredientsService;
    _auth0Provider = auth0Provider;
  }
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth0Provider;

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData, userInfo);
      return Ok(ingredient);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{ingredientId}")]
  public async Task<ActionResult<Ingredient>> DeleteIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      _ingredientsService.DeleteIngredient(ingredientId, userInfo.Id);
      return Ok("Ingredients deleted!");
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}