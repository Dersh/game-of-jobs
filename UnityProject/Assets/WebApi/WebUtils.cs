using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public static class WebUtils
{
    private const string Url = "http://api.game-of-jobs.solution-now.ru/api";

    public static void SendQuestEnd(string body)
    {
        SendRequest("POST", "/quest/end", body);
    }
    public static void SendQuestEnd(string body, Action<AsyncOperation> onCompleted)
    {
        SendRequest("POST", "/quest/end", body, onCompleted);
    }

    public static void SendQuestLog(string body)
    {
        SendRequest("POST", "/quest/log", body);
    }
    public static void SendQuestLog(string body, Action<AsyncOperation> onCompleted)
    {
        SendRequest("POST", "/quest/log", body, onCompleted);
    }

    public static string ParseUrl(string url, string name)
    {
        string[] args = url.Split('?')[1].Split('&');
        foreach (string arg in args)
        {
            string[] data = arg.Split('=');
            if (data[0] == name)
                return data[1];
        }
        return "";
    }

    private static void SendRequest(string method, string path, string body)
    {
        SendRequest(method, path, body, (op) => { });
    }
    private static void SendRequest(string method, string path, string body, Action<AsyncOperation> onCompleted)
    {
        UnityWebRequest request = new UnityWebRequest(Url + path, method);
        {
            request.method = method;
            UploadHandler handler = new UploadHandlerRaw(System.Text.Encoding.ASCII.GetBytes(body));
            handler.contentType = "application/json";
            request.uploadHandler = handler;
            UnityWebRequestAsyncOperation asyncOperation = request.SendWebRequest();
            asyncOperation.completed += onCompleted;
            asyncOperation.completed += (s) => {
                request.Dispose();
            };
        }
    }

}
