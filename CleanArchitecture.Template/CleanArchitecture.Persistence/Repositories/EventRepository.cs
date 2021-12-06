using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public Task<bool> IsEventNameAndDateUnique(string? name, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
