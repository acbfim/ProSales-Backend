using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using Microsoft.EntityFrameworkCore;
using ProSales.Domain.Dtos;
using ProSales.Domain.Global;
using ProSales.Repository.Contexts;
using ProSales.Repository.Contracts;

namespace ProSales.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ProSalesContext context;

        public ClientRepository(ProSalesContext context )
        {
            this.context = context;
        }
        public async Task<Client> GetByExternalId(Guid externalId)
        {
            IQueryable<Client> query = this.context.Client.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.ExternalId == externalId);
        }

        public async Task<Client> GetById(long id)
        {
            IQueryable<Client> query = this.context.Client.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Client> GetByFullName(string fullName)
        {
            IQueryable<Client> query = this.context.Client.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.FullName.ToUpper() == fullName.ToUpper());
        }

        public async Task<ICollection<Client>> GetAllByQuery(ClientQuery query)
        {
            var newSkip = query.Skip == 0 ? 0 : (query.Skip*query.Take);

            var result = this.context.Client.AsQueryable()
            .Skip(newSkip)
            .Take((int)query.Take);

            result = result
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.ContactType)
                .Include(x => x.Addresses)
                .Include(x => x.Documents)
                    .ThenInclude(x => x.DocumentType);

            result = result
            .Filter(query).Sort(query);

            return await result.ToListAsync();
        }

        public async Task<long> GetCountItems<T>(T query)  where T : class
        {
            var result = this.context.Client.AsQueryable().AsNoTracking();

            result = result
            .Filter((ICustomQueryable)query);

            return result.Count();
        }

        public async Task<ICollection<Client>> GetByDocument(DocumentQuery query)
        {
            var newSkip = query.Skip == 0 ? 0 : (query.Skip*query.Take);

            var result = this.context.Client.AsQueryable()
            .Skip(newSkip)
            .Take((int)query.Take);

            result = result
                .Include(x => x.Documents.Where(c => c.Value == query.Value))
                    .ThenInclude(x => x.DocumentType)
                .Include(x => x.Addresses)
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.ContactType);

            result = result.Where(x => x.Documents
                    .First(c => c.ClientId == x.Id)
                .Value.Contains(query.Value));

            return await result.ToListAsync();
        }

        public async Task<ICollection<Client>> GetByContact(ContactQuery query)
        {
            var newSkip = query.Skip == 0 ? 0 : (query.Skip*query.Take);

            var result = this.context.Client.AsQueryable()
            .Skip(newSkip)
            .Take((int)query.Take);

            result = result
                .Include(x => x.Documents.Where(c => c.Value == query.Value))
                    .ThenInclude(x => x.DocumentType)
                .Include(x => x.Addresses)
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.ContactType);

            result = result.Where(x => x.Contacts
                    .First(c => c.ClientId == x.Id)
                .Value.Contains(query.Value));

            return await result.ToListAsync();
        }
    }
}