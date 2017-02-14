using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dz.NPOI
{
    public static class ExcelImport
    {
        /// <summary>
        /// Excel导入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="startRow"></param>
        /// <returns></returns>
        public static IEnumerable<T> Import<T>(string filePath, int sheetIndex = 0, int startRow = 1) where T : class, new()
        {
            var workbook = InitiateWorkbook(filePath);
            var sheet = workbook.GetSheetAt(sheetIndex);
            var rows = sheet.GetRowEnumerator();

            var list = new List<T>();//存储数据
            var columnNameDic = new Dictionary<string, int>();//存储Excel的标题列的位置，如：姓名-0，性别-1...
            var rowIndex = 0;
            while (rows.MoveNext())
            {
                var row = rows.Current as IRow;
                if (rowIndex == 0)
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
                    rowIndex++;
                }

                if (row.RowNum < startRow)
                {
                    continue;
                }
                var item = new T();
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
                foreach (var property in properties)
                {
                    var columnAttr = property.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;
                    if (columnAttr != null)
                    {
                        if (columnNameDic.ContainsKey(columnAttr.Title))
                        {
                            var getCellIndex = columnNameDic[columnAttr.Title];
                            var value = row.GetCellValue(getCellIndex);
                            if (value != null)
                            {
                                var propType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                var safeValue = Convert.ChangeType(value, propType, CultureInfo.CurrentCulture);
                                property.SetValue(item, safeValue, null);
                            }
                            else
                            {
                                var propType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                var safeValue = Convert.ChangeType(columnAttr.DefaultValue, propType, CultureInfo.CurrentCulture);
                                property.SetValue(item, safeValue, null);
                            }
                        }
                        else
                        {
                            var propType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            var safeValue = Convert.ChangeType(columnAttr.DefaultValue, propType, CultureInfo.CurrentCulture);
                            property.SetValue(item, safeValue, null);
                        }
                    }
                }
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 初始化Excel读取
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static IWorkbook InitiateWorkbook(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new NotSupportedException("导入文件不存在");
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
                throw new NotSupportedException($"Not an excel file {filePath}");
            }
        }

        /// <summary>
        /// 获取某行某列的值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        internal static object GetCellValue(this IRow row, int index)
        {
            var cell = row.GetCell(index);
            if (cell == null)
            {
                return null;
            }
            switch (cell.CellType)
            {
                // This is a trick to get the correct value of the cell.
                // NumericCellValue will return a numeric value no matter the cell value is a date or a number.
                case CellType.Numeric:
                    return cell.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula:
                    return cell.ToString();
                case CellType.Blank:
                case CellType.Unknown:
                default:
                    return null;
            }
        }
    }
}
