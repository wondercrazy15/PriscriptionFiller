using System;
namespace PrescriptionFiller.interfaces
{
    public interface IResizing
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
        byte[] ResizeImage(byte[] imageData, float sizeDesired);
    }
}
