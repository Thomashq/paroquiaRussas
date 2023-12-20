using paroquiaRussas.Models.Json;

namespace paroquiaRussas.Utility.Factory.LiturgyFactory.Interface
{
    public interface ILiturgyInterface
    {
        WeekLiturgyJson CreateWeeklyLiturgy(string jsonResult)
        {
            return new WeekLiturgyJson();
        }

        SundayLiturgyJson CreateSundayLiturgy(string jsonResult)
        {
            return new SundayLiturgyJson();
        }
    }
}
