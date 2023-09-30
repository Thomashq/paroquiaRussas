using paroquiaRussas.Utility.Resources;

namespace paroquiaRussas.Utility.Utilities
{
    public class ManagementImages
    {
        private const string IMAGEPATH = "wwwroot/img/";

        public static string SaveImage(string image)
        {
            if (image == null)
                throw new Exception(Exceptions.EXC24);

            string imageName = image.Split(',')[0];

            string basa64 = image.Split(',')[2];

            byte[] imageBytes = Convert.FromBase64String(basa64);

            string imagePath = Path.Combine(IMAGEPATH, imageName);

            File.WriteAllBytes(imagePath, imageBytes);

            return $"/img/{imageName}";
        }    
    }
}
