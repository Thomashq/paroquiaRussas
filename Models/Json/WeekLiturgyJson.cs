namespace paroquiaRussas.Models.Json
{
    public class WeekLiturgyJson : LiturgyJsonBase
    {
        public ReadingJson primeiraLeitura { get; set; }

        public string segundaLeitura { get; set; }

        public PsalmJson salmo { get; set; }

        public ReadingJson evangelho { get; set; }
    }
}
