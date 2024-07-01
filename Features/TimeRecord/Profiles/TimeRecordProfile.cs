using AutoMapper;
using time_tracker_case;
using time_tracker_case.Models;

public class TimeRecordMappingProfile : Profile
{
    public TimeRecordMappingProfile()
    {
        CreateMap<CreateTimeRecordDto, TimeRecord>();
    }
}
