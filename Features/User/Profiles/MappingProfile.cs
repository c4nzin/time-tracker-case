using AutoMapper;
using time_tracker_case;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, AuthenticatedUserDto>();
    }
}
