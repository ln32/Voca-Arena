using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class GoogleData
{
    public string result, msg, func;
}

public class LogDataManager : MonoBehaviour
{
    public Global_Value gv;
    const string WebURL = "https://script.google.com/macros/s/AKfycbwN8vtm5YKgwl6oaPrOJUf5n-l94CA4TdghJqFu/exec";
    public UnityWebRequest www;

    public void searchMatch()
    {
        WWWForm form = new WWWForm();
        form.AddField("func", "searchMatch");

        StartCoroutine(Post(form));
    }

    public void uploadLogData(string serialCode, string logData)
    {
        WWWForm form = new WWWForm();
        form.AddField("func", "uploadLogData");
        form.AddField("serialCode", serialCode);
        form.AddField("content", logData);

        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)  
    {
        using (UnityWebRequest www = UnityWebRequest.Post(WebURL, form)) // 반드시 using을 써야한다
        {
            // postStart
            yield return www.SendWebRequest();
            if (www.isDone) Response(www.downloadHandler.text);
        }
    }

    public GoogleData GD;
    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        GD = JsonUtility.FromJson<GoogleData>(json);
        // func activated
        if (GD.func == "searchMatch")
            gv.displayManager.activateMatchResponse(GD.msg);
    }
}
