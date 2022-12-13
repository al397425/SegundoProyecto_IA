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
        GameObject unitPrefab = GameObject.Find("CharacterPrefab");
        Vector3 v = new Vector3(10, 10, 0);
        GameObject characterUnit = Instantiate(unitPrefab, v, Quaternion.identity);
        characterUnit.GetComponent<CharacterClass>().type = unitType;
        characterUnit.GetComponent<CharacterClass>().SetStats();
        characterUnit.transform.SetParent(GameObject.Find("Units").transform, false);

       // return characterUnit;


    }
}



        