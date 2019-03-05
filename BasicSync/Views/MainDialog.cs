using BasicSync.Controllers;
using BasicSync.Models;
using BasicSync.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicSync
{
    public partial class MainDialog : Form
    {
        private MainController _controller;

        public MainDialog()
        {
            InitializeComponent();

            _controller = new MainController(this);
            _controller.LoadDataGridSource();
        }

        public void SetDataSource(object dataSource)
        {
            dataGridView1.DataSource = dataSource;
        }

        private void MainDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dataGridView1.DataSource is DataTable dataTable)
            {
                _controller.SaveChanges(dataTable);
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            var dataGrid = sender as DataGridView;

            if (dataGrid.Columns.Contains(nameof(BasicEntity.Id)))
            {
                e.Row.Cells[nameof(BasicEntity.Id)].Value = Guid.NewGuid().ToString();
            }
            if (dataGrid.Columns.Contains(nameof(BasicEntity.SyncStatus)))
            {
                e.Row.Cells[nameof(BasicEntity.SyncStatus)].Value = false;
            }
            if (dataGrid.Columns.Contains(nameof(BasicEntity.IsDeleted)))
            {
                e.Row.Cells[nameof(BasicEntity.IsDeleted)].Value = false;
            }
            if (dataGrid.Columns.Contains(nameof(BasicEntity.RowVersion)))
            {
                e.Row.Cells[nameof(BasicEntity.RowVersion)].Value = 0;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dataGrid = sender as DataGridView;

            if (dataGrid.Columns.Contains(nameof(BasicEntity.SyncStatus)))
            {
                dataGrid.Rows[e.RowIndex].Cells[nameof(BasicEntity.SyncStatus)].Value = false;
            }
        }

        private void SyncButton_Click(object sender, EventArgs e)
        {
            _controller.Sync();
        }
    }
}
