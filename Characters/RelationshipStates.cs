using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RelationshipStates : MonoBehaviour
{
    public static List<NPCRelationshipState> relationships = new List<NPCRelationshipState>();


    public static void LoadStats(List<NPCRelationshipState> relatioshipsToLoad)
    {
        if (relatioshipsToLoad == null)
        {
            relationships = new List<NPCRelationshipState>();
            return;
        }
        relationships = relatioshipsToLoad;
    }
    public static bool FirstMeeting(CharacterData character)
    {
        return !relationships.Exists(i => i.name == character.name);
    }

    public static NPCRelationshipState GetNPCRelationship(CharacterData character)
    {
        if (FirstMeeting(character)) return null;

        return relationships.Find(i =>  i.name == character.name);
    }

    public static void UnlockCharacter(CharacterData character)
    {
        relationships.Add(new NPCRelationshipState(character.name));
    }

    public static void AddFriendPoints(CharacterData character, int points)
    {
        if (FirstMeeting(character))
        {
            Debug.LogError("the player has not met this character yet!");
            return;
        }
        GetNPCRelationship(character).friendshipPoints += points;
    }

    public static bool IsFirstConversationOfTheDay (CharacterData character)
    {
        if (FirstMeeting(character)) return true;
        NPCRelationshipState npc = GetNPCRelationship(character);
        return !npc.hasTalkedToday;
    }
}
