namespace EfCoreRelationshipDemo.Models.ManyToMany;

public class Course
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();

    private Course() { }

    public Course(Guid id, string title)
    {
        Id = id;
        Title = title;
    }

    public Course(string title) :
        this(Guid.NewGuid(), title)
    { }
}
