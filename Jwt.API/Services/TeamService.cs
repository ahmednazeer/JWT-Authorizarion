namespace WebApi.Services;

using AutoMapper;
using Data;
using System.Linq;
using Data.Entities;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Models.Users;

public interface ITeamService
{
    List<Team> GetAll();
    Team GetById(int id);
    Team UpdateTeam(int id, UpsertTeamModel model);
    void AddTeam(UpsertTeamModel model);
    void Delete(int id);
}

public class TeamService : ITeamService
{
    private AppDbContext _context;
    private readonly IMapper _mapper;

    public TeamService(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<Team> GetAll()
    {
        return _context.Teams.ToList();
    }

    public Team GetById(int id)
    {
        return getTeam(id);
    }
    public void AddTeam(UpsertTeamModel model)
    {
        Team team= _mapper.Map<Team>(model);
        team.Name = model.Name;
        team.Country = model.Country;
        team.TeamPrincipal = model.Principal;
        _context.Teams.Add(team);
        _context.SaveChanges();
    }
    public Team UpdateTeam(int id,UpsertTeamModel model)
    {
        var team = _context.Teams.FirstOrDefault(x => x.Id == id);
        if (team is null)
            return null;
        team.Name = model.Name;
        team.Country = model.Country;
        team.TeamPrincipal = model.Principal;
        _context.Teams.Add(team);
        _context.SaveChanges();
        return team;
    }

   

    public void Delete(int id)
    {
        var Team = getTeam(id);
        _context.Teams.Remove(Team);
        _context.SaveChanges();
    }

    // helper methods

    private Team getTeam(int id)
    {
        var Team = _context.Teams.Find(id);
        if (Team == null) throw new KeyNotFoundException("Team not found");
        return Team;
    }

    
}