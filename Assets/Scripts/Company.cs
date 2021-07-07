using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//You can purchase stock & go into the negative of cash, which is not allowed

public class Company : MonoBehaviour
{
    public string companyName;
    public float stockVal;
    public int numOfStock;
    public int playerStockNum;
    private TextMeshProUGUI[] tmproArr;
    private TMP_InputField inputField;
    private GameController gameController;
    private int savedInt;

    public void Awake()
    {
        UpdateValues();
        tmproArr = GetComponentsInChildren<TextMeshProUGUI>();
        inputField = GetComponentInChildren<TMP_InputField>();
        gameController = GameObject.Find("GameController").GetComponentInChildren<GameController>();
    }

    public void Buy()
    {
        nullCheck();
        int val = int.Parse(inputField.text);
        Debug.Log("Buy Pressed " + "Value: " + tmproArr[1].text + tmproArr[3].text);
        if ((val * stockVal) > gameController.cash)
        {
            for(int i = val; i < numOfStock; i--)
            {
                if((i * stockVal) <= gameController.cash)
                {
                    val = i;
                    break;
                }
            }
        }
        if (!(numOfStock >= val))
        {
            val = numOfStock;
        }
        float totalValue = val * stockVal;
        gameController.cash -= totalValue;
        playerStockNum += val;
        numOfStock -= val;
        stockVal = Mathf.Round(stockVal * 100f) / 100f;
        gameController.cash = Mathf.Round(gameController.cash * 100f) / 100f;
        UpdateValues();
        gameController.updateGameBar();
        inputField.text = "0";
    }

    public void Sell()
    {
        nullCheck();
        Debug.Log("Sell Pressed " + "Value: " + tmproArr[1].text + tmproArr[3].text);
        int val = int.Parse(inputField.text);
        if (!(playerStockNum >= val))
        {
            inputField.text = playerStockNum.ToString();
        }

        if(playerStockNum != 0)
        {
            numOfStock += val;
            playerStockNum -= val;
        }
        float totalValue = val * stockVal;
        gameController.cash += totalValue;
        stockVal = Mathf.Round(stockVal * 100f) / 100f;
        UpdateValues();
        gameController.updateGameBar();
        inputField.text = "0";
    }

    public void BuyAll()
    {
        nullCheck();
        int val = 0;
        Debug.Log("Buy Pressed " + "Value: " + tmproArr[1].text + tmproArr[3].text);

        for (int i = val; i < numOfStock; i++)
        {
            if ((i * stockVal) >= gameController.cash)
            {
                val = i-1;
                break;
            }
        }

        if (!(numOfStock >= val))
        {
            val = numOfStock;
        }
        float totalValue = val * stockVal;
        gameController.cash -= totalValue;
        playerStockNum += val;
        numOfStock -= val;
        stockVal = Mathf.Round(stockVal * 100f) / 100f;
        gameController.cash = Mathf.Round(gameController.cash * 100f) / 100f;
        UpdateValues();
        gameController.updateGameBar();
        inputField.text = "0";
    }

    public void SellAll()
    {
        nullCheck();
        Debug.Log("Sell Pressed " + "Value: " + tmproArr[1].text + tmproArr[3].text);
        int val = playerStockNum;
        if (playerStockNum != 0)
        {
            numOfStock += val;
            playerStockNum -= val;
        }
        float totalValue = val * stockVal;
        gameController.cash += totalValue;
        stockVal = Mathf.Round(stockVal * 100f) / 100f;
        UpdateValues();
        gameController.updateGameBar();
        inputField.text = "0";
    }

    public void nullCheck()
    {
        if(inputField.text == "")
        {
            inputField.text = "0";
        }
    }

    public void Increment()
    {
        if(int.Parse(inputField.text) < numOfStock + playerStockNum)
        {
            inputField.text = (int.Parse(inputField.text) + 1).ToString();
        }
        else
        {
            inputField.text = numOfStock.ToString();
        }
    }

    public void Decrement()
    {
        if(inputField.text != "0")
        {
            inputField.text = (int.Parse(inputField.text) - 1).ToString();
        }
        
    }

    public void UpdateValues()
    {
        tmproArr = GetComponentsInChildren<TextMeshProUGUI>();
        tmproArr[0].text = companyName;
        tmproArr[2].text = "Stock Value: \n$" + stockVal.ToString();
        tmproArr[4].text = "Player Stock #: \n" + playerStockNum.ToString();
        tmproArr[6].text = "Available Stocks: \n" + numOfStock.ToString();
    }
}
