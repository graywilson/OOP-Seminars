using OOP_Seminars.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

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
    }
}