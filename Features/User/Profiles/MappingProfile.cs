using AutoMapper;
using time_tracker_case;
using time_tracker_case.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, AuthenticatedUserDto>();
    }
}
