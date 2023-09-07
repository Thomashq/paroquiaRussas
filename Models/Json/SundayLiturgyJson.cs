namespace paroquiaRussas.Models.Json
{
    public class SundayLiturgyJson : LiturgyJsonBase
    {
        public ReadingJson primeiraLeitura { get; set; }

        public ReadingJson? segundaLeitura { get; set; }

        public PsalmJson salmo { get; set; }

        public ReadingJson evangelho { get; set; }
    }
}
