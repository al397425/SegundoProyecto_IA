using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelection : MonoBehaviour
{
    public bool playerTurn;
    public int numberTurn;

    private GameObject pastUnit;
    private GameObject currentUnit;
    private GameObject attackButton;
    //private GameObject passButton;
    private GameObject textStats;

    private GameObject currentUnitInfo;
    private bool infoShows;

    public Material archerTex;
    public Material tankTex;
    public Material infantryTex;
    public Material aerialTex;


    // Start is called before the first frame update
    void Start()
    {
 
        pastUnit = null;
        attackButton = GameObject.Find("AttackButton");
        textStats = GameObject.Find("UnitInfoText");
        currentUnitInfo = GameObject.Find("UnitInfoText");
        currentUnitInfo.SetActive(false);
        attackButton.SetActive(false);
        infoShows = false;
        playerTurn = true;
        numberTurn = 0;

      //Button btnpass = passButton.GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Material GetMaterialUnit(string t)
    {
        if (t == "archer")
        {
           return archerTex;
        }
        else if (t == "infantry")
        {
            return infantryTex;
        }
        else if (t == "tank")
        {
            return tankTex;

        }
        else if (t == "aerial")
        {
            return aerialTex;
        }
        return null;
    }

    public void activateUnit(GameObject go)
    {
        if (pastUnit != null) //no es la primera vez en activarse una unidad durante la partida
        {
            if(currentUnit.GetComponent<CharacterClass>().team == go.GetComponent<CharacterClass>().team) //del mismo jugador, cambia el personaje activo
            {
                pastUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = false;
                pastUnit = currentUnit;
                pastUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = false;
                currentUnit = go;
                currentUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = true;
                currentUnit.GetComponent<CharacterPathfindingMovementHandler>().SetTargetPosition(currentUnit.transform.position);


            }
            else  //no son del mismo equipo 
            {
                Debug.Log("Son de equipos distintos, pueden luchar");
                attackButton.SetActive(true);
                
            }
            
        }
        else if(pastUnit==null) //primera unidad que comienza en el turno (al final del turno resetea)
        {
            pastUnit = go;
            currentUnit = go;
            currentUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = true;
            currentUnit.GetComponent<CharacterPathfindingMovementHandler>().SetTargetPosition(currentUnit.transform.position);
        }
        


    }

    public void attackUnit()
    {
        if(currentUnit!=null && pastUnit!=null)
        {
            Debug.Log("Se puede atacar");
            pastUnit.GetComponent<CharacterClass>().AttackUnit(currentUnit);
        }
        attackButton.SetActive(false);
    }

    public void changeTurn()
    {
        //Debug.Log("changeturn button works");
        playerTurn = !playerTurn;
        pastUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = false;
        pastUnit = null;
        currentUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = false;
        currentUnit = null;
        numberTurn = +1;


        //

        if (playerTurn) { Debug.Log("Cambiado el turno, ahora es turno del jugador 1"); }
        else { Debug.Log("Cambiado el turno, ahora es turno del jugador 2 (IA)"); }
        
    }

    public string UnitInfo()
    {
        string info = "No unit selected";

        if (currentUnit != null)
        {
           info = "Unit Type: " + currentUnit.GetComponent<CharacterClass>().type +
          "\nHP: " + currentUnit.GetComponent<CharacterClass>().health +
          "\nAttack: " + currentUnit.GetComponent<CharacterClass>().attack +
          "\nMovement range: " + currentUnit.GetComponent<CharacterClass>().movement +
          "\nAttack range: " + currentUnit.GetComponent<CharacterClass>().attackRange;
        }
      

        return info;

    }

    public void ShowUnitInfo()
    {
        currentUnitInfo.SetActive(!infoShows); 

    }

}
