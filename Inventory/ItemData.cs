using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    public string description;

    //Icono que se mostrara en el UI
    public Sprite thumbnail;

    //GameObject que se mostrara en la escena
    public GameObject gameModel;

    public int cost;
   
}
