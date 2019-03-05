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
            pullChanges();
            pushChanges();
        }

        private void pushChanges()
        {
            var changes = _context.BasicEntities
                .Where(b => b.SyncStatus == false)
                .OrderBy(b => b.RowVersion)
                .ToList();

            foreach (var change in changes)
            {
                var method = change.RowVersion == 0 ? Method.POST : Method.PUT;
                var request = new RestRequest("api/BasicEntities", method);
                request.JsonSerializer = new JsonSerializer();
                request.AddJsonBody(change);

                var response = _client.Execute<BasicEntity>(request);

                if (response.IsSuccessful)
                {
                    if (response.Data != null)
                    {
                        response.Data.SyncStatus = true;
                        _context.Entry(change).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                        _context.Entry(response.Data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
            }
        }

        private void pullChanges()
        {
            var maxSync = _context.BasicEntities
                .Select(b => b.RowVersion)
                .DefaultIfEmpty(0)
                .Max();

            // pull since maxSync

            var changes = new List<BasicEntity>();

            foreach (var change in changes)
            {
                change.SyncStatus = true;
                _context.BasicEntities.Add(change);
            }

            _context.SaveChanges();
        }
    }
}
