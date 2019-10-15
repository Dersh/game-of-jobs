using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Networking;
public class Quest : MonoBehaviour
{
    [FormerlySerializedAs("guid")] [SerializeField] private string _guid;
    private void InitializeGuid()
    {
#if UNITY_EDITOR
        _guid = "1531c91a-7c30-4e27-8fa4-6f62b639d773";
#else
        _guid = WebUtils.ParseUrl(Application.absoluteURL, "guid");
#endif
    }
    [FormerlySerializedAs("time")] [SerializeField] private int _time;
    private void InitializeTime()
    {
        _time = (int)Time.time;
    }
    [FormerlySerializedAs("score")] [SerializeField] private int _score;
    private void InitializeScore()
    {
        _score = Score.Value;
    }
    [FormerlySerializedAs("xyz")] [SerializeField] private string _xyz;
    private void InitializeXYZ()
    {
        Vector3 pos = transform.position;
        _xyz = $"{ (int)pos.x },{ (int)pos.y },{ (int)pos.z }";
    }
    public virtual string ToJson()
    {
        InitializeGuid();
        InitializeTime();
        InitializeScore();
        InitializeXYZ();
        return JsonUtility.ToJson(this);
    }
}