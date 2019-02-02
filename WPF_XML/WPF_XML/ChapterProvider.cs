using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DynamicExpresso;

namespace WPF_XML
{
    public class ChapterProvider
    {
        public static ObservableCollection<Chapter> Chapters = new ObservableCollection<Chapter>(); // ObservableCollection а не list чтоб могло обновляться во вью

        public static ObservableCollection<Chapter> ParseXml(string path) { //считываем данные из xml
            Chapters = new ObservableCollection<Chapter>();
            XDocument doc = XDocument.Load(path);
            string exp, result;

            var interpreter = new Interpreter(); //для вычисляемого поля

            foreach (XElement chapterElement in doc.Root.Elements("Chapters").Elements("Chapter")) //перебираем главы
            {
                XAttribute captionAttribute = chapterElement.Attribute("Caption");

                Chapter chapter = new Chapter();
                chapter.Positions = new List<Position>();
                chapter.Сaption = captionAttribute.Value;

                foreach (XElement positionElement in chapterElement.Elements("Position")) //перебираем позиции в главе
                {
                    Position position = new Position();
                    position.Resources = new List<Resource>();

                    XAttribute numberAttributePos = positionElement.Attribute("Number");
                    XAttribute codeAttributePos = positionElement.Attribute("Code");
                    XAttribute captionAttributePos = positionElement.Attribute("Caption");
                    XAttribute unitsAttributePos = positionElement.Attribute("Units");

                    exp = positionElement.Elements().First().FirstAttribute.Value; //вычисляемое поле
                    exp = exp.Replace(",", "."); //меняем формат экселя

                    result = interpreter.Eval(exp).ToString(); //опенсорс библиотечка, все безопасно
                    result = Math.Round(Convert.ToDouble(result), 3).ToString(); //округляем до 3 знаков

                    position.Number = numberAttributePos.Value;
                    position.Code = codeAttributePos.Value;
                    position.Сaption = captionAttributePos.Value;
                    position.Units = unitsAttributePos.Value;
                    position.Fx = result;

                    foreach (XElement resourceElement in positionElement.Elements("Resources").Elements()) //перебираем ресурсы в позиции
                    {
                        Resource resource = new Resource();

                        XAttribute codeAttributeRes = resourceElement.Attribute("Code");
                        XAttribute captionAttributeRes = resourceElement.Attribute("Caption");
                        XAttribute quantAttributeRes = resourceElement.Attribute("Quantity");

                        resource.Code = codeAttributeRes.Value;
                        resource.Сaption = captionAttributeRes.Value;

                        if (quantAttributeRes != null) { resource.Quantity = quantAttributeRes.Value; }
                        else { resource.Quantity = "0"; } //пока не спросил почему бывает пустое поле

                        position.Resources.Add(resource);
                    }

                    chapter.Positions.Add(position);
                }

                Chapters.Add(chapter); //добавляем в колекцию главу

            }

            return Chapters;
        }

    }

}
