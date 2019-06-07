using System.Text;

namespace WatchIt
{
    public static class TextUtils
    {
        public static string AddSpacesBeforeCapitalLetters(string text)
        {
            StringBuilder newText = new StringBuilder(text.Length * 2);

            newText.Append(text[0]);

            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                {
                    newText.Append(' ');
                }

                newText.Append(text[i]);
            }

            return newText.ToString();
        }
    }
}
