namespace MyCosts.API.Controllers;

[Route("api/v2.0/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    public CategoriesController()
    {

    }

    [HttpPost]
    public IActionResult Add()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public IActionResult Get()
    {
        throw new NotImplementedException();
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
