namespace EfCoreRelationshipDemo.Models.OneToOne;

public class Person
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Passport Passport { get; private set; }

    private Person() { }

    public Person(Guid id, string name, Passport passport)
    {
        Id = id;
        Name = name;
        Passport = passport;
    }

    public Person(string name, Passport passport) :
        this(Guid.NewGuid(), name, passport)
    { }

    public void SetPassport(Passport passport)
    {
        Passport = passport;
    }
}
