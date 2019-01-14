using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAnalsys.wandoujia
{
    public class CatePageModel
    {

        public State state { get; set; }
        public Data data { get; set; }
    }

    public class State
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string tips { get; set; }
    }

    public class Data
    {
        public int currPage { get; set; }
        public string content { get; set; }
    }


}
