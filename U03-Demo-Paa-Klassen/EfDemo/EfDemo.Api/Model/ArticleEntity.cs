namespace EfDemo.Api.Model;

public class ArticleEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<AuthorEntity> Authors { get; set; }
}