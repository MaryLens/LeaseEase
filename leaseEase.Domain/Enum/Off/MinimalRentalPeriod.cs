using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.Domain.Enum.Off
{
    public enum MinimalRentalPeriod
    {
        [Description("3 days")]
        [Day(3)]
        threeDays = 1,

        [Description("1 week")]
        [Day(7)]
        oneweek = 2,

        [Description("2 weeks")]
        [Day(14)]
        twoweeks = 3,

        [Description("1 month")]
        [Day(30)]
        onemonth = 4,

        [Description("2 months")]
        [Day(60)]
        twomonth = 5,

        [Description("6 months")]
        [Day(180)]
        sixmonth = 6,

        [Description("No preference")]
        [Day(0)]
        NoPreference = 7
    }

    public class DescriptionAttribute : Attribute
    {
        public string Text { get; }

        public DescriptionAttribute(string text)
        {
            Text = text;
        }
    }
    public class DayAttribute : Attribute
    {
        public int Days { get; }

        public DayAttribute(int days)
        {
            Days = days;
        }
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this System.Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute?.Text ?? value.ToString();
        }
        public static int GetDay(this System.Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DayAttribute)Attribute.GetCustomAttribute(field, typeof(DayAttribute));
            return attribute?.Days ?? 0;
        }
    }
}