using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Linq;

namespace CleanArchitecture.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventsExportQueryHandler : IRequestHandler<GetEventsExportQuery, EventExportFileVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;
        private readonly ICsvExporter _csvExporter;

        public GetEventsExportQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository, ICsvExporter csvExporter)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _csvExporter = csvExporter;
        }

        public async Task<EventExportFileVm> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
        {
            var orderedEvents = _mapper.Map<List<EventExportDto>>((await _eventRepository.ListAllAsync()).OrderBy(ev => ev.Date));

            var fileData = _csvExporter.ExportEventsToCsv(orderedEvents);

            var eventExportFileDto = new EventExportFileVm
            {
                ContentType = "text/csv",
                Data = fileData,
                EventExportFileName = $"{Guid.NewGuid()}.csv"
            };

            return eventExportFileDto;
        }
    }
}
