namespace apbd_cw3_s32823.Classes;

public abstract class Equipment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
    
    protected Equipment(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        IsAvailable = true;
    }
    
    public void SetAvailable(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }
}