using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ZTunnel.Pmms.Core.Excel
{
    public class ExcelHelper
    {
        /// <summary>
        /// 创建ExcelPackage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas">数据实体</param>
        /// <param name="columnNames">列名</param>
        /// <param name="outOfColumns">排除列</param>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="title">标题</param>
        /// <param name="isProtected">是否加密</param>
        /// <returns></returns>
        private static ExcelPackage CreateExcelPackage<T>(List<T> datas, Dictionary<string, string> columnNames, List<string> outOfColumns, string sheetName = "Sheet1", string title = "", int isProtected = 0, Dictionary<int, string> total = null)
        {
            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(sheetName);
            if (isProtected == 1)
            {
                worksheet.Protection.IsProtected = true;//设置是否进行锁定
                worksheet.Protection.SetPassword("xiangzhidaomimama");//设置密码
                worksheet.Protection.AllowAutoFilter = false;//下面是一些锁定时权限的设置
                worksheet.Protection.AllowDeleteColumns = false;
                worksheet.Protection.AllowDeleteRows = false;
                worksheet.Protection.AllowEditScenarios = false;
                worksheet.Protection.AllowEditObject = false;
                worksheet.Protection.AllowFormatCells = false;
                worksheet.Protection.AllowFormatColumns = false;
                worksheet.Protection.AllowFormatRows = false;
                worksheet.Protection.AllowInsertColumns = false;
                worksheet.Protection.AllowInsertHyperlinks = false;
                worksheet.Protection.AllowInsertRows = false;
                worksheet.Protection.AllowPivotTables = false;
                worksheet.Protection.AllowSelectLockedCells = false;
                worksheet.Protection.AllowSelectUnlockedCells = false;
                worksheet.Protection.AllowSort = false;
            }
            var titleRow = 0;
            if (!string.IsNullOrWhiteSpace(title))
            {
                titleRow = 1;
                worksheet.Cells[1, 1, 1, columnNames.Count()].Merge = true;//合并单元格
                worksheet.Cells[1, 1].Value = title;
                worksheet.Cells.Style.WrapText = true;
                worksheet.Cells[1, 1].Style.Font.Bold = true;//字体为粗体
                worksheet.Cells[1, 1].Style.Font.Color.SetColor(Color.Black);//字体颜色
                worksheet.Cells[1, 1].Style.Font.Name = "微软雅黑";//字体
                worksheet.Cells[1, 1].Style.Font.Size = 12;//字体大小
                worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                worksheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
                worksheet.Row(1).Height = 30;//设置行高
                //worksheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
            }
            // worksheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
            // worksheet.Row(1).Height = 5;//设置行高

            //worksheet.Row(1).CustomHeight = true;//自动调整行高
            //worksheet.Column(1).Width = 15;//设置列宽
            //获取要反射的属性,加载首行
            Type myType = typeof(T);
            List<PropertyInfo> myPro = new List<PropertyInfo>();
            int i = 1;
            foreach (string key in columnNames.Keys)
            {
                PropertyInfo p = myType.GetProperty(key);
                myPro.Add(p);

                worksheet.Cells[1 + titleRow, i].Value = columnNames[key];
                worksheet.Cells[1 + titleRow, i].Style.Font.Bold = true;//字体为粗体
                worksheet.Cells[1 + titleRow, i].Style.Font.Color.SetColor(Color.Black);//字体颜色
                worksheet.Cells[1 + titleRow, i].Style.Font.Name = "微软雅黑";//字体
                worksheet.Column(i).Width = 18;//设置列宽
                i++;
            }

            int row = 2 + titleRow;
            foreach (T data in datas)
            {
                int column = 1;
                foreach (PropertyInfo p in myPro.Where(info => !outOfColumns.Contains(info.Name)))
                {
                    //worksheet.Cells[4, 7].Style.Numberformat.Format = "￥#,###.00";
                    worksheet.Cells[row, column].Value = p == null ? "" : Convert.ToString(p.GetValue(data, null));
                    worksheet.Row(row).Height = 15;//设置行高
                    column++;
                }
                row++;
            } //添加汇总
            if (total != null)
            {
                worksheet.Cells[row, 1].Value = "汇总";
                foreach (var item in total)
                {
                    worksheet.Cells[row, item.Key].Value = Convert.ToString(item.Value);
                    worksheet.Row(row).Height = 15;//设置行高
                }
            }
            return package;
        }
        public static Byte[] GetByteToExportExcel<T>(List<T> datas, Dictionary<string, string> columnNames, List<string> outOfColumn, string sheetName = "Sheet1", string title = "", int isProtected = 0, Dictionary<int, string> total = null)
        {
            using (var fs = new MemoryStream())
            {
                using (var package = CreateExcelPackage(datas, columnNames, outOfColumn, sheetName, title, isProtected, total))
                {
                    package.SaveAs(fs);
                    return fs.ToArray();
                }
            }
        }
    }
}
