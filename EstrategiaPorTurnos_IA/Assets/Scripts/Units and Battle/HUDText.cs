using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDText : MonoBehaviour
{
    private string currentTurn;
    private int numberTurn;
    private UnitSelection uniSel;
    // Start is called before the first frame update
    void Start()
    {
        uniSel = GameObject.Find("GameHandler").GetComponent<UnitSelection>();
        numberTurn = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        if (uniSel.playerTurn) { currentTurn = "Player"; }
        else currentTurn = "IA";

        numberTurn = uniSel.numberTurn;

        GetComponent<Text>().text = "Current Turn ("+ numberTurn + "): "+ currentTurn;
    }
}
