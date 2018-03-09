using Aspose.Cells;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AppAnalsys
{
    public partial class Form1 : Form
    {
        string LIST_URL = "https://search.appstore.vivo.com.cn/port/packages/?apps_per_page=20&screensize=720_1280&pictype=webp&u=1234567890&patch_sup=1&plat_key_ver=0&imei=867182033235142&nt=WIFI&id=0&platApkVer=0&build_number=Flyme+6.2.0.4A&elapsedtime=202744204&plateformVersionName=null&sshow=110&density=2.0&cs=0&av=24&an=7.0&plateformVersion=0&app_version=1141&platApkVerName=null&key={0}&page_index={1}&target=local&cfrom=2&model=M6&s=2%7C3528779343";
        string APP_URL = "https://appc.baidu.com/uiserver?usertype=1&cen=cuid_cut_cua_uid&abi=armeabi-v7a&action=detail&pkname=com.baidu.appsearch&pname={0}&province=qivtklO_ValxRH8868B3kjiheug_RBuRodkqA&disp=Flyme+6.2.0.4A&gms=false&from=1000561u&cct=q8vJkluJVagxRSiIqOSCkluheaghMHf3odfqA&pu=ctv%401%2Ccfrom%401000561u%2Ccua%40_avLC_aE-i4qywoUfpw1zyPLsioeuL8bxLqqC%2Ccuid%400iSH8_aU2iYLav8S082Xigufv8g2aHuIgiSku_aHv86XuviJ0avKigamvi_Ka2fQga2VfqqqB%2Ccut%40ruL-izuYD8gBNQzt5Z5mA%2Cosname%40baiduappsearch&network=WF&operator=460004&psize=3&country=CN&is_support_webp=true&cll=gu2RNYalBfgoueiy0a2GNguABfANTYFiB&uid=0iSH8_aU2iYLav8S082Xigufv8g2aHuIgiSku_aHv8q-uHixguvk8gua28_OOv853dqqC&f=urlhandle&language=zh&part=main&apn=&platform_version_id=24&ver=16794628&&crid=1520501469198&native_api=1&location=q8vJkluJVagxRSiIqOSCkluheaghMHf3odfqA&bdussid=&ptl=hps";
        List<ExportedModel> searchResults = new List<ExportedModel>();
        List<ExportedModel> existsApps = new List<ExportedModel>();
        public Form1()
        {
            InitializeComponent();
        }

        private void EnsureDirectory()
        {
            var dir = Path.Combine(Environment.CurrentDirectory, "查询结果");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtKeyword.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入关键字。");
            }
            EnsureDirectory();
            btnStart.Enabled = false;
            txtKeyword.Enabled = false;
            searchResults.Clear();
            existsApps.Clear();
            var keyword = txtKeyword.Text.Trim();
            ReadExistsApp(keyword);
            using (WebClient client = new WebClient())
            {
                int pageIndex = 1;
                ProcessPage(client, keyword, pageIndex);
            }
            ExportExcel(keyword);
            MessageBox.Show("处理完成！");
            btnStart.Enabled = true;
            txtKeyword.Enabled = true;
        }

        public void ReadExistsApp(string keyword)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "查询结果", GetSafeFileName(keyword));
            if (!File.Exists(path))
                return;
            Workbook workbook = new Workbook(path, new LoadOptions(LoadFormat.Xlsx));
            var all = workbook.Worksheets[0].Cells;
            for (int i = 1; i < all.Rows.Count; i++)
            {
                var row = all.Rows[i];
                var model = new ExportedModel
                {
                    PackageName = row[0].StringValue,
                    AppName = row[1].StringValue,
                    PackageId = row[2].IntValue,
                    Official = row[3].StringValue,
                    VersionCode = row[4].IntValue,
                    VersionName = row[5].StringValue,
                    DownloadCount = row[6].StringValue,
                    IsOnline = row[7].StringValue,
                    UserContent = row[8].StringValue,
                    NewStatus = 0
                };
                this.existsApps.Add(model);
            }
        }



        public void ExportExcel(string keyword)
        {
            var list = new List<ExportedModel>();
            list.AddRange(this.existsApps.OrderByDescending(x => x.IsOnline).ThenBy(x => x.AppName));
            list.AddRange(this.searchResults.OrderByDescending(x => x.IsOnline).ThenBy(x => x.AppName));
            var templatePath = Path.Combine(Environment.CurrentDirectory, "导出摸板.xlsx");
            DataSet dataSource = new DataSet();
            var dt = new DataTable("App");
            dataSource.Tables.Add(dt);
            dt.Columns.Add("Package", typeof(string));
            dt.Columns.Add("AppName", typeof(string));
            dt.Columns.Add("PackageId", typeof(int));
            dt.Columns.Add("Official", typeof(string));
            dt.Columns.Add("VersionCode", typeof(int));
            dt.Columns.Add("VersionName", typeof(string));
            dt.Columns.Add("DownloadCount", typeof(string));
            dt.Columns.Add("IsOnline", typeof(string));
            dt.Columns.Add("UserContent", typeof(string));
            foreach (var item in list)
            {
                var row = dt.NewRow();
                row["Package"] = item.PackageName;
                row["AppName"] = item.AppName;
                row["PackageId"] = item.PackageId;
                row["IsOnline"] = item.IsOnline;
                row["UserContent"] = item.UserContent;
                row["Official"] = item.Official;
                row["VersionCode"] = item.VersionCode;
                row["VersionName"] = item.VersionName;
                row["DownloadCount"] = item.DownloadCount;
                dt.Rows.Add(row);
            }
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Workbook = new Workbook(templatePath);
            designer.SetDataSource(dataSource);
            designer.Process();
            string filePath = Path.Combine(Environment.CurrentDirectory, "查询结果", GetSafeFileName(keyword));
            designer.Workbook.Save(filePath, Aspose.Cells.SaveFormat.Xlsx);
        }

        public string GetSafeFileName(string keyword)
        {
            string hash = keyword.GetHashCode().ToString();
            foreach (var invalidChar in Path.GetInvalidFileNameChars())
            {
                keyword = keyword.Replace(invalidChar, '0');
            }
            foreach (var invalidChar in Path.GetInvalidPathChars())
            {
                keyword = keyword.Replace(invalidChar, '0');
            }
            return keyword + "-" + hash + ".xlsx";
        }

        private void ProcessPage(WebClient client, string keyword, int pageIndex)
        {
            var encodeKeyword = HttpUtility.UrlEncode(keyword);
            var listUrl = string.Format(LIST_URL, encodeKeyword, pageIndex);
            client.Encoding = Encoding.UTF8;
            var json = client.DownloadString(listUrl);
            var result = JsonConvert.DeserializeObject<SearchResult>(json);
            if (result.totalCount > 0)
            {
                foreach (var app in result.value)
                {

                    var existApp = this.existsApps.FirstOrDefault(x => x.PackageId == app.id && x.PackageName == app.package_name);
                    if (existApp != null)
                    {
                        existApp.DownloadCount = app.download_count;
                        existApp.VersionCode = app.version_code;
                        existApp.VersionName = app.version_name;
                    }
                    else
                    {
                        existApp = new ExportedModel
                        {
                            AppName = app.title_zh,
                            PackageId = app.id,
                            PackageName = app.package_name,
                            Official = app.official == "0" ? "" : "是",
                            NewStatus = 1,
                            DownloadCount = app.download_count,
                            VersionCode = app.version_code,
                            VersionName = app.version_name
                        };
                        searchResults.Add(existApp);
                    }
                    try
                    {
                        var appUrl = string.Format(APP_URL, app.package_name);
                        var appJson = GetHtml(appUrl, Encoding.UTF8);
                        var obj = JsonConvert.DeserializeObject(appJson) as JObject;
                        var appInfo = JsonConvert.DeserializeObject<AppResult>(appJson);
                        bool isOnline = !string.IsNullOrEmpty(appInfo.result.data.data.appinfo.package);
                        existApp.IsOnline = isOnline ? "是" : "";

                    }
                    catch (Exception ex)
                    {
                        existApp.IsOnline = "";
                    }

                }
                if (result.pageNo < result.maxPage)
                {
                    pageIndex++;
                    ProcessPage(client, keyword, pageIndex);
                }
            }
        }




        /// <summary>
        /// 获取源代码, 需要把获取到的https页面字节流通过gzip解压，用这种方法解决了乱码问题。
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHtml(string url, Encoding encoding)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 20000;
                request.AllowAutoRedirect = false;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK && response.ContentLength < 1024 * 1024)
                {
                    if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                        reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), encoding);
                    else
                        reader = new StreamReader(response.GetResponseStream(), encoding);
                    string html = reader.ReadToEnd();
                    return html;
                }
            }
            catch
            {
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (reader != null)
                    reader.Close();
                if (request != null)
                    request = null;
            }
            return string.Empty;
        }

    }
}
