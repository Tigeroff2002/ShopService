using System.Drawing;

namespace ShopService.Helpers;

public static class ImageHelper
{
    public static byte[] ConvertToByteArray(this Image img)
    {
        using (var stream = new MemoryStream())
        {
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
