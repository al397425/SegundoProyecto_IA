using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    private bool playerTurn;
    public Button yourButton;
   // public string type;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        playerTurn = GameObject.Find("GameHandler").GetComponent<UnitSelection>().playerTurn;

    }

    // Update is called once per frame
    void Update()
    {
        playerTurn = GameObject.Find("GameHandler").GetComponent<UnitSelection>().playerTurn;

    }




    public void GenerateCharacters(string unitType)
    {
        //falta hacer que sea una posicion que tu se�ales, de momento he puesto que lo pusiese 
        //en una posici�n fija para testear

        //esto a lo mejor est� hecho muy cutre pero de momento funciona as� que
        GameObject unitPrefab = GameObject.Find("CharacterPrefab");
        Vector3 v = new Vector3(10, 10, 0);
        GameObject characterUnit = Instantiate(unitPrefab, v, Quaternion.identity);
        characterUnit.GetComponent<CharacterClass>().type = unitType;

        if (playerTurn) { characterUnit.GetComponent<CharacterClass>().team = 1; }
        else { characterUnit.GetComponent<CharacterClass>().team = 2; }

        characterUnit.GetComponent<CharacterClass>().SetStats();
        characterUnit.transform.SetParent(GameObject.Find("Units").transform, false);



    }
}



        