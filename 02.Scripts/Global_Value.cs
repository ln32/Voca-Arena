using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Value : MonoBehaviour
{
    public float gameTime = 0;
    public WordDataLoader wordDataLoader;
    public MatchActivate matchActivate;
    public DisplayManager displayManager;
    public LogDataManager logDataManager;

    private void Start()
    {
        wordDataLoader.gv = this;
        matchActivate.gv = this;
        displayManager.gv = this;
        logDataManager.gv = this;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
    }
}
