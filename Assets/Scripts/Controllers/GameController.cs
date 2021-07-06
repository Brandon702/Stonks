using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public enum eState
{
    TITLE,
    GAME,
    PAUSE,
    GAMEOVER,
    INSTRUCTIONS,
    MENU,
    EXITGAME
}

public class GameController : MonoBehaviour
{

    #region Singleton
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {

        }
    }
    #endregion



    [Header("Editable values")]
    public eState state = eState.TITLE;
    public float cash = 1000.00f;

    //Dont touch these variables:
    bool forceOnce = true;
    private float investedCash = 0f;
    private TextMeshProUGUI[] tmpVals;
    private float cashNeeded = 1000000;
    private int day = 1;
    private float timeElapsed = 0.0f;
    private float timeLength = 45.0f;
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        tmpVals = GameObject.Find("MainObjects").GetComponentsInChildren<TextMeshProUGUI>();
        updateGameBar();
    }

    void Update()
    {

        if (state == eState.MENU)
        {
            //turnDisplay.SetActive(false);

            forceOnce = true;
        }

        //Game is running
        if (state == eState.GAME)
        {
            timeElapsed += Time.fixedDeltaTime;
            //Debug.Log(timeElapsed);
            if (forceOnce == true)
            {
                GameSession();

                forceOnce = false;
            }

            if (timeElapsed > timeLength)
            {
                Debug.Log("Day Pased");
                day++;
                updateGameBar();
                timeElapsed = 0.0f;
            }
        }
    }
    public void GameSession()
    {
        //Run once on game start things go here
    }

    public void updateGameBar()
    {
        cashNeeded = 1000000.00f - (cash + investedCash);
        tmpVals[0].text = "Cash: " + cash.ToString();
        tmpVals[1].text = "Invested Cash: " + investedCash.ToString();
        tmpVals[2].text = "Cash Needed: " + cashNeeded;
        tmpVals[3].text = "Day " + day;
    }
}


