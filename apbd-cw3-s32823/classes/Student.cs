namespace apbd_cw3_s32823.Classes;

public class Student : User
{
    public Student(string firstName, string lastName) : base(firstName, lastName)
    {
        UserType = "Student";
    }
}