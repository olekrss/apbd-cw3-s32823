namespace apbd_cw3_s32823.Classes;

public abstract class User
{
    public Guid ID { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string UserType { get; protected set; }

    protected User(string firstName, string lastName)
    {
        ID = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
    }
}