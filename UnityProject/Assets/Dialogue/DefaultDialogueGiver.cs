using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DefaultDialogueGiver : MonoBehaviour
{
    #region Ink's dialogue
    [SerializeField]
    protected TextAsset _dialogueJson;
    public static KeyCode _interactButton = KeyCode.E;
    public static KeyCode _exitButton = KeyCode.Escape;
    protected Story _dialogue;
    protected string _answer;
    protected bool _isInDialogue;
    [SerializeField]
    protected string _npcName;
    protected virtual void StartDialogue()
    {
        _dialogue = new Story(_dialogueJson.text);
        _isInDialogue = true;
    }
    protected virtual void EndDialogue()
    {
        _isInDialogue = false;
    }
    protected virtual void InterruptDialogue()
    {
        _isInDialogue = false;
    }
    public void GiveAnswerSafe(int index)
    {
        if (_isInDialogue)
        {
            GiveAnswer(index);
        }
    }
    protected virtual void GiveAnswer(int index)
    {
        _dialogue.ChooseChoiceIndex(index);
    }
    #endregion
    #region Trigger
    private DialogueGiverTrigger _trigger;
    private bool _playerIsInZone;
    private void InteractTrigger(GameObject go, bool state)
    {
        if (go.name == "Player" || go.tag == "Player")
        {
            _playerIsInZone = state;
        }
    }
    #endregion
    #region Monobehaviour's
    protected virtual void Awake()
    {
        if (string.IsNullOrEmpty(_npcName))
            _npcName = gameObject.name;
        _trigger = GetComponentInChildren<DialogueGiverTrigger>();
        _trigger.onTriggerEnter = (GameObject s) =>
        {
            InteractTrigger(s, true);
            DialogueUI.Instance.ShowTip(true);
        };
        _trigger.onTriggerExit = (GameObject s) =>
        {
            InteractTrigger(s, false);
            DialogueUI.Instance.ShowTip(false);
        };
    }
    private void Update()
    {
        if (Input.GetKeyDown(_interactButton) && _playerIsInZone)
        {
            StartDialogue();
        }
        else if (Input.GetKeyDown(_exitButton) && _isInDialogue)
        {
            InterruptDialogue();
        }
    }
    #endregion
}
