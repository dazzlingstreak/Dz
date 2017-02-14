using Dz.Core.Random;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dz.NPOI
{
    public static class ExcelExport
    {
        /// <summary>
        /// 导出Excel的初始化
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static IWorkbook InitiateWorkbook(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            var extension = Path.GetExtension(filePath);
            var copyTempPath = AppDomain.CurrentDomain.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(filePath) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + new RNGCryptoServiceProvider().Next(100, 1000) + extension;
            File.Copy(filePath, copyTempPath);

            if (extension.Equals(".xls"))
            {
                using (var stream = new FileStream(copyTempPath, FileMode.Open, FileAccess.Read))
                {
                    return new HSSFWorkbook(stream);
                }
            }
            else if (extension.Equals(".xlsx"))
            {
                using (var stream = new FileStream(copyTempPath, FileMode.Open, FileAccess.Read))
                {
                    return new XSSFWorkbook(stream);
                }
            }
            else
            {
                throw new NotSupportedException($"Not an excel filePath {filePath}");
            }
        }

        public static void Export(string filePath)
        {
            var workbook = InitiateWorkbook(filePath);

        }
    }
}
