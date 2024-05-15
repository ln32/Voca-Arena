using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WordDataLoader : MonoBehaviour
{
    public Global_Value gv;

    const string URL = "https://docs.google.com/spreadsheets/d/18qcOAfrYft0mdBkspmHHZ450gFL7coSgvmt09P76cxI/export?format=tsv";//&range=A:A   
    const string WebURL = "https://script.google.com/macros/s/AKfycbwN8vtm5YKgwl6oaPrOJUf5n-l94CA4TdghJqFu/exec";
    public UnityWebRequest www;

    public Transform wordUIAry;
    public string[] wordAry;
    public Text[] textAry;

    string stringData;

    void Start()
    {
        if (wordUIAry == null)
        {
            // WordBlockAry is null
            return;
        }

        textAry = wordUIAry.GetComponentsInChildren<Text>();
        GetData();
    }

    public void GetData()
    {
        StartCoroutine(Get());
    }

    IEnumerator Get()
    {
        UnityWebRequest www;
        www = UnityWebRequest.Get(URL);

        yield return www.SendWebRequest();
        stringData = www.downloadHandler.text;

        //split by \t \n
        {
            char[] Filter = { '\t', '\n' };
            wordAry = stringData.Split(Filter);
        }

        for (int i = 0; i < textAry.Length; i++)
        {
            textAry[i].text = wordAry[i];
        }
    }
}