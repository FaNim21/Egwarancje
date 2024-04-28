using SkiaSharp;

namespace Egwarancje.Utils;

public class Helper
{
    public static byte[] CompressImage(byte[] imageData, int quality)
    {
        using var bitmap = SKBitmap.Decode(imageData) ?? throw new ArgumentException("Invalid image data.");
        using var image = SKImage.FromBitmap(bitmap);
        using var memoryStream = new MemoryStream();

        image.Encode(SKEncodedImageFormat.Jpeg, quality).SaveTo(memoryStream);
        return memoryStream.ToArray();
    }
}
