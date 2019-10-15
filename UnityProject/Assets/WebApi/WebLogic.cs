using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class WebLogic : MonoBehaviour
{
    private Quest[] _quests;
    
    private void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }
    private void Update()
    {
        CheckSendLog();
    }
    private void OnActiveSceneChanged(Scene from, Scene to)
    {
        _quests = FindObjectsOfType<Quest>();
    }

    public void CloseGame()
    {
        SendEndAll((op) => {
            Application.Quit();
        });
    }
    [Header("Log")]
    public float threshold;
    private float _lastTime;
    public void CheckSendLog()
    {
        if (Time.time - _lastTime > threshold)
        {
            SendLogAll();
            _lastTime = Time.time;
        }
    }

    private void SendEndAll()
    {
        SendEndAll((op) => { });
    }
    private void SendEndAll(Action<AsyncOperation> onCompleted)
    {
        foreach (Quest quest in _quests)
            WebUtils.SendQuestEnd(quest.ToJson(), onCompleted);
    }
    private void SendLogAll()
    {
        SendLogAll((op) => { });
    }
    private void SendLogAll(Action<AsyncOperation> onCompleted)
    {
        foreach (Quest quest in _quests)
            WebUtils.SendQuestLog(quest.ToJson(), onCompleted);
    }
}
