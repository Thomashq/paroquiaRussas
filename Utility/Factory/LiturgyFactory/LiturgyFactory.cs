using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using paroquiaRussas.Models.Json;
using paroquiaRussas.Utility.Factory.LiturgyFactory.Interface;

namespace paroquiaRussas.Utility.Factory.LiturgyFactory
{
    public class WeekLiturgyFactory : ILiturgyInterface
    {
        public WeekLiturgyJson CreateWeeklyLiturgy(string jsonResult)
        {
            try
            {
                WeekLiturgyJson liturgyJson = JsonConvert.DeserializeObject<WeekLiturgyJson>(jsonResult);

                return liturgyJson;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
    
    public class SundayLiturgyFactory : ILiturgyInterface
    {
        public SundayLiturgyJson CreateSundayLiturgy(string jsonResult)
        {
            try
            {
                SundayLiturgyJson liturgyJson = JsonConvert.DeserializeObject<SundayLiturgyJson>(jsonResult);

                return liturgyJson;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
