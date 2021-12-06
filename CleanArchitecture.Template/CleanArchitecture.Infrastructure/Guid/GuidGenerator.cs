namespace CleanArchitecture.Infrastructure.Guid
{
    using CleanArchitecture.Application.Contracts.Infrastructure;
    using System;

    public class GuidGenerator : IGuidGenerator
    {
        public async Task<Guid> GetNextAsync()
        {
            return await Task.FromResult(Guid.NewGuid());
        }
    }
}
