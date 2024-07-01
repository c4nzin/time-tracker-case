using AutoMapper;
using time_tracker_case;
using time_tracker_case.Models;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<CreateProjectDto, Project>();
    }
}
