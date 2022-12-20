using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelection : MonoBehaviour
{
    public bool playerTurn;
    public int numberTurn;
    public GameObject[] playerArmy;

    private GameObject pastUnit;
    private GameObject currentUnit;
    private GameObject attackButton;
    private GameObject rivalUnit;
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
        currentUnitInfo = GameObject.Find("UnitInfoTextBox");
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
            if (currentUnit.GetComponent<CharacterClass>().team == go.GetComponent<CharacterClass>().team) //del mismo jugador, cambia el personaje activo
            {
                Debug.Log("No son mismo equipo, cambio personaje activo");
                pastUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = false;
                pastUnit = currentUnit;
                pastUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = false;
                currentUnit = go;
                currentUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = true;
                //currentUnit.GetComponent<CharacterPathfindingMovementHandler>().SetTargetPosition(currentUnit.transform.position);    //Comentado porque creo que no hacen falta


            }
            else  //no son del mismo equipo 
            {
                Debug.Log("Son de equipos distintos, pueden luchar");
                rivalUnit = go;
                attackButton.SetActive(true);

            }

        }
        else if (pastUnit == null) //primera unidad que comienza en el turno (al final del turno resetea)
        {
            Debug.Log("Primera unidad activa");
            pastUnit = go;
            currentUnit = go;
            currentUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = true;
            //currentUnit.GetComponent<CharacterPathfindingMovementHandler>().SetTargetPosition(currentUnit.transform.position);    //Comentado porque creo que no hacen falta

        }



        else { Debug.Log("No es tu unidad"); }




    }
    public void moveUnitIA(GameObject go)
    {
        CharacterPathfindingMovementHandler cm = go.GetComponent<CharacterPathfindingMovementHandler>();
        int n = Random.Range(0, playerArmy.Length);
        bool attacked = false;
        Debug.Log("Set " + go.name + " target position to: " + playerArmy[n].transform.position);
        attacked = cm.CheckAttack(playerArmy);
        cm.SetTargetPosition(playerArmy[n].transform.position);
        if (!attacked)
        {
            Debug.Log("Not attacked");
            cm.CheckAttack(playerArmy);
        }
    }

    public void attackUnit()
    {
        if (currentUnit != null && rivalUnit != null)
        {
            Debug.Log("Se puede atacar");
            currentUnit.GetComponent<CharacterClass>().AttackUnit(rivalUnit);
            rivalUnit = null;
        }
        attackButton.SetActive(false);
    }

    public void changeTurn()
    {
        //Debug.Log("changeturn button works");
        playerTurn = !playerTurn;
        // pastUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = false;
        pastUnit = null;
        //currentUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = false;
        currentUnit = null;
        numberTurn++;


        Debug.Log("CAMBIO TURNO, TURNO DE JUGADOR ES " + playerTurn);

    }

    public string UnitInfo()
    {
        string info = "No unit selected";

        if (currentUnit != null)
        {
            info = "Unit Type: " + currentUnit.GetComponent<CharacterClass>().GetTypeUnit() +
           "\nHP: " + currentUnit.GetComponent<CharacterClass>().GetHealth() +
           "\nAttack: " + currentUnit.GetComponent<CharacterClass>().GetAttack() +
           "\nMovement range: " + currentUnit.GetComponent<CharacterClass>().GetMovement() +
           "\nAttack range: " + currentUnit.GetComponent<CharacterClass>().GetRange();
        }


        return info;

    }

    public void ShowUnitInfo()
    {
        currentUnitInfo.SetActive(!infoShows);

    }

}
