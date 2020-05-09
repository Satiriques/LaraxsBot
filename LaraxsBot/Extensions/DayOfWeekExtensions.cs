namespace System
{
    public enum DayOfWeekFrench
    {
        Dimanche,
        Lundi,
        Mardi,
        Mercredi,
        Jeudi,
        Vendredi,
        Samedi,
    }

    public static class DayOfWeekExtensions
    {
        public static DayOfWeekFrench ToFrench(this DayOfWeek dayOfWeek)
        {
            return (DayOfWeekFrench)dayOfWeek;
        }
    }
}
