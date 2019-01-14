using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAnalsys.utils
{
   public class FileUtil
    {

        public static string GetSafeFileName(string keyword)
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
    }
}
