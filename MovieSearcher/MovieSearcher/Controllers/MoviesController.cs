using Microsoft.AspNetCore.Mvc;
using MovieSearcher.Services;

namespace MovieSearcher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoveSearchOmdb _searchService;
        public MoviesController(IMoveSearchOmdb searchService) 
        {
            _searchService = searchService; 
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string title)
        {
            return Ok(await _searchService.SearchByTitleAsync(title));
        }
    }
}
