using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyUI : MonoBehaviour
{
    public Text MoneyText;
    // Update is called once per frame
    void Update()
    {
        MoneyText.text = "$ " + PlayerStats.Money.ToString();
    }
}
