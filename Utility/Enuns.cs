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

        public static string GetEnumDescriptio(Enum value)
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

            // Se não houver um atributo Description, retorna o nome do enum como padrão
            return value.ToString();
        }
    }

}