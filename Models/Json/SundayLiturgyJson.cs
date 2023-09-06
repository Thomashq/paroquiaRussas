namespace paroquiaRussas.Models.Json
{
    public class SundayLiturgyJson
    {
        public string data { get; set; }
        public string liturgia { get; set; }
        public string cor { get; set; }
        public string dia { get; set; }
        public string oferendas { get; set; }
        public string comunhao { get; set; }
        public ReadingJson primeiraLeitura { get; set; }
        public ReadingJson? segundaLeitura { get; set; }
        public PsalmJson salmo { get; set; }
        public ReadingJson evangelho { get; set; }
    }
}
