namespace WebApi.Models.Users;

using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

public class UpsertTeamModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Country { get; set; }
    public string Principal { get; set; }
}