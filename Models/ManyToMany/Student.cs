namespace EfCoreRelationshipDemo.Models.ManyToMany;

public class Student
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();

    private Student() { }

    public Student(Guid id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }

    public Student(string fullName) :
        this(Guid.NewGuid(), fullName)
    { }
}
