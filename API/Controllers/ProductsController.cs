namespace MyCosts.API.Controllers;

[Route("api/v2.0/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpPost]
    public IActionResult Add([FromBody] ProductModel model)
    {
        _productsService.Add(model);
        return Ok();
    }

    [HttpGet]
    public IActionResult Get()
    {
        var products = _productsService.Get().Select(p => new ProductResponseModel(p));
        return Ok(products);
    }

    [HttpDelete("{id}")]
    public IActionResult Remove([FromRoute] int id)
    {
        _productsService.Remove(id);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] ProductResponseModel model)
    {
        _productsService.Update(model);
        return Ok();
    }
}
