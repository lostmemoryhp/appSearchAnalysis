using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAnalsys
{
    public class ExportedModel
    {
        /// <summary>
        /// 包名
        /// </summary>
        public string PackageName { get; set; }
        /// <summary>
        /// 包ID
        /// </summary>
        public int PackageId { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 是否官方发布
        /// </summary>
        public string Official { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int VersionCode { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public string VersionName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public string DownloadCount { get; set; }

        /// <summary>
        /// 是否上线
        /// </summary>
        public string IsOnline { get; set; }

        /// <summary>
        /// 余旭手填那一列
        /// </summary>
        public string UserContent { get; set; }

        public int NewStatus { get; set; }
    }
}
