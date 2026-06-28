namespace EfCoreRelationshipDemo.Models.OneToOne;

public class Passport
{
    public Guid PersonId { get; private set; }
    public string Number { get; private set; }
    public Person Person { get; private set; }

    private Passport() { }

    public Passport(string number, Person person)
    {
        PersonId = person.Id;
        Number = number;
        Person = person;
    }
}
