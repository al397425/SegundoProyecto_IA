using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDText : MonoBehaviour
{
    private string currentTurn;
    private int numberTurn;
    private UnitSelection uniSel;
    private GameObject turnObj;
    private GameObject unitInfo;

    // Start is called before the first frame update
    void Start()
    {
        uniSel = GameObject.Find("GameHandler").GetComponent<UnitSelection>();
        turnObj = GameObject.Find("CurrentTurnText");
        unitInfo = GameObject.Find("UnitInfoText");
        numberTurn = 0;
    }

    // Update is called once per frame
    void Update()
    {
       if (uniSel.playerTurn) { currentTurn = "Player"; }
       else currentTurn = "IA";

       numberTurn = uniSel.numberTurn;

        turnObj.GetComponent<Text>().text = "Current Turn (" + numberTurn + "): " + currentTurn;



        //
        if (unitInfo.activeSelf) { unitInfo.GetComponent<Text>().text = uniSel.UnitInfo(); }
        

    }
        
            

       
}
