using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Company : MonoBehaviour
{
    public string companyName;
    public float stockVal;
    public int numOfStock;
    private TextMeshProUGUI[] tmproArr;

    public void Awake()
    {
        tmproArr = GetComponentsInChildren<TextMeshProUGUI>();
        tmproArr[0].text = companyName + " |";
        tmproArr[1].text = "$" + stockVal.ToString() + " |";
        tmproArr[3].text = numOfStock.ToString() + "# stocks";
    }
}
