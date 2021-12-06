using AutoMapper;
using CleanArchitecture.Application.Features.Events.Commands.CreateEvent;
using CleanArchitecture.Application.Features.Events.Commands.DeleteEvent;
using CleanArchitecture.Application.Features.Events.Commands.UpdateEvent;
using CleanArchitecture.Application.Features.Events.Queries.GetEventDetail;
using CleanArchitecture.Application.Features.Events.Queries.GetEventsExport;
using CleanArchitecture.Application.Features.Events.Queries.GetEventsList;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, DeleteEventCommand>().ReverseMap();
            CreateMap<Event, EventExportDto>().ReverseMap();
        }
    }
}
