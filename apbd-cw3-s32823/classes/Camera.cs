namespace apbd_cw3_s32823.Classes;

public class Camera : Equipment
{
    public double MegaPixels { get; private set; }
    public bool HasLensIncluded { get; private set; }

    public Camera(string name, double megaPixels, bool hasLensIncluded) : base(name)
    {
        MegaPixels = megaPixels;
        HasLensIncluded = hasLensIncluded;
    }
}