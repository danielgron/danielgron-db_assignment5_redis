using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RedisCachedApi.Services;

namespace RedisCachedApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OccupationController : ControllerBase
{

    private readonly ILogger<OccupationController> _logger;
    private readonly OccupationService _occupationService;

    public OccupationController(ILogger<OccupationController> logger, OccupationService occupationService)
    {
        _logger = logger;
        _occupationService = occupationService;
    }

    [HttpGet("api/{cached}")]
    public async Task<OccupationResult> Get(bool cached)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        var wrap = new OccupationResult();

        if (cached)
            wrap.Occupations = await _occupationService.GetOccupationsCached();

        else
            wrap.Occupations = await _occupationService.GetOccupationsUnCached();
        
        sw.Stop();
        wrap.TimeElapsed = sw.ElapsedMilliseconds;
        return wrap;


    }
}
