using Newtonsoft.Json;
using paroquiaRussas.Models;
using paroquiaRussas.Models.Json;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Factory.LiturgyFactory;
using paroquiaRussas.Utility.Factory.LiturgyFactory.Interface;

namespace paroquiaRussas.Mapper
{
    public static class LiturgyMapper
    {
        public static LiturgyModel LiturgyJsonToModel(object liturgyJson)
        {
            ILiturgyInterface liturgyFactory;

            SundayLiturgyJson? sundayLiturgy = new SundayLiturgyJson();
            sundayLiturgy = null;

            WeekLiturgyJson? weekLiturgyJson = new WeekLiturgyJson();
            weekLiturgyJson = null;

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                liturgyFactory = new SundayLiturgyFactory();
                sundayLiturgy = liturgyFactory.CreateSundayLiturgy(JsonConvert.SerializeObject(liturgyJson));
            }
            else
            {
                liturgyFactory = new WeekLiturgyFactory();
                weekLiturgyJson = liturgyFactory.CreateWeeklyLiturgy(JsonConvert.SerializeObject(liturgyJson));
            }

            LiturgyModel liturgyModel = ParseJsonToModel(weekLiturgyJson, sundayLiturgy);
            liturgyModel.primeiraLeitura.texto = LiturgyEditor.FormatLiturgyText(liturgyModel.primeiraLeitura.texto);
            liturgyModel.evangelho.texto = LiturgyEditor.FormatLiturgyText(liturgyModel.evangelho.texto);
            liturgyModel.salmo.texto = LiturgyEditor.FormatSalmText(liturgyModel.salmo.texto);

            if(liturgyModel.segundaLeituraDomingo != null)
                liturgyModel.segundaLeituraDomingo.texto = LiturgyEditor.FormatLiturgyText(liturgyModel.segundaLeituraDomingo.texto);

            return liturgyModel;
        }

        private static LiturgyModel ParseJsonToModel(WeekLiturgyJson? weekLiturgyJson, SundayLiturgyJson? sundayLiturgy)
        {
            LiturgyModel liturgyModel = new LiturgyModel();

            if (sundayLiturgy == null)
            {
                liturgyModel.cor = weekLiturgyJson.cor;
                liturgyModel.data = weekLiturgyJson.data;
                liturgyModel.liturgia = weekLiturgyJson.liturgia;
                liturgyModel.primeiraLeitura = weekLiturgyJson.primeiraLeitura;
                liturgyModel.salmo = weekLiturgyJson.salmo;
                liturgyModel.evangelho = weekLiturgyJson.evangelho;
                liturgyModel.segundaLeitura = weekLiturgyJson.segundaLeitura;
                liturgyModel.oferendas = weekLiturgyJson.oferendas;
                liturgyModel.dia = weekLiturgyJson.dia;
                liturgyModel.comunhao = weekLiturgyJson.comunhao;
            }
            else
            {
                liturgyModel.cor = sundayLiturgy.cor;
                liturgyModel.data = sundayLiturgy.data;
                liturgyModel.liturgia = sundayLiturgy.liturgia;
                liturgyModel.primeiraLeitura = sundayLiturgy.primeiraLeitura;
                liturgyModel.salmo = sundayLiturgy.salmo;
                liturgyModel.evangelho = sundayLiturgy.evangelho;
                liturgyModel.segundaLeituraDomingo = sundayLiturgy.segundaLeitura;
                liturgyModel.oferendas = sundayLiturgy.oferendas;
                liturgyModel.dia = sundayLiturgy.dia;
                liturgyModel.comunhao = sundayLiturgy.comunhao;
            }

            return liturgyModel;
        }
    }
}
