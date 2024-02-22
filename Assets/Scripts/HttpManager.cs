using System;
using System.Collections.Generic;
using UnityEngine;
using HttpRequest;
using ClientServer;
using ClientServer.WWWResponse;

public enum ePacketType
{    
    POST,
    GET,
    PUT,
    DELETE,
}

public class HttpManager : MonoBehaviour
{
    public string basePath = "https://65d6faa927d9a3bc1d79cece.mockapi.io/v1/buttons";

   public void SendPacket<T>(ePacketType packetType, Action<T> action, string id = null)
{
    switch (packetType)
    {
        case ePacketType.GET:
            GET(action, id);
            break;
        case ePacketType.POST:
            POST(action);
            break;
        case ePacketType.PUT:
            PUT(action, id);
            break;
        case ePacketType.DELETE:
            DELETE(action, id);
            break;
    }
}

private void GET<T>(Action<T> action, string id = null)
{
    string requestURL = basePath;
    if (!string.IsNullOrEmpty(id))
    {
        requestURL += "/" + id;
    }

    // Создание GET-запроса для получения данных с сервера
    var req = SimpleHttpClient.Get(requestURL)
        .OnSuccess(res =>
        {
            string newJson = "{ \"Items\": " + res.Text + "}";
            Debug.Log("res.Text " + newJson);
            T data = JsonUtility.FromJson<T>(newJson);
            action.Invoke(data);
        })
        .OnError(err => Debug.LogWarning(err.Error))
        .OnNetworkError(netErr => Debug.LogError(netErr.Error))
        .Send();
}

private void POST<T>(Action<T> action)
{
    string requestURL = basePath + "/post";

    // Создание POST-запроса для создания нового ресурса на сервере
    var req = SimpleHttpClient.PostJson(requestURL, "")
        .OnSuccess(res =>
        {
            T data = JsonUtility.FromJson<T>(res.Text);
            action.Invoke(data);
        })
        .OnError(err => Debug.LogWarning(err.Error))
        .OnNetworkError(netErr => Debug.LogError(netErr.Error))
        .Send();
}

private void PUT<T>(Action<T> action, string id)
{
    string requestURL = basePath + "/put";
    if (!string.IsNullOrEmpty(id))
    {
        requestURL += "/" + id;
    }

    // Создание PUT-запроса для обновления существующего ресурса на сервере
    var req = SimpleHttpClient.Put(requestURL, "")
        .OnSuccess(res =>
        {
            T data = JsonUtility.FromJson<T>(res.Text);
            action.Invoke(data);
        })
        .OnError(err => Debug.LogWarning(err.Error))
        .OnNetworkError(netErr => Debug.LogError(netErr.Error))
        .Send();
}

private void DELETE<T>(Action<T> action, string id)
{
    string requestURL = basePath + "/delete";
    if (!string.IsNullOrEmpty(id))
    {
        requestURL += "/" + id;
    }

    // Создание DELETE-запроса для удаления ресурса на сервере
    var req = SimpleHttpClient.Delete(requestURL)
        .OnSuccess(res =>
        {
            T data = JsonUtility.FromJson<T>(res.Text);
            action.Invoke(data);
        })
        .OnError(err => Debug.LogWarning(err.Error))
        .OnNetworkError(netErr => Debug.LogError(netErr.Error))
        .Send();
}
}
