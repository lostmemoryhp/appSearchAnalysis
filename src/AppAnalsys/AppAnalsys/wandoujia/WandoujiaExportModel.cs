using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAnalsys.wandoujia
{
    public class WandoujiaExportModel
    {
        /// <summary>
        /// 包名
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public String FileSize { get; set; }

        /// <summary>
        /// 下载次数1
        /// </summary>
        public double DownloadCount1 { get; set; }

        /// <summary>
        /// 下载次数2
        /// </summary>
        public double DownloadCount2 { get; set; }

        /// <summary>
        /// app详情页
        /// </summary>
        public String AppUrl { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            var other = obj as WandoujiaExportModel;
            if (other == null)
            {
                return false;
            }
            return PackageName == other.PackageName;
        }
    }
}
