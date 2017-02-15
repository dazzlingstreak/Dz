using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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

            if (extension.Equals(".xls"))
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    return new HSSFWorkbook(stream);
                }
            }
            else if (extension.Equals(".xlsx"))
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    return new XSSFWorkbook(stream);
                }
            }
            else
            {
                throw new NotSupportedException($"Not an excel filePath {filePath}");
            }
        }

        /// <summary>
        /// 数据导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="filePath">导出模板路径</param>
        /// <param name="sheetName">导出模板的sheet页名称</param>
        /// <param name="headRow">sheet中的标题头行</param>
        /// <param name="footRows">sheet中的页脚行集合</param>
        /// <returns>字节数组</returns>
        public static byte[] Export<T>(this IEnumerable<T> source, string filePath, string sheetName, int headRow, List<int> footRows = null)
        {
            var workbook = InitiateWorkbook(filePath);
            var sheet = workbook.GetSheet(sheetName);
            if (sheet == null)
            {
                throw new Exception("sheet页不存在");
            }
            var rows = sheet.GetRowEnumerator();

            var columnNameDic = new Dictionary<string, int>();//存储Excel的标题列的位置，如：姓名-0，性别-1...
            var rowIndex = 0;
            while (rows.MoveNext())
            {
                var row = rows.Current as IRow;
                if (rowIndex == headRow)
                {
                    //标题列处理，解析为（标题-列号）存储，在数据行处理时，根据标题对应取值
                    for (var i = 0; i < row.Cells.Count; i++)
                    {
                        var cell = row.Cells[i];
                        if (!string.IsNullOrEmpty(cell.StringCellValue) && !columnNameDic.ContainsKey(cell.StringCellValue))
                        {
                            columnNameDic.Add(cell.StringCellValue, cell.ColumnIndex);
                        }
                    }
                    break;
                }
                rowIndex++;
            }

            ICellStyle dateCellStyle = null;
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);

            //如果有页脚
            if (footRows != null && (headRow + source.Count()) > footRows.Min())
            {
                footRows = footRows.OrderBy(p => p).ToList();
                foreach (var item in footRows)
                {
                    sheet.CopyRow(item, item + source.Count());
                }
            }
            //数据填充
            var dataRow = headRow + 1;
            foreach (var item in source)
            {
                var row = sheet.CreateRow(dataRow);
                foreach (var property in properties)
                {
                    var columnAttr = property.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;
                    if (columnAttr != null)
                    {
                        if (columnNameDic.ContainsKey(columnAttr.Title))
                        {
                            var getCellIndex = columnNameDic[columnAttr.Title];
                            var cell = row.CreateCell(getCellIndex);
                            var value = property.GetValue(item);
                            if (property.PropertyType == typeof(bool))
                            {
                                cell.SetCellValue((bool)value);
                            }
                            else if (property.PropertyType == typeof(DateTime))
                            {
                                if (dateCellStyle == null)
                                {
                                    dateCellStyle = workbook.CreateCellStyle();

                                    var dateFormat = workbook.CreateDataFormat();

                                    dateCellStyle.DataFormat = dateFormat.GetFormat("yyyy-MM-dd");
                                }

                                cell.CellStyle = dateCellStyle;

                                cell.SetCellValue(Convert.ToDateTime(value));
                            }
                            else if (property.PropertyType == typeof(Guid))
                            {
                                cell.SetCellValue(Convert.ToString(value));
                            }
                            else if (property.PropertyType == typeof(double))
                            {
                                cell.SetCellValue(Convert.ToDouble(value));
                            }
                            else
                            {
                                cell.SetCellValue(Convert.ToString(value));
                            }
                        }
                    }
                }
                dataRow++;
            }

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                return ms.ToArray();
            }
        }
    }
}
