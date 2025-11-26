using Application.Common.Results;
using Application.Interfaces;
using Application.Statistics.DTOs;
using Application.Statistics.Queries;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace WebApi.Controllers.Admin;

[ApiController]
[Route("api/statistics")]
public class StatisticsController(
    IQueryHandler<GetStatisticQuery, Result<GetStatisticDto>> queryHandler,
    IQueryHandler<GetAvarageSectionStatisticQuery, Result<List<AvarageSectionStatisticDto>>> statisticSectionQueryHandler) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await queryHandler.HandleAsync(new GetStatisticQuery());
        
        if (!result.IsSuccess)
        {
            return HandleError(result);
        }
        
        return Ok(result);
    }

    [HttpGet("section/avarage")]
    public async Task<IActionResult> GetStatisticAsync()
    {
        var result= await statisticSectionQueryHandler.HandleAsync(new GetAvarageSectionStatisticQuery());
        if (!result.IsSuccess)
        {
            return HandleError(result);
        }
        
        return Ok(result);
    }
    
    private IActionResult HandleError<T>(Result<T> result)
    {
        return result.ErrorType switch
        {
            ErrorType.NotFound => NotFound(new { error = result.Message }),
            ErrorType.Validation => BadRequest(new { error = result.Message }),
            ErrorType.Conflict => Conflict(new { error = result.Message }),
            ErrorType.Internal => StatusCode(500, new { error = result.Message }),
            _ => StatusCode(500, new { error = result.Message ?? "Unhandled error" })
        };
    }
}