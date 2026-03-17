namespace apbd_cw3_s32823.Classes;

public class Projector : Equipment
{
    public string Resolution { get; private set; }
    public int Brightness { get; private set; }

    public Projector(string name, string resolution, int brightness) : base(name)
    {
        Resolution = resolution;
        Brightness = brightness;
    }
}