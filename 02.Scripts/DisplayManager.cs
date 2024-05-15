using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayManager : MonoBehaviour
{
    public Global_Value gv;

    public List<GameObject> DisplayList;
    // 0 - main, 1 - wordList, 2 - matchUI
    public int Index = 0;

    public void switchUI(int new_index)
    {
        if (new_index >= DisplayList.Count)
            return;

        switch (new_index)
        {
            case 2:
                matchUIActivate(setCodeRandom());
                break;

            default:
                break;
        }

        DisplayList[Index].SetActive(false);
        DisplayList[new_index].SetActive(true);
        Index = new_index;
    }

    MatchActivate matchUI;
    void matchUIActivate(string code)
    {
        gv.matchActivate.matchActivate(code);
    }

    string setCodeRandom()
    {
        System.Random rand = new System.Random();
        string temp = "";
        int index = rand.Next(1,4);
        int main = rand.Next(1, 100);

        temp += "0/";
        temp += main;
        temp += "/";

        for (int i = 1; i < 5; i++)
        {
            if (index == i)
                temp += main;
            else
                temp += rand.Next(1, 100);

            if(i!=4)
                temp += "/";
        }

        return temp;
    }

    public void wrongEffect()
    {
        DisplayList[4].SetActive(true);
    }

    public void wrongEffectOPP()
    {
        DisplayList[5].SetActive(true);
    }

    public void correctEffectOPP()
    {
        DisplayList[6].SetActive(true);
    }

    public void searchMatch()
    {
        DisplayList[Index].SetActive(false);
        gv.logDataManager.searchMatch();
    }

    public void activateMatchResponse(string msg)
    {
        string[] temp = msg.Split(',');
        Debug.Log(temp[0]);
        Debug.Log(temp[1]);
        
        DisplayList[2].SetActive(true);
        Index = 2;
        matchUIActivate(temp[0]);
        gv.matchActivate.setDownloadLogData(temp[1].Split('-', '/'));
    }
}
