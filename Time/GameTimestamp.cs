using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameTimestamp 
{
    public int year;
    public enum Season
    {
        Primavera,
        Verano,
        Otoño,
        Invierno
    }
    public Season season;

    public enum DayOfTheWeek
    {
        
        Martes,
        Miércoles,
        Jueves,
        Viernes,
        Sábado,
        Domingo,
        Lunes


    }
    public int day;
    public int hour;
    public int minute;

    public GameTimestamp(int year, Season season, int day, int hour, int minute)
    {
        this.year = year;
        this.season = season;   
        this.day = day; 
        this.hour = hour;
        this.minute = minute;
    }

    public GameTimestamp(GameTimestamp timestamp)
    {
        this.year = timestamp.year;
        this.season = timestamp.season;
        this.day = timestamp.day;
        this.hour = timestamp.hour;
        this.minute = timestamp.minute;
    }

    public void UpdateClock()
    {
        minute++;
        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if (hour >= 24)
        {
            hour = 0;
            day++;
        }
        if(day> 30)
        {
            day = 1;
            if(season== Season.Invierno)
            {
                season= Season.Verano;
                year++;
            }
            else
            {
                season++;
            }
            
        }
    }

    public DayOfTheWeek GetDayOfTheWeek()
    {
        int daysPassed = YearsToDays(year) + SeasonToDays(season) + day;
        int dayIndex = daysPassed % 7;
        return (DayOfTheWeek)dayIndex;
    }

    public static int HoursToMinutes(int hour)
    {
        return hour * 60;
    }

    public static int DaysToHours(int days)
    {
        return days * 24;
    }
    public static int SeasonToDays(Season season)
    {
        int seasonIndex = (int)season;
        return seasonIndex * 30;
    }

    public static int YearsToDays(int years)
    {
        return years * 4 * 30;
    }
    public static int TimestampInMinutes( GameTimestamp timestamp)
    {
        return HoursToMinutes(DaysToHours(YearsToDays(timestamp.year)) + DaysToHours(SeasonToDays(timestamp.season)) + DaysToHours(timestamp.day) + timestamp.hour) + timestamp.minute ;
    }

    public static int ComparesTimestamps(GameTimestamp timestamp1, GameTimestamp timestamp2)
    {
        int timestamp1Hours = DaysToHours(YearsToDays(timestamp1.year)) + DaysToHours(SeasonToDays(timestamp1.season)) + DaysToHours(timestamp1.day) + timestamp1.hour;
        int timestamp2Hours = DaysToHours(YearsToDays(timestamp2.year)) + DaysToHours(SeasonToDays(timestamp2.season)) + DaysToHours(timestamp2.day) + timestamp2.hour;
        int difference = timestamp2Hours - timestamp1Hours; 
        return Mathf.Abs(timestamp2Hours - timestamp1Hours);
    }
}
