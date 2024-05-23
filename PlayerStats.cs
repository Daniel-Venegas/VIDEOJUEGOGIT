using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{

    public static int Money = 1;
    public static int Level
    {
        get { return Money / 2; } // Nivel es el dinero dividido entre dos
    }

    public const string CURRENCY = "G";
   public static void Spend(int cost)
    {
        if(cost > Money)
        {
            Debug.LogError("El jugador no tiene suficiente dinero");
            return;
        }
        Money -= cost;
        UIManager.Instance.RenderPlayerStats();
    }

    public static void Earn (int income)
    {
        Money += income;
        UIManager.Instance.RenderPlayerStats();
    }


    public static void LoadStats(int money)
    {
        Money = money;
        UIManager.Instance.RenderPlayerStats();
    }
}

