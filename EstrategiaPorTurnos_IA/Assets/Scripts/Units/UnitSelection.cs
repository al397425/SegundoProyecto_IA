using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelection : MonoBehaviour
{
    private GameObject pastUnit;
    private GameObject currentUnit;
    private GameObject attackButton;

    // Start is called before the first frame update
    void Start()
    {
 
        pastUnit = null;
        attackButton = GameObject.Find("AttackButton");
        attackButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateUnit(GameObject go)
    {
        if (pastUnit != null) //no es la primera vez en activarse una unidad durante la partida
        {
            pastUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = false;
            pastUnit = currentUnit;
            
        }
        else{
            pastUnit = go;
        }
        currentUnit = go;
        currentUnit.GetComponent<CharacterPathfindingMovementHandler>().enabled = true;
        currentUnit.GetComponent<CharacterPathfindingMovementHandler>().SetTargetPosition(currentUnit.transform.position);
        attackButton.SetActive(true);

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
}
