using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    public Button yourButton;
   // public string type;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void GenerateCharacters(string unitType)
    {
        //falta hacer que sea una posicion que tu señales, de momento he puesto que lo pusiese 
        //en una posición fija para testear

        //esto a lo mejor está hecho muy cutre pero de momento funciona así que
        GameObject unitPrefab = GameObject.Find("CharacterPrefab");
        Vector3 v = new Vector3(10, 10, 0);
        GameObject characterUnit = Instantiate(unitPrefab, v, Quaternion.identity);
        characterUnit.GetComponent<CharacterClass>().type = unitType;
        characterUnit.GetComponent<CharacterClass>().SetStats();
        characterUnit.transform.SetParent(GameObject.Find("Units").transform, false);



    }
}



        