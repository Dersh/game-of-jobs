using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<DialogueUI>();
            return _instance;
        }
    }
    private static DialogueUI _instance;
    public float TimeForSpeech;
    [SerializeField]
    private Text _tip;
    public void ShowTip(bool show)
    {
        _tip.enabled = show;
    }
    [SerializeField]
    private Font font;
    [SerializeField]
    private Canvas _menuCanvas;
    public void HideAll()
    {
        foreach (Graphic elem in _menuCanvas.GetComponentsInChildren<Graphic>())
            elem.enabled = false;

    }
    [SerializeField]
    private Text _answer;
    private void ShowAnswer()
    {
        _answer.enabled = true;
        _suggestionsList.enabled = false;
    }
    [SerializeField]
    private GridLayoutGroup _suggestionsList;
    private Text[] _suggestionsTexts;
    private const float AnswerMargin = 200;
    private const float CellHeight = 75;
    public Action onNoSuggestions;
    public Action onNoAnswer;
    public Action onAnimationStart;
    public Action onAnimationEnd;
    private void ShowSuggestions()
    {
        _answer.enabled = false;
        _suggestionsList.enabled = true;
    }

    private Coroutine _dialogueIteration;
    public void IterateDialogue(string name, string answer, string[] suggestions, DefaultDialogueGiver giver)
    {
        _dialogueIteration = StartCoroutine(IterateDialogueCoroutine(name, answer, suggestions, giver));
    }
    public void StopDialogue()
    {
        StopCoroutine(_dialogueIteration);
    }
    private IEnumerator IterateDialogueCoroutine(string name, string answer, string[] suggestions, DefaultDialogueGiver giver)
    {
        GameObject.Find("Viewport").GetComponent<Image>().enabled = true;
        foreach (Text t in _suggestionsTexts)
            Destroy(t.gameObject);

        ShowAnswer();
        _answer.text = $"[{name}]: {answer}";
        RectTransform rectAnswer = _answer.GetComponent<RectTransform>();
        if (answer.Length != 0)
            yield return new WaitForSeconds(TimeForSpeech);
        else
            onNoAnswer();

        _suggestionsTexts = new Text[suggestions.Length];
        _suggestionsList.GetComponent<RectTransform>().sizeDelta = new Vector2(_suggestionsList.GetComponent<RectTransform>().sizeDelta.x, 100 * _suggestionsTexts.Length);
        for (int i = 0; i < _suggestionsTexts.Length; i++)
        {
            int index = i;
            _suggestionsTexts[index] = new GameObject($"Line {index}").AddComponent<Text>();
            _suggestionsTexts[index].transform.parent = _suggestionsList.transform;
            _suggestionsTexts[index].transform.localScale = Vector3.one;
            _suggestionsTexts[index].text = $"- {suggestions[index]}";
            _suggestionsTexts[index].color = Color.white;
            _suggestionsTexts[index].font = font;
            _suggestionsTexts[index].fontSize = 48;
            _suggestionsTexts[index].gameObject.AddComponent<Button>().onClick.AddListener(
            () =>
            {
                giver.GiveAnswerSafe(index);
            });
        }
        if (suggestions.Length == 0)
            onNoSuggestions();
        /*float canvasWidth = _menuCanvas.GetComponent<RectTransform>().sizeDelta.x;
        _suggestionsList.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth - AnswerMargin * 2, _suggestionsTexts.Length * CellHeight);
        _suggestionsList.cellSize = new Vector2(canvasWidth - AnswerMargin * 2, CellHeight);*/
        ShowSuggestions();
    }

    private void Start()
    {
        onNoAnswer = () => {
            Debug.Log("No Answer");
        };
        _suggestionsTexts = _suggestionsList.GetComponentsInChildren<Text>();
        _tip.text = $"Нажмите {DefaultDialogueGiver._interactButton.ToString()}, чтобы начать диалог";
        HideAll();
    }
}
