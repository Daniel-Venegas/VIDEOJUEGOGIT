using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed2 : InteractableObject
{
    public override void Pickup()
    {
        UIManager.Instance.TriggerYesNoPrompt("¿Quieres ir a dormir?", GameStateManager.Instance.Sleep);
    }
}
