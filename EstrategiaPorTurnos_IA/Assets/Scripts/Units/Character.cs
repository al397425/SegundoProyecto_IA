using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    GameObject unitPrefab;
    // Start is called before the first frame update
    void Start()
    {
        unitPrefab = GameObject.Find("CharacterPrefab");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Character(int x, int y, string unit)
    {
        Vector3 v = new Vector3(x, y, 0);
        GameObject characterUnit = Instantiate(unitPrefab,v,Quaternion.identity);
        characterUnit.GetComponent<CharacterClass>().type = unit;

        // characterUnit.GetComponent<Image>().sprite = 
       // return characterUnit;
    }
}
