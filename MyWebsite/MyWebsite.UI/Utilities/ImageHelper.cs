namespace MyWebsite.UI.Utilities
{
    public static class ImageHelper
    {
        public static string GetImageSource(byte[]? imageData, string defaultImagePath = "assets/img/product/1.png")
        {
            return imageData is { Length: > 0 } ? $"data:image/png;base64,{Convert.ToBase64String(imageData)}" : defaultImagePath;
        }
    }
}
