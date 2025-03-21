using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System;

public class APIRequest : MonoBehaviour
{
    private void Awake()
    {
        BagEvents.SubscribeOn(BagEvents.EventType.putInBag, PutInRequest);
        BagEvents.SubscribeOn(BagEvents.EventType.removeFromBag, RemoveRequest);
    }

    private string apiUrl = "https://wadahub.manerai.com/api/inventory/status";
    private string authToken = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP"; 

    private void PutInRequest(int id)
    {
        SendPostRequest(id, "put_in");
    }

    private void RemoveRequest(int id)
    {
        SendPostRequest(id, "remove");
    }

    /// <summary>
    /// Turns parameters to JSON and creates request
    /// </summary>
    private void SendPostRequest(int id, string satus)
    {
        RequestedItemData itemData = new RequestedItemData
        {
            id = id,
            status = satus
        };

        string jsonData = JsonUtility.ToJson(itemData);
        StartCoroutine(PostRequest(jsonData));
    }

    /// <summary>
    /// Sending request on server
    /// </summary>
    private IEnumerator PostRequest(string jsonData)
    {
        
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        // Create request
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + authToken);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
    }
    private void OnDestroy()
    {
        BagEvents.Unsubscribe(BagEvents.EventType.putInBag, PutInRequest);
        BagEvents.Unsubscribe(BagEvents.EventType.removeFromBag, RemoveRequest);
    }

    [Serializable]
    private class ResponseData
    {
        public string response;
        public string status;
        public RequestedItemData data_submitted;
    }

    [Serializable]
    private class RequestedItemData
    {
        public int id;
        public string status;
    }
}
