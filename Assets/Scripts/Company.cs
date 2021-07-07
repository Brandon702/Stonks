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
    public float playerStockVal;
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
        Debug.Log("Buy Pressed " + "Value: " + tmproArr[1].text + tmproArr[3].text);
        if ((int.Parse(inputField.text) * stockVal) < gameController.cash)
        {
            for(int i = 0; i > numOfStock; i++)
            {
                if((i * stockVal) > gameController.cash)
                {
                    break;
                    savedInt = i - 1;
                    inputField.text = savedInt.ToString();
                }
            }
        }
        if (!(numOfStock >= int.Parse(inputField.text)))
        {
            inputField.text = numOfStock.ToString();
        }
        float totalValue = int.Parse(inputField.text) * stockVal;
        gameController.cash -= totalValue;
        playerStockNum += int.Parse(inputField.text);
        numOfStock -= int.Parse(inputField.text);
        stockVal = Mathf.Round(stockVal * 100f) / 100f;
        gameController.cash = Mathf.Round(gameController.cash * 100f) / 100f;
        UpdateValues();
        gameController.updateGameBar();
        inputField.text = "0";
    }

    public void Sell()
    {
        Debug.Log("Sell Pressed " + "Value: " + tmproArr[1].text + tmproArr[3].text);
        if (!(playerStockNum >= int.Parse(inputField.text)))
        {
            inputField.text = playerStockNum.ToString();
        }

        if(playerStockNum != 0)
        {
            numOfStock += int.Parse(inputField.text);
            playerStockNum -= int.Parse(inputField.text);
        }
        float totalValue = int.Parse(inputField.text) * stockVal;
        gameController.cash += totalValue;
        stockVal = Mathf.Round(stockVal * 100f) / 100f;
        UpdateValues();
        gameController.updateGameBar();
        inputField.text = "0";
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
        tmproArr[0].text = companyName + "";
        tmproArr[1].text = "Stock Value $: " + stockVal.ToString() + "";
        tmproArr[2].text = "Player Stock #: " + playerStockNum.ToString() + "";
        tmproArr[3].text = numOfStock.ToString() + "# stocks";
    }
}
