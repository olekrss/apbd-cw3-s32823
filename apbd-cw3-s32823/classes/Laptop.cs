namespace apbd_cw3_s32823.Classes;

public class Laptop : Equipment
{
    public int RamSizeGb { get; private set; }
    public string ProcessorType { get; private set; }

    public Laptop(string name, int ramSizeGb, string processorType) : base(name)
    {
        RamSizeGb = ramSizeGb;
        ProcessorType = processorType;
    }
}