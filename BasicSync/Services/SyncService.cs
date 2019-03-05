using BasicSync.Data;
using BasicSync.Models;
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

        public SyncService(ApplicationDbContext context)
        {
            _context = context;
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
