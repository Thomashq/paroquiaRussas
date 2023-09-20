using paroquiaRussas.Models.Json;

namespace paroquiaRussas.Models
{
    public class LiturgyModel : LiturgyJsonBase
    {
        public ReadingJson primeiraLeitura { get; set; }

        public string segundaLeitura { get; set; }

        public ReadingJson? segundaLeituraDomingo { get; set; }

        public PsalmJson salmo { get; set; }

        public ReadingJson evangelho { get; set; }

    }
}
