using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAnalsys
{

    public class AppResult2
    {
        public int error_no { get; set; }
        public string message { get; set; }
        public Result2 result { get; set; }
    }

    public class Result2
    {
        public Data0 data { get; set; }
        public string dirtag { get; set; }
    }

    public class Data0
    {
        public int type { get; set; }
        public Data1 data { get; set; }
    }

    public class Data10
    {
        public object[] appinfo { get; set; }
        public List0[] list { get; set; }
    }

    public class List0
    {
        public int type { get; set; }
        public Data20 data { get; set; }
    }

    public class Data20
    {
        public List10[] list { get; set; }
        public string shareurl { get; set; }
        public int feedback_appchannel { get; set; }
        public int unable_download { get; set; }
        public int download_jumppage_index { get; set; }
        public int is_from_mission { get; set; }
        public int download_immediatly { get; set; }
    }

    public class List10
    {
        public int type { get; set; }
        public Data30 data { get; set; }
    }

    public class Data30
    {
        public Videoinfo0 videoinfo { get; set; }
        public Label_Jump0 label_jump { get; set; }
        public int try_play_state { get; set; }
        public int display_score { get; set; }
        public int display_count { get; set; }
        public List20[] list { get; set; }
    }

    public class Videoinfo0
    {
    }

    public class Label_Jump0
    {
    }

    public class List20
    {
        public int type { get; set; }
        public Data40 data { get; set; }
    }

    public class Data40
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
        public object[] groupids { get; set; }
    }

    public class List30
    {
        public int datatype { get; set; }
        public object itemdata { get; set; }
    }


}
