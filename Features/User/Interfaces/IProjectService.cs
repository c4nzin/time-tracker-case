namespace time_tracker_case.Services;

public interface IProjectService
{
    Task<CreateProjectDto> CreateProject(CreateProjectDto createProjectDto);
}
