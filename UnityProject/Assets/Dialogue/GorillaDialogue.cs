using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class GorillaDialogue : DefaultDialogueGiver
{
    [SerializeField] private TextAsset _directDialogue;
    [SerializeField] private TextAsset _cycledDialogue;
    private bool _secondStage;
    protected override void StartDialogue()
    {
        base.StartDialogue();
        SwitchToDialogueCamera(true);
        DialogueUI.Instance.onNoSuggestions += EndDialogue;
        ChooseAnswer();
    }
    protected override void EndDialogue()
    {
        base.EndDialogue();
        if (_secondStage)
            InterruptDialogue();
        else
        {
            _dialogueJson = _cycledDialogue;
            StartDialogue();
            _secondStage = true;
        }
    }
    protected override void InterruptDialogue()
    {
        base.InterruptDialogue();
        SwitchToDialogueCamera(false);
        DialogueUI.Instance.HideAll();
        DialogueUI.Instance.StopDialogue();
        DialogueUI.Instance.onNoSuggestions -= EndDialogue;
    }
    protected override void GiveAnswer(int index)
    {
        base.GiveAnswer(index);
        ChooseAnswer();
    }
    private void ChooseAnswer()
    {
        string answer = _dialogue.currentText.Trim();
        while (_dialogue.canContinue)
        {
            _dialogue.Continue();
            answer = _dialogue.currentText.Trim();
        }

        List<string> suggestions = new List<string>();
        foreach (Choice choice in _dialogue.currentChoices)
            suggestions.Add(choice.text);

        DialogueUI.Instance.IterateDialogue(_npcName, answer, suggestions.ToArray(), this);
    }
    private Camera _dialogueCamera;
    private void SwitchToDialogueCamera(bool state)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = !state;
        _dialogueCamera.enabled = state;
    }
    protected override void Awake()
    {
        base.Awake();
        _dialogueCamera = GetComponentInChildren<Camera>(); 
    }
}
