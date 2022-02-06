namespace MyCosts.API.Controllers;

[Route("api/v2.0/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IProductCategoriesService _productCategoriesService;

    public CategoriesController(IProductCategoriesService productCategoriesService)
    {
        _productCategoriesService = productCategoriesService;
    }

    [HttpPost]
    public IActionResult Add([FromBody] ProductCategoryModel categoryModel)
    {
        _productCategoriesService.Add(categoryModel);
        return Ok();
    }

    [HttpGet]
    public IActionResult Get()
    {
        var productCategories = _productCategoriesService.Get().Select(pc => new ProductCategoryResponseModel(pc));
        return Ok(productCategories);
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
