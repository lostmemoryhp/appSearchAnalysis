using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAnalsys
{
    public class SearchResult
    {
        public bool result { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int maxPage { get; set; }
        public int totalCount { get; set; }
        public string from { get; set; }
        public Focus[] focus { get; set; }
        public int wordType { get; set; }
        public string sps { get; set; }
        public string searchType { get; set; }
        public int pageNo { get; set; }
        public Value[] value { get; set; }
    }


    public class Focus
    {
        public int[] pos { get; set; }
        public int div { get; set; }
        public App[] apps { get; set; }
        public int type { get; set; }
    }

    public class App
    {
        public int id { get; set; }
        public string title_zh { get; set; }
        public string package_name { get; set; }
        public string developer { get; set; }
        public string download_count { get; set; }
        public string official { get; set; }
        public string icon_url { get; set; }
        public string download_url { get; set; }
        public string version_name { get; set; }
        public int version_code { get; set; }
        public int size { get; set; }
        public string from { get; set; }
        public float score { get; set; }
        public int commentCount { get; set; }
        public string patchs { get; set; }
        public int tag { get; set; }
        public string remark { get; set; }
        public string ssource { get; set; }
        public int[] stype { get; set; }
        public string cpdps { get; set; }
        public int cp { get; set; }
        public int atype { get; set; }
        public int test_type { get; set; }
        public string transParam { get; set; }
    }

    public class Value
    {
        public int id { get; set; }
        public string title_zh { get; set; }
        public string package_name { get; set; }
        public string developer { get; set; }
        public string download_count { get; set; }
        public string official { get; set; }
        public string icon_url { get; set; }
        public string download_url { get; set; }
        public string version_name { get; set; }
        public int version_code { get; set; }
        public int size { get; set; }
        public string from { get; set; }
        public float score { get; set; }
        public int commentCount { get; set; }
        public string patchs { get; set; }
        public int tag { get; set; }
        public string remark { get; set; }
        public string[] screenshot { get; set; }
        public float downloadPercent { get; set; }
        public string ssource { get; set; }
        public int[] stype { get; set; }
        public int atype { get; set; }
        public int test_type { get; set; }
        public string transParam { get; set; }
    }

}
