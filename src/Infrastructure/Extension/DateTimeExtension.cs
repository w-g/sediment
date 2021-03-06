﻿using System.Globalization;

namespace System
{
    /// <summary>
    /// 日期的扩展
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 获取指定日所在周
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(this DateTime dateTime)
        {
            GregorianCalendar calendar = new GregorianCalendar(GregorianCalendarTypes.Localized);

            return calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>
        /// 获取星期几
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDayofWeek(this DateTime dateTime)
        {
            string dayofweek = string.Empty;

            dayofweek = string.Format("星期{0}", DateTimeExtension.GetWeekString(dateTime));

            return dayofweek;
        }

        /// <summary>
        /// 将当前 DateTime 对象的值转换为其等效的友好时间字符串表示形式
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToFriendlyDateString(this DateTime dateTime)
        {
            string friendlyDate = string.Empty;

            // 校准时间
            dateTime = dateTime.ToLocalTime();
            DateTime now = DateTime.Now;

            TimeSpan span = dateTime - now;

            // 当前的前后三十天内
            if (Math.Abs(span.Days) <= 30)
            {
                int week_span = dateTime.GetWeekOfYear() - now.GetWeekOfYear();
                int day_span = (dateTime.Date - now.Date).Days;

                // 当天
                if (span.Days == 0)
                {
                    if (span.Minutes == 0)
                    {
                        friendlyDate = "现在";
                    }
                    // 当前的前后一小时之内
                    else if (Math.Abs(span.Hours) <= 1)
                    {
                        if (span.Minutes > 0)
                        {
                            friendlyDate = string.Format("{0}分钟后", Math.Abs(span.Minutes));
                        }
                        else
                        {
                            friendlyDate = string.Format("{0}分钟前", Math.Abs(span.Minutes));
                        }
                    }
                    // 当前的前后一小时之外
                    else
                    {
                        if (span.Hours > 0)
                        {
                            friendlyDate = string.Format("{0}小时后", Math.Abs(span.Hours));
                        }
                        else
                        {
                            friendlyDate = string.Format("{0}小时前", Math.Abs(span.Hours));
                        }
                    }
                }
                // 当前的后三十天内
                else if (span.Days > 0)
                {
                    // 同一年
                    if (dateTime.Year == now.Year)
                    {
                        // 不同周
                        if (week_span == 1)
                        {
                            friendlyDate = string.Format("下周{0}", DateTimeExtension.GetWeekString(dateTime));
                        }
                        // 同一周
                        else
                        {
                            // n天后
                            friendlyDate = string.Format("{0}天后", Math.Abs(day_span));
                        }
                    }
                    else
                    {
                        // n天后
                        friendlyDate = string.Format("{0}天后", Math.Abs(day_span));
                    }
                }
                // 当前的前三十天内
                else
                {
                    // 同一年
                    if (dateTime.Year == now.Year)
                    {
                        if (week_span == -1)
                        {
                            friendlyDate = string.Format("上周{0}", DateTimeExtension.GetWeekString(dateTime));
                        }
                        else
                        {
                            friendlyDate = string.Format("{0}天前", Math.Abs(day_span));
                        }
                    }
                    else
                    {
                        friendlyDate = string.Format("{0}天前", Math.Abs(day_span));
                    }
                }
            }
            else
            {
                if (dateTime.Year != now.Year)
                {
                    friendlyDate = dateTime.ToString("yyyy年MM月dd日");
                }
                else
                {
                    friendlyDate = dateTime.ToString("MM月dd日");
                }
            }

            return friendlyDate;
        }

        /// <summary>
        /// 获得星期描述
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static string GetWeekString(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "一";

                case DayOfWeek.Tuesday:
                    return "二";

                case DayOfWeek.Wednesday:
                    return "三";

                case DayOfWeek.Thursday:
                    return "四";

                case DayOfWeek.Friday:
                    return "五";

                case DayOfWeek.Saturday:
                    return "六";

                case DayOfWeek.Sunday:
                    return "日";

                default:
                    return string.Empty;
            }
        }
    }
}
