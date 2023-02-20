namespace EfDemo.Api.Model;

public class AuthorEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    // https://zditect.com/blog/10938955.html
    // https://code-maze.com/dotnet-collections-ienumerable-iqueryable-icollection/
    public ICollection<ArticleEntity> Articles { get; set; }
}