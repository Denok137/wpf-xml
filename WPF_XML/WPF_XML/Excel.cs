using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace WPF_XML
{
    class Excel
    {
        public static void GenerateExcelFile(ObservableCollection<Chapter> chapters, string pathSave)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];

            int currentRow = 0; //тут всегда будет текущая строка чтоб знать куда записывать данные

            int firstChapterRow; // для группировки запоминаем начало главы
            int currentColPos; //будем бегать по колонкам позиций
            int currentColPosRes; //будем бегать по колонкам ресурсов

            for (int currentChap = 0; currentChap < chapters.Count; currentChap++) //бегаем по главам
            {
                currentRow++;

                firstChapterRow = currentRow+1;

                ws.Cells[currentRow, 1].Value = chapters[currentChap].Сaption;
                ws.Range[ws.Cells[currentRow, 1], ws.Cells[currentRow, 6]].Merge(); //объединяем

                for (int currentPos = 0; currentPos < chapters[currentChap].Positions.Count; currentPos++) //бегаем по позициям
                {
                    currentRow++;

                    currentColPos = 2;
                    ws.Cells[currentRow, currentColPos].Value = chapters[currentChap].Positions[currentPos].Number;
                    ws.Cells[currentRow, ++currentColPos].Value = chapters[currentChap].Positions[currentPos].Code;
                    ws.Cells[currentRow, ++currentColPos].Value = chapters[currentChap].Positions[currentPos].Сaption;
                    ws.Cells[currentRow, ++currentColPos].Value = chapters[currentChap].Positions[currentPos].Units;
                    ws.Cells[currentRow, ++currentColPos].Value = chapters[currentChap].Positions[currentPos].Fx;

                    for (int currentResource = 0; currentResource < chapters[currentChap].Positions[currentPos].Resources.Count; currentResource++) //бегаем по ресурсам
                    {
                        currentRow++;
                        currentColPosRes = 3;
                        ws.Cells[currentRow, currentColPosRes].Value = chapters[currentChap].Positions[currentPos].Resources[currentResource].Code;
                        ws.Cells[currentRow, ++currentColPosRes].Value = chapters[currentChap].Positions[currentPos].Resources[currentResource].Сaption;
                        ws.Cells[currentRow, ++currentColPosRes].Value = "";
                        ws.Cells[currentRow, ++currentColPosRes].Value = chapters[currentChap].Positions[currentPos].Resources[currentResource].Quantity;

                        ws.Range[ws.Cells[currentRow, 1], ws.Cells[currentRow, 6]].Rows.Group(); //группируем ресурсы
                    }
                }

                 ws.Range[ws.Cells[firstChapterRow, 1], ws.Cells[currentRow, 6]].Rows.Group(); //группируем главы

            }


            ws.Outline.SummaryRow = XlSummaryRow.xlSummaryAbove; //ставит плюсик сверху при группировке

            ws.Columns[2].ColumnWidth = 2.2;
            ws.Columns[3].ColumnWidth = 21;
            ws.Columns[4].ColumnWidth = 30.53;
            ws.Columns[5].ColumnWidth = 7.5;
            ws.Columns[6].ColumnWidth = 6.6;

            ws.Columns[4].WrapText = true; //перенос текста
            ws.Columns[5].WrapText = true;

            Range range = ws.Range[ws.Cells[1, 1], ws.Cells[currentRow, 6]];

            range.Borders.Weight = XlBorderWeight.xlThin; //тонкая
            range.Borders.LineStyle = XlLineStyle.xlContinuous; //линия

            try
            {
                ws.SaveAs(pathSave);
            }
            catch {
                //файл занят скорей всего или нет доступа
            }

        }
    }
}
