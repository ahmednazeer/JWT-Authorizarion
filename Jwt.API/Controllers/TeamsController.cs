
using AutoMapper;
using Data;
using Data.Entities;
using Jwt.API.Controllers;
using Jwt.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Models.Users;
using WebApi.Services;
namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class TeamsController : BaseController
{
    private readonly ITeamService _teamService;
    private IMapper _mapper;

    public TeamsController(
        ITeamService teamService,
        IMapper mapper)
    {
        _teamService = teamService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var teams = _teamService.GetAll();
        return Ok(teams);//GetActionResultByModel(new CoreResponseModel<object> { Data = teams }, null);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var team = _teamService.GetById(id);
        return Ok(new 
        {
            Data=team,
            Success=true,
            StatusCode=(int)HttpStatusCode.OK
        });
    }

    [HttpPost]
    public IActionResult Create(UpsertTeamModel model)
    {
        _teamService.AddTeam(model);
        return Ok(new 
        {
            //Data = null,
            Success = true,
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Team Added Successfully!"
        });
    }

    [HttpPut("id")]
    public IActionResult Update(int id,[FromBody]UpsertTeamModel model)
    {
        var team=_teamService.UpdateTeam(id,model);
        if (team is null)
            return Ok(new //CoreResponseModel<Team>
            {
                //Data = null,
                Success = false,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Team Updated Successfully!"
            });

        return Ok(new //CoreResponseModel<Team>
        {
            //Data = null ,
            Success = true,
            StatusCode =(int)HttpStatusCode.NoContent,
            Message = "Team Updated Successfully!"
        });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _teamService.Delete(id);
        return Ok(new { message = "User deleted" });
    }
}