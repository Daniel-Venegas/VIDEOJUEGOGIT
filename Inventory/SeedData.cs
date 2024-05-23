using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedData : ItemData
{
    //Tiempo en el que la semilla para madurar
    public int daysToGrow;

    //la cosecha que da la semilla
    public ItemData cropToYield;

    public GameObject seedling;

    [Header("Regrowable")]
    public bool regrowable
        ;
    public int daysToRegrow;

}
