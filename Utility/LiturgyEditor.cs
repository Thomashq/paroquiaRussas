using System.Text.RegularExpressions;

namespace paroquiaRussas.Utility
{
    public static class LiturgyEditor
    {
        public static string FormatLiturgyText(string liturgyText)
        {
            if (liturgyText == null)
                return string.Empty;

            string formatedText = Regex.Replace(liturgyText, @"(\d+)", @"<sup>$1 </sup>");

            if(formatedText.Contains("Ou:"))
                formatedText = Regex.Replace(formatedText, @",Ou:(.*)", "");

            return formatedText;
        }

        public static string FormatSalmText(string salmText)
        {
            if (salmText == null)
                return string.Empty;

            string formatedText = salmText.Replace("—", "<br> —");

            return formatedText;
        }
    }
}
