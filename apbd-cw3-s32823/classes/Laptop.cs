namespace apbd_cw3_s32823.Classes;

public class Laptop : Equipment
{
    public int RamSizeGb { get; set; }
    public string ProcessorType { get; set; }

    public Laptop(string name, int ramSizeGb, string processorType) : base(name)
    {
        RamSizeGb = ramSizeGb;
        ProcessorType = processorType;
    }
}