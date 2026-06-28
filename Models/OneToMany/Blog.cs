namespace EfCoreRelationshipDemo.Models.OneToMany;

public class Blog
{
    public Guid Id { get; private set; }
    public string Url { get; private set; }
    public ICollection<Post> Posts { get; private set; } = new List<Post>();

    private Blog() { }

    internal Blog(Guid id, string url, ICollection<Post> posts)
    {
        Id = id;
        Url = url;
        Posts = posts;
    }

    public Blog(string url, ICollection<Post> posts) :
        this(Guid.NewGuid(), url, posts)
    { }
}