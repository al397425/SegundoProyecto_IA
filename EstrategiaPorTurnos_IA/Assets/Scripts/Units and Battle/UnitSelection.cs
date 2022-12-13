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

    // Start is called before the first frame update
    void Start()
    {
 
        pastUnit = null;
        attackButton = GameObject.Find("AttackButton");
        attackButton.SetActive(false);
        playerTurn = true;
        numberTurn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(currentUnit!=null && pastUnit!=null && currentUnit != pastUnit)
        {
            Debug.Log("Se puede atacar");
            pastUnit.GetComponent<CharacterClass>().AttackUnit(currentUnit);
        }
        attackButton.SetActive(false);
    }

    public void changeTurn()
    {
        playerTurn = !playerTurn;
        pastUnit = null;
        currentUnit = null;
        numberTurn = +1;


        //

        if (playerTurn) { Debug.Log("Cambiado el turno, ahora es turno del jugador 1"); }
        else { Debug.Log("Cambiado el turno, ahora es turno del jugador 2 (IA)"); }
        
    }
}
