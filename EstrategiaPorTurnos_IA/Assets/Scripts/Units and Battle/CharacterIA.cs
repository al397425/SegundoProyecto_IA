using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIA : ICharacter
{
    CreateCharacter createCh;
    CharacterClass chClass;
    UnitSelection unitSel;
    int ActionsNumber;

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
        //He pensado para que la IA pase el turno que si tiene por ejemplo 3 unidades =  3 ActionsNumber y cada vez que mueva
        // una unidad se reste las acciones de ese turno y luego se llame a changeTurn
    }
    public string PickUnitClass()
    {
        string bestType = "";


        return bestType;
    }

    public void CreateUnits()
    {
        //en funcion de lo que hay en el campo y el coste y recursos de los que dispone decide qu� hacer
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

        //si a�n puede mira a d�nde moverse

    }

    private GameObject PickUnit()
    {
        //mira todas las unidades disponibles y elige cu�l es la m�s �ptima
        GameObject bestPick = null;

        return bestPick;

    }
}
