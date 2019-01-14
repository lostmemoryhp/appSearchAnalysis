using AppAnalsys.wandoujia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppAnalsys
{
    public partial class FrmWandoujia : Form
    {
        SynchronizationContext syncContext = null;
        WandoujiaService wandoujiaService = new WandoujiaService();
        public FrmWandoujia()
        {
            InitializeComponent();
            syncContext = SynchronizationContext.Current;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            Task.Factory.StartNew<List<WandoujiaExportModel>>(() =>
            {
                var result = new List<WandoujiaExportModel>();
                var allCates = wandoujiaService.getAllCategories();
                foreach (var cate in allCates)
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        var items = wandoujiaService.getPageModelByCate(cate.cateId, i);
                        result.AddRange(items);
                    }
                }
                return result;
            }).ContinueWith(r =>
            {
                wandoujiaService.ExportExcel(r.Result);
                syncContext.Post(state =>
                {
                    MessageBox.Show("抓取完成!");
                    btnStart.Enabled = true;
                }, null);
            });
        }
    }
}
