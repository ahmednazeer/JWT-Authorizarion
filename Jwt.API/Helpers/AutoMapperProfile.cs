namespace WebApi.Helpers;

using AutoMapper;
using Data.Entities;
using WebApi.Models.Users;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UpsertTeamModel, Team>();
    }
}