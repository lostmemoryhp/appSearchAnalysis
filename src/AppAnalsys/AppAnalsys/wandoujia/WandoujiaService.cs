using AppAnalsys.utils;
using Aspose.Cells;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppAnalsys.wandoujia
{
    public class WandoujiaService
    {
        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        public List<CateModel> getAllCategories()
        {
            var html = HttpUtil.GetHtml("https://www.wandoujia.com/category/app", Encoding.UTF8);

            var doc = NSoup.NSoupClient.Parse(html);
            var links = doc.Select("div.container li.parent-cate a.cate-link");
            return links.Select(x =>
            {
                var href = x.Attr("href");
                var cateId = int.Parse(href.Substring(href.LastIndexOf("/") + 1));
                var cate = new CateModel { url = href, cateId = cateId };
                return cate;
            }).ToList();
        }

        public List<WandoujiaExportModel> getPageModelByCate(int cateId, int pageIndex)
        {
            //var result = new List<WandoujiaExportModel>();
            var result = new ConcurrentQueue<WandoujiaExportModel>();
            string url = $"https://www.wandoujia.com/wdjweb/api/category/more?catId={cateId}&subCatId=0&page={pageIndex}";
            var html = HttpUtil.GetHtml(url);
            var pagemodel = JsonConvert.DeserializeObject<CatePageModel>(html);
            if (pagemodel.data != null && !String.IsNullOrEmpty(pagemodel.data.content))
            {
                var doc = NSoup.NSoupClient.Parse(pagemodel.data.content);
                var apps = doc.Select("li");
                apps.AsParallel().ForAll(item =>
                {

                    WandoujiaExportModel model = new WandoujiaExportModel
                    {
                        PackageName = item.Attr("data-pn"),
                        AppName = item.Select("div.app-desc>h2>a").Text,
                        AppUrl = item.Select("div.app-desc>h2>a").Attr("href"),
                        DownloadCount1 = getInstallCount(item.Select("div.app-desc>div.meta>span.install-count").Text),
                        FileSize = item.Select("div.app-desc>div.meta").Select("span").Last().Text()
                    };
                    result.Enqueue(model);
                });
            }
            var list = result.Distinct().ToList();
            list.AsParallel().ForAll(model =>
            {
                var model2 = getAppDetail(model.AppUrl);
                model.DownloadCount2 = model2.DownloadCount2;
                model.UpdateTime = model2.UpdateTime;
            });
            return list;
        }
        private double getInstallCount(String installCountStr)
        {
            double installCount = 0;
            installCountStr = installCountStr.Trim();
            if (installCountStr.IndexOf("万") != -1)
            {
                var index = installCountStr.IndexOf("万");
                installCountStr = installCountStr.Substring(0, index);
                installCount = double.Parse(installCountStr);
            }
            else if (installCountStr.IndexOf("亿") != -1)
            {
                var index = installCountStr.IndexOf("亿");
                installCountStr = installCountStr.Substring(0, index);
                installCount = double.Parse(installCountStr) * 10000;
            }
            else
            {
               
                if (installCountStr.IndexOf("人") != -1)
                {
                    var index = installCountStr.IndexOf("人");
                    installCountStr = installCountStr.Substring(0, index);
                    installCount = double.Parse(installCountStr) / 10000;
                }
                else if (installCountStr.IndexOf("次") != -1)
                {
                    var index = installCountStr.IndexOf("次");
                    installCountStr = installCountStr.Substring(0, index);
                    installCount = double.Parse(installCountStr) / 10000;
                }
            }
            return installCount;
        }


        public WandoujiaExportModel getAppDetail(String url)
        {
            var html = HttpUtil.GetHtml(url);
            if (String.IsNullOrEmpty(html))
            {
                Thread.Sleep(1000);
                html = HttpUtil.GetHtml(url);
            }
            var doc = NSoup.NSoupClient.Parse(html);
            var time = doc.Select("div.app-info-wrap .update-time").Attr("datetime");
            var installCount = doc.Select("div.app-info-wrap .install").Text.Trim();
            var result =  new WandoujiaExportModel
            {
                UpdateTime = time,
                DownloadCount2 = getInstallCount(installCount)
            };
            return result;
        }



        public void ExportExcel(List<WandoujiaExportModel> items)
        {
            var templatePath = Path.Combine(Environment.CurrentDirectory, "template_wandoujia.xlsx");
            DataSet dataSource = new DataSet();
            var dt = new DataTable("App");
            dataSource.Tables.Add(dt);
            dt.Columns.Add("Package", typeof(string));
            dt.Columns.Add("AppName", typeof(string));
            dt.Columns.Add("UpdateTime", typeof(string));
            dt.Columns.Add("FileSize", typeof(string));
            dt.Columns.Add("DownloadCount1", typeof(double));
            dt.Columns.Add("DownloadCount2", typeof(double));
            foreach (var item in items)
            {
                var row = dt.NewRow();
                row["Package"] = item.PackageName;
                row["AppName"] = item.AppName;
                row["UpdateTime"] = item.UpdateTime;
                row["FileSize"] = item.FileSize;
                row["DownloadCount1"] = item.DownloadCount1;
                row["DownloadCount2"] = item.DownloadCount2;
                dt.Rows.Add(row);
            }
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Workbook = new Workbook(templatePath);
            designer.SetDataSource(dataSource);
            designer.Process();
            string filePath = Path.Combine(Environment.CurrentDirectory, "查询结果", FileUtil.getSafeFileNameWithTime("豌豆荚", ".xlsx"));
            var dir=new DirectoryInfo(Path.GetDirectoryName(filePath));
            if (!dir.Exists)
            {
                dir.Create();
            }
            designer.Workbook.Save(filePath, Aspose.Cells.SaveFormat.Xlsx);
        }

    }
}
