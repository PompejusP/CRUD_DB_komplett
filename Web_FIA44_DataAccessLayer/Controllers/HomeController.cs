using Microsoft.AspNetCore.Mvc;
using Web_FIA44_DataAccessLayer.DAL;
using Web_FIA44_DataAccessLayer.Models;

namespace Web_FIA44_DataAccessLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccessable dal;
        public HomeController(IConfiguration conf)
        {
            //der connectionString wird aus der appsettings.json Datei gelesen und in die Variable connectionString geschrieben
            //"ConnectionStrings": {
            // "SqlServer": "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=DalDemo;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=True"
            //die ist der server der Datenbank, der Name der Datenbank, die Art der Authentifizierung und ob die Datenbank verschlüsselt ist
            string connString = conf.GetConnectionString("SqlServer");
            dal = new SqlDal(connString);

        }
        #region Alle Artikel einsehen bzw Startseite anzeigen
        [HttpGet]
        public IActionResult Index()
        {

            List<Article> AllArticle = dal.GetAllArticles();
            return View(AllArticle);
        }
        #endregion
        #region Neuen Artikel erstellen
        [HttpGet]
        public IActionResult Create()
        {
            //Artikel mit werten vorbelegen und in die View schicken
            //Article article = new Article();
            //article.Quantity = 0;
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Article article)
        {if (!ModelState.IsValid)
            {
                return View(article);
            }
            dal.InsertArticle(article);
            return RedirectToAction("Index");
        }
        public IActionResult delete(int Aid)
        {
            dal.DeleteArticleById(Aid);
            return RedirectToAction("Index");
        }
        #endregion
        #region Artikel editieren bzw. Ändern
        [HttpGet]
        public IActionResult Update(int Aid)
        {
            Article article = dal.GetArticleById(Aid);
            return View(article);
        }
        [HttpPost]
        public IActionResult Update(Article article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }
            dal.UpdateArticle(article);
            return RedirectToAction("Index");
        }
        #endregion
        #region Artikel in Detailansicht anzeigen lassen
        [HttpGet]
        public IActionResult Details(int Aid)
        {
            Article article = dal.GetArticleById(Aid);
            return View(article);
        }
        #endregion
    }
}
