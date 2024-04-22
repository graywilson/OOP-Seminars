using OOP_Seminars.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using OfficeOpenXml;

namespace OOP_Seminars.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DecisionAlgorithm d = new DecisionAlgorithm();
            return View();
        }

        [HttpGet]
        public ActionResult LoadData()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult LoadExel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadExcel(HttpPostedFileBase fileUpload, string algorithmSelect, string forestArraySelect)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Установка контекста лицензии

            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                try
                {
                    // Использование ExcelPackage для чтения данных
                    using (var package = new ExcelPackage(fileUpload.InputStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        int rowCount = worksheet.Dimension.Rows;

                        // Создание списка для хранения данных из Excel
                        List<TreeData> treeDataList = new List<TreeData>();

                        // Чтение данных из Excel и сохранение их в модель TreeData
                        for (int row = 2; row <= rowCount; row++)
                        {
                            double xCoordinate = Convert.ToDouble(worksheet.Cells[row, 2].Value);
                            double yCoordinate = Convert.ToDouble(worksheet.Cells[row, 3].Value);

                            // Проверка, что обе координаты не равны 0
                            if (xCoordinate != 0 && yCoordinate != 0)
                            {
                                TreeData data = new TreeData
                                {
                                    PointNumber = worksheet.Cells[row, 1].Value?.ToString(),
                                    X = xCoordinate/100000,
                                    Y = yCoordinate/100000,
                                    TreeType = worksheet.Cells[row, 4].Value?.ToString(),
                                    Diameter = worksheet.Cells[row, 5].Value?.ToString()
                                };
                                treeDataList.Add(data);
                            }
                        }

                        // Передача списка treeDataList в представление ResultExcel
                        return View("ResultExcel", treeDataList);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Ошибка при обработке файла: " + ex.Message;
                    return View("ErrorView");
                }
            }

            ViewBag.Error = "Файл не был загружен";
            return View("ErrorView");
        }

        [HttpPost]
        public ActionResult LoadData(HttpPostedFileBase fileUpload, string algorithmSelect, string forestArraySelect)
        {
            ViewBag.algorithmSelect = algorithmSelect;
            ViewBag.forestArraySelect = forestArraySelect;
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                // Чтение данных из загруженного XML-файла
                var forestAreaModel = new ForestAreaModel();
                using (var stream = fileUpload.InputStream)
                {
                    var serializer = new XmlSerializer(typeof(ForestAreaModel));
                    forestAreaModel = (ForestAreaModel)serializer.Deserialize(stream);
                }

                // Здесь можно добавить дополнительную обработку данных, например, сохранение их в базе данных

                // Перенаправляем пользователя на страницу выбора
                return View("Result", forestAreaModel);
            }

            // Если файл не был загружен, пишем об ошибке
            ViewBag.Error = "Не получилось загрузить файл!";
            return View();
        }

        [HttpGet]
        public ActionResult Result()
        {
            ViewBag.algorithmSelect = "Алгоритм1";
            ViewBag.forestArraySelect = "Метод1";
            var filePath = Server.MapPath("~/data_gk.xml"); // Получаем путь к файлу

            if (!System.IO.File.Exists(filePath))
            {
                // Обработка ошибки, если файл не найден
                return HttpNotFound();
            }

            ForestAreaModel forestAreaModel;

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(ForestAreaModel));
                forestAreaModel = (ForestAreaModel)serializer.Deserialize(stream);
            }

            return View(forestAreaModel);
        }

        [HttpGet]
        public ActionResult ResultExcel()
        {
            ViewBag.algorithmSelect = "Алгоритм1";
            ViewBag.forestArraySelect = "Метод1";
            var filePath = Server.MapPath("~/exp.xlsx"); // Получаем путь к файлу на сервере

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Установка контекста лицензии

                // Использование ExcelPackage для чтения данных
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    int rowCount = worksheet.Dimension.Rows;

                    // Создание списка для хранения данных из Excel
                    List<TreeData> treeDataList = new List<TreeData>();

                    // Чтение данных из Excel и сохранение их в модель TreeData
                    for (int row = 2; row <= rowCount; row++)
                    {
                        double xCoordinate = Convert.ToDouble(worksheet.Cells[row, 2].Value);
                        double yCoordinate = Convert.ToDouble(worksheet.Cells[row, 3].Value);

                        // Проверка, что обе координаты не равны 0
                        if (xCoordinate != 0 && yCoordinate != 0)
                        {
                            TreeData data = new TreeData
                            {
                                PointNumber = worksheet.Cells[row, 1].Value?.ToString(),
                                X = xCoordinate / 100000,
                                Y = yCoordinate / 100000,
                                TreeType = worksheet.Cells[row, 4].Value?.ToString(),
                                Diameter = worksheet.Cells[row, 5].Value?.ToString()
                            };
                            treeDataList.Add(data);
                        }
                    }

                    // Передача списка treeDataList в представление ResultExcel
                    return View("ResultExcel", treeDataList);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ошибка при обработке файла: " + ex.Message;
                return View("ErrorView");
            }
        }
    }
}