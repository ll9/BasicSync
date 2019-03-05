using BasicSync.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSync.Controllers
{
    public class MainController
    {
        private MainDialog _view;
        private ApplicationDbContext _efContext;
        private AdoContext _adoContext;

        public MainController(MainDialog view)
        {
            _view = view;
            _efContext = new ApplicationDbContext();
            _adoContext = new AdoContext();

            _efContext.Database.Migrate();
        }

        internal void LoadDataGridSource()
        {
            var dataTable = new DataTable();
            _adoContext.Fill(dataTable);
            _view.SetDataSource(dataTable);
        }

        internal void SaveChanges(DataTable dataTable)
        {
            _adoContext.Update(dataTable);
        }
    }
}
