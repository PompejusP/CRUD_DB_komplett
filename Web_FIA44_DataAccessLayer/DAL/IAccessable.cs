using Web_FIA44_DataAccessLayer.Models;

namespace Web_FIA44_DataAccessLayer.DAL
{
    public interface IAccessable
    {
        // Die 4 Crud - Methoden für die Datenbank + 1 Methode für die ausgabe aller Artikel
        // Mit diesen Methoden kann ich alles machen was ich mit der Datenbank machen will
        int InsertArticle(Article article);
        Article GetArticleById(int Aid);
        List<Article> GetAllArticles();
        bool UpdateArticle(Article article);
        bool DeleteArticleById(int Aid);

        //weitere nützliche Methoden können hier auch enthalten sein aber die 4+1 Methoden sind die wichtigsten
        // Die 4+1 Methoden sind notwendig um die Datenbank zu verwalten und die Artikel zu verwalten
    }
}
