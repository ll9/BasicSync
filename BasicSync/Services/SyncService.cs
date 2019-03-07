using BasicSync.Data;
using BasicSync.Models;
using BasicSync.Serializers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSync.Services
{
    public class SyncService
    {
        private readonly ApplicationDbContext _context;
        private readonly RestClient _client;

        public SyncService(ApplicationDbContext context)
        {
            _context = context;
            _client = new RestClient("https://localhost:44370/");

        }

        public void Sync()
        {
            IQueryable<BasicEntity> changes = GetChanges();
            var maxSync = GetMaxSync();

            var request = new RestRequest("api/BasicEntities/{maxSync}", Method.POST);
            request.JsonSerializer = new JsonSerializer();
            request.AddUrlSegment("maxSync", maxSync);
            request.AddJsonBody(changes);

            var response = _client.Execute<List<BasicEntity>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                using (var context = new ApplicationDbContext())
                {
                    foreach (var item in response.Data)
                    {
                        item.SyncStatus = true;

                        if (context.BasicEntities.Any(e => e.Id == item.Id))
                        {
                            context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            context.BasicEntities.Add(item);
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        private int GetMaxSync()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.BasicEntities
                    .Select(b => b.RowVersion)
                    .DefaultIfEmpty(0)
                    .Max();
            }
        }

        private IQueryable<BasicEntity> GetChanges()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.BasicEntities
                    .Where(b => b.SyncStatus == false);
            }
        }
    }
}
