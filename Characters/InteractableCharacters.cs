using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacters : InteractableObject
{
    public CharacterData characterData;
    NPCRelationshipState relationship;

    private void Start()
    {
        relationship = RelationshipStates.GetNPCRelationship(characterData);
    }

    public override void Pickup()
    {

        List<DialogueLine> dialogueToHave = characterData.defaultDialogue;

        System.Action onDialogueEnd = null;

        if (RelationshipStates.FirstMeeting(characterData))
        {
            dialogueToHave = characterData.onFirstMeet;
            onDialogueEnd += OnFirstMeeting;
           
        }

        if (RelationshipStates.IsFirstConversationOfTheDay(characterData))
        {
            onDialogueEnd += OnFirstConversation;

        }

        DialogueManager.Instance.StartDialogue(dialogueToHave , onDialogueEnd);
    }
    void OnFirstMeeting()
    {
        RelationshipStates.UnlockCharacter(characterData);
        relationship = RelationshipStates.GetNPCRelationship(characterData);
    }

    void OnFirstConversation()
    {
        Debug.Log("This is the firts conversation of the day");
        RelationshipStates.AddFriendPoints(characterData, 20);

        relationship.hasTalkedToday = true;
    }
}
