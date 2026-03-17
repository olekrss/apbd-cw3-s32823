namespace apbd_cw3_s32823.Classes;

public class Employee : User
{
    public Employee(string firstName, string lastName) : base(firstName, lastName)
    {
        UserType = "Employee";
    }
}