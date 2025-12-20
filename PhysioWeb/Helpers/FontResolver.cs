using System.Reflection;
using PdfSharp.Fonts;

namespace PhysioWeb.Helpers
{
    public class FontResolver : IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            if (faceName == "Arial#")
            {
                return GetFontBytes("YourNamespace.Fonts.arial.ttf");
            }

            return null;
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("Arial", StringComparison.OrdinalIgnoreCase))
            {
                return new FontResolverInfo("Arial#");
            }

            return null;
        }

        private byte[] GetFontBytes(string resourceName)
        {
            using Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(resourceName);

            using MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
