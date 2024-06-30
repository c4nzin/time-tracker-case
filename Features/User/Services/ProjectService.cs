﻿using Microsoft.EntityFrameworkCore;
using time_tracker_case.Contexts;
using time_tracker_case.Models;

namespace time_tracker_case.Services;

public class ProjectService : IProjectService
{
    private readonly IdentityDbContext _context;
    private readonly IUserService _userService;

    public ProjectService(IdentityDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<CreateProjectDto> CreateProject(CreateProjectDto createProjectDto)
    {
        var existingProject = await _context.Projects.FirstOrDefaultAsync(p =>
            p.Name == createProjectDto.Name
        );

        if (existingProject != null)
        {
            throw new Exception("A project with the same name already exists.");
        }

        var user = await _userService.GetAuthenticatedUser();

        var newProject = new Project { Name = createProjectDto.Name, UserId = user.Id };

        _context.Projects.Add(newProject);
        await _context.SaveChangesAsync();

        return createProjectDto;
    }

    public async Task<ProjectDetails> GetProjectById(Guid projectId)
    {
        List<double> numbers = new List<double>();

        var user = await _userService.GetAuthenticatedUser();

        var project = await _context.Projects.FirstOrDefaultAsync(p =>
            p.Id == projectId && p.UserId == user.Id
        );
        if (project == null)
        {
            throw new BadHttpRequestException("Project not found or access denied.");
        }

        var records = await _context
            .TimeRecords.Where(table => table.ProjectId == project.Id)
            .ToListAsync();

        var totalProfit = records.Sum(tp => tp.Profit);

        return new ProjectDetails { Project = project, TotalProfit = totalProfit };
    }
}
