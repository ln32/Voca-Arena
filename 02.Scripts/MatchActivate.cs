using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchActivate : MonoBehaviour
{
    public Global_Value gv;
    public List<Text> textUI_case0;

    string serialCode;
    public void matchActivate(string _serialCode)
    {
        serialCode = _serialCode;
        string[] codeDataAry = _serialCode.Split('/');

        if (codeDataAry.Length == 0)
        {            
            // Input Code is broken 
            return;
        }

        if (codeDataAry[0] == "0")
            matchCase0(codeDataAry);
    }

    float matchStartTime;
    public string Answercase0;
    void matchCase0(string[] codeDataAry)
    {
        logData = "";
        matchStartTime = gv.gameTime;
        if (codeDataAry.Length != 6)
        {
            // MatchCase is broken 
            return;
        }


        if (true)
        {
            int i = 1;
            string targetWord = gv.wordDataLoader.wordAry[int.Parse(codeDataAry[i]) * 2];

            textUI_case0[0].text = targetWord;
            textUI_case0[5].text = targetWord;
        }

        for (int i = 2; i < 6; i++)
        {
            string targetWord = gv.wordDataLoader.wordAry[int.Parse(codeDataAry[i]) * 2 + 1];

            textUI_case0[i - 1].text = targetWord;
            textUI_case0[i - 1 + 5].text = targetWord;
        }

        for (int i = 2; i < 6; i++)
        {
            if (codeDataAry[1] == codeDataAry[i])
            {
                Answercase0 = (i - 1) + "";
                break;
            }
        }

    }

    public string logData;
    public void TouchCase0(int index)
    {
        if (index + "" == Answercase0)
        {
            logData += index + "-" + (Mathf.Round((gv.gameTime - matchStartTime) * 10) / 10);
            gv.logDataManager.uploadLogData(serialCode,logData);
            gv.displayManager.switchUI(3);
        }
        else
        {
            logData += index + "-" + (Mathf.Round((gv.gameTime - matchStartTime) * 10) / 10) + "/";
            gv.displayManager.wrongEffect();
        }
    }

    [SerializeField]
    List<string> downloadLogData = null;
    public void setDownloadLogData(string[] data)
    {
        if (downloadLogData != null)
            downloadLogData.Clear();

        

        for (int i = 0; i < data.Length; i++)
        {
            downloadLogData.Add(data[i]);
        }
    }

    private void Update()
    {
        if (downloadLogData.Count != 0)
        {
            if (float.Parse(downloadLogData[1]) < gv.gameTime - matchStartTime)
            {

                if (downloadLogData.Count <= 2)
                {
                    gv.displayManager.correctEffectOPP();
                    downloadLogData.RemoveAt(0);
                    downloadLogData.RemoveAt(0);
                }
                else
                {
                    gv.displayManager.wrongEffectOPP();
                    downloadLogData.RemoveAt(0);
                    downloadLogData.RemoveAt(0);
                }
            }
        }
    }
}
