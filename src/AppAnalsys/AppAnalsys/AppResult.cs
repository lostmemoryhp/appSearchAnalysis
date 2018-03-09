using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAnalsys
{
    public class AppResult
    {
        public int error_no { get; set; }
        public string message { get; set; }
        public Result result { get; set; }
    }



    public class Result
    {
        public Data data { get; set; }
        public string dirtag { get; set; }
    }

    public class Data
    {
        public int type { get; set; }
        public Data1 data { get; set; }
    }

    public class Data1
    {
        public Appinfo appinfo { get; set; }
        public List[] list { get; set; }
    }

    public class Appinfo
    {
        public string sname { get; set; }
        public string package { get; set; }
        public string docid { get; set; }
        public string download_inner { get; set; }
        public string groupid { get; set; }
        public string icon { get; set; }
        public string packageid { get; set; }
        public string signmd5 { get; set; }
        public string size { get; set; }
        public string type { get; set; }
        public string versioncode { get; set; }
        public string versionname { get; set; }
        public string manual_short_brief { get; set; }
        public string f { get; set; }
        public string catename { get; set; }
        public string cateid { get; set; }
        public string detail_background { get; set; }
        public string md5 { get; set; }
        public string popu_index { get; set; }
        public string comment_tag_display { get; set; }
        public int is_official { get; set; }
        public string official_icon_url { get; set; }
        public string all_download { get; set; }
    }

    public class List
    {
        public int type { get; set; }
        public Data2 data { get; set; }
    }

    public class Data2
    {
        public List1[] list { get; set; }
        public string shareurl { get; set; }
        public int feedback_appchannel { get; set; }
        public int unable_download { get; set; }
        public int download_jumppage_index { get; set; }
        public int is_from_mission { get; set; }
        public int download_immediatly { get; set; }
    }

    public class List1
    {
        public int type { get; set; }
        public Data3 data { get; set; }
    }

    public class Data3
    {
        public Videoinfo videoinfo { get; set; }
        public Label_Jump label_jump { get; set; }
        public int try_play_state { get; set; }
        public string display_score { get; set; }
        public string display_count { get; set; }
        public List2[] list { get; set; }
    }

    public class Videoinfo
    {
        public int playcount { get; set; }
        public int orientation { get; set; }
        public string from { get; set; }
        public string iconurl { get; set; }
        public string duration { get; set; }
    }

    public class Label_Jump
    {
        public string title { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public string fParam { get; set; }
    }

    public class List2
    {
        public int type { get; set; }
        public Data4 data { get; set; }
    }

    public class Data4
    {
        public string title { get; set; }
        public string f { get; set; }
        public List3[] list { get; set; }
        public bool footview_visible { get; set; }
        public string dataurl { get; set; }
        public string package { get; set; }
        public string docid { get; set; }
        public string packageid { get; set; }
        public string groupid { get; set; }
        public string versionname { get; set; }
        public string comment_tag_display { get; set; }
        public string[] groupids { get; set; }
    }

    public class List3
    {
        public int datatype { get; set; }
        public Itemdata itemdata { get; set; }
    }

    public class Itemdata
    {
        public string is_fold { get; set; }
        public string[] screenshots { get; set; }
        public string[] screenshots_large { get; set; }
        public string brief { get; set; }
        public Apptag[] apptags { get; set; }
        public string changelog { get; set; }
        public string[] labels { get; set; }
        public string[] permission_type { get; set; }
        public string[] permission_guide { get; set; }
        public string title { get; set; }
        public object jump { get; set; }
        public int type { get; set; }
        public string img { get; set; }
        public string src { get; set; }
        public int part { get; set; }
        public string f { get; set; }
        public string dataurl { get; set; }
        public App_Moreversion[] app_moreversion { get; set; }
        public string sourcename { get; set; }
        public Dev_Display dev_display { get; set; }
        public string md5 { get; set; }
    }

    public class Dev_Display
    {
        public string dev_id { get; set; }
        public string dev_name { get; set; }
        public string dev_score { get; set; }
        public string f { get; set; }
    }

    public class Apptag
    {
        public string tagname { get; set; }
        public Jump jump { get; set; }
    }

    public class Jump
    {
        public string fParam { get; set; }
        public int type { get; set; }
        public Bundle bundle { get; set; }
    }

    public class Bundle
    {
        public Page page { get; set; }
    }

    public class Page
    {
        public int type { get; set; }
        public Data5 data { get; set; }
    }

    public class Data5
    {
        public string dataurl { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string bgurl { get; set; }
        public string f { get; set; }
        public bool footview_visible { get; set; }
        public int filterinstalled { get; set; }
    }

    public class App_Moreversion
    {
        public string version { get; set; }
        public Content[] content { get; set; }
        public string versioncode { get; set; }
    }

    public class Content
    {
        public string packageid { get; set; }
        public string groupid { get; set; }
        public string docid { get; set; }
        public string sname { get; set; }
        public string size { get; set; }
        public string updatetime { get; set; }
        public string versioncode { get; set; }
        public string sourcename { get; set; }
        public string type { get; set; }
        public string all_download_pid { get; set; }
        public string strDownload { get; set; }
        public string display_score { get; set; }
        public string all_download { get; set; }
        public string f { get; set; }
    }

}
