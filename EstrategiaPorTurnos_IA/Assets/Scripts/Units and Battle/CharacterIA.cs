using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIA : ICharacter
{
    CreateCharacter createCh;
    CharacterClass chClass;
    UnitSelection unitSel;

    // Start is called before the first frame update
    void Start()
    {
        /*
        createCh = ;
        chClass = ;
        unitSel = ;*/

        

    }

    // Update is called once per frame
    void Update()
    {

    }
    public string PickUnitClass()
    {
        string bestType = "";


        return bestType;
    }

    public void CreateUnits()
    {
        //en funcion de lo que hay en el campo y el coste y recursos de los que dispone decide qué hacer
        string unitType = PickUnitClass();
        if (unitType != "") { createCh.GenerateCharacters(unitType); }
        else { Debug.Log("No se reclutaran unidades este turno"); }
    }
    public void AttackUnit(GameObject go)
    {
        
    }

    public void MoveUnit(int x, int y)
    {
        GameObject go = PickUnit();

        //prueba si puede atacar y si le conviene
        AttackUnit(go);

        //si aún puede mira a dónde moverse

    }

    private GameObject PickUnit()
    {
        //mira todas las unidades disponibles y elige cuál es la más óptima
        GameObject bestPick = null;

        return bestPick;

    }
}
