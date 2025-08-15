using Microsoft.AspNetCore.Mvc;
using MxFace.Fingerprint.Shared.Models;
//using MxFace.SDK.Fingerprint.Interfaces;

namespace MxFace.Fingerprint.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FingerprintController : ControllerBase
{
    //private readonly ISearch _searchService;

    //public FingerprintController(ISearch searchService)
    //{
    //    _searchService = searchService;
    //}

    //[HttpPost("enroll")]
    //public async Task<IActionResult> Enroll([FromBody] EnrollRequest request)
    //{
    //    var result = await _searchService.Enroll(
    //        request.TemplateData,
    //        request.PersonId,
    //        request.Group
    //    );
    //    return Ok(result);
    //}

    //[HttpPost("search")]
    //public async Task<IActionResult> Search([FromBody] SearchRequest request)
    //{
    //    var result = await _searchService.Search(request.TemplateData, request.Group);
    //    return Ok(result);
    //}

    //[HttpPost("verify")]
    //public async Task<IActionResult> Verify([FromBody] MatchRequest request)
    //{
    //    var result = await _searchService.Verify(request.SourceTemplate, request.TargetTemplate);
    //    return Ok(result);
    //}
}
