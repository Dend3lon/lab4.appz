using AutoMapper;
using BusinessLogic.Services;
using BusinessModels;
using DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace WebAppAnticafe.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivityController : ControllerBase
{
    private readonly ActivityService _activityService;
    private readonly IMapper _mapper;
    public ActivityController(IMapper mapper, ActivityService activityService)
    {
        _activityService = activityService;
        _mapper = mapper;
    }
    [HttpPost("CreateActivity")]
    public IActionResult CreateActivity([FromBody] ActivityDto activityDto)
    {
        var activity = _mapper.Map<ActivityBusinessModel>(activityDto);
        _activityService.CreateActivity(activity);
        return Ok();
    }
    [HttpGet("GetAllActivities")]
    public IActionResult GetAllActivities()
    {
        var activities = _activityService.GetAllActivities();
        var activityDtos = _mapper.Map<List<ActivityDto>>(activities);
        return Ok(activityDtos);
    }
    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteActivity(int id)
    {
        _activityService.DeleteActivity(id);
        return NoContent();
    }
}

