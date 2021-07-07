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

        cashNeeded = cashNeededStatic;
    }
    #endregion



    [Header("Editable values")]
    public eState state = eState.TITLE;
    public float cash = 1000.00f;

    //Dont touch these variables:
    bool forceOnce = true;
    public float investedCash = 0f;
    private TextMeshProUGUI[] tmpVals;
    public static float cashNeededStatic = 1000000f;
    public float cashNeeded;
    public int day = 1;
    private float timeElapsed = 0.0f;
    private float timeLength = 5.0f;
    private Company[] companies;

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
            if (forceOnce == true)
            {
                GameSession();
                forceOnce = false;
            }
            timeElapsed += Time.fixedDeltaTime;
            //Debug.Log(timeElapsed);
            updateGameBar();
            if (timeElapsed >= timeLength)
            {
                Debug.Log("Day Pased");
                foreach (Company company in companies)
                {
                    company.stockVal += 0.5f + UnityEngine.Random.Range((-(company.stockVal) * 0.1f), (company.stockVal * 0.15f));
                    company.stockVal = Mathf.Round(company.stockVal * 100f) /100f;
                    company.UpdateValues();
                }
                day++;
                updateGameBar();
                timeElapsed = 0.0f;
            }
        }
    }
    public void GameSession()
    {
        //Run once on game start things go here
        tmpVals = GameObject.Find("MainObjects").GetComponentsInChildren<TextMeshProUGUI>();
        companies = GameObject.Find("Companies").GetComponentsInChildren<Company>();
    }

    public void updateGameBar()
    {
        cashNeeded = cashNeededStatic - (cash + investedCash);
        tmpVals[0].text = "Cash: \n$" + cash.ToString();
        tmpVals[1].text = "Invested Cash: \n$" + investedCash.ToString();
        tmpVals[2].text = "Gross Value: \n$" + (investedCash + cash).ToString();
        tmpVals[3].text = "Cash Needed: \n$" + cashNeeded;
        tmpVals[4].text = "Day " + day;
    }
}


