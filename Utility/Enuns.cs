using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace paroquiaRussas.Utility
{
    public class Enum
    {
        public enum Role
        {
            [Description("Administrador")]
            [Display(Name = "Administrador")]
            Admin = 0,
            [Description("Escritor")]
            [Display(Name = "Escritor")]
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

            // Se não houver um atributo Description, retorna o nome do enum como padrão
            return value.ToString();
        }
    }

}