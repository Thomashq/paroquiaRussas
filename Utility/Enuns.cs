using System.ComponentModel;

namespace paroquiaRussas.Utility
{
    public class Enum
    {
        public enum Role
        {
            [Description("Administrador")]
            Admin = 0,
            [Description("Escritor")]
            Writer = 1
        }

        public static string GetEnumDescription(System.Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo != null)
            {
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }

            return value.ToString();
        }
    }

}