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
    public IActionResult Add()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public IActionResult Get()
    {
        List<Product> products = _productsService.Get();
        return Ok(products);
    }

    [HttpDelete("{id}")]
    public IActionResult Remove([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public IActionResult Update()
    {
        throw new NotImplementedException();
    }
}
