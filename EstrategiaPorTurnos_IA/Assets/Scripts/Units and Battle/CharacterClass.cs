using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharacterClass:MonoBehaviour, ICharacter 
{
    private int movement; //n�mero casillas que se podr� desplazar
   private int cost;
    private int attackRange;
    private int health;
    private int maxHealth;
    private int attack;
    public string type; //tipo de unidad
    public bool wasMoved; //para saber si la unidad ya fue usada
    private int id;
    public int[] position = { 0, 0 };
    public int team;
    public UnitSelection unitSel;
    public Button attackButton;
    private MeshRenderer mr;
    private bool isActive;

    public void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        SetStats();
        isActive = false;
        

    }
    // Start is called before the first frame update

    //en verdad este constructor no se utiliza, luego lo borro
    public CharacterClass(string t)
    {
        type = t;


    }

    // Update is called once per frame
    public void Update()
    {
        //Esto ahora mismo no funciona

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //coge la posicion que has clicado
        BoxCollider2D currentUnit = this.GetComponent<BoxCollider2D>();

        if(Input.GetMouseButtonDown(0) && currentUnit.OverlapPoint(mousePos)) {

            StatsTell();
            if (team == 1)
            {
                unitSel.activateUnit(gameObject);
                isActive = true;
            }
           

            //�Habr�a que meterlos todos en una lista/recorrerlos para deshabilitarlos? Eso o en el game manager m�s f�cil

        }
     
    }

    public void SetStats()
    {
        if (this.type == "archer")
        {
            movement = 100;
            attackRange = 30;
            health = 1;
            attack = 2;
            type = "archer";
            cost = 1;
            
        }
        else if (this.type == "infantry")
        {
            movement = 100;
            attackRange = 10;
            health = 3;
            attack = 2;
            type = "infantry";
            cost = 1;
        }
        else if (this.type == "tank")
        {
            movement = 100;
            attackRange = 20;
            health = 4;
            attack = 3;
            type = "tank";
            cost = 3;


        }
        else if (this.type == "aerial")
        {
            movement = 100;
            attackRange = 20;
            health = 2;
            attack = 2;
            type = "aerial";
            cost = 3;

        }
        else
        {
            Debug.Log("No hay tipo, es "+ this.type);
        }

        mr.material = unitSel.GetMaterialUnit(type);


    }

    public string GetTypeUnit()
    {
        return type;
    }

    public int GetMovement()
    {
        return movement;
    }
    public int GetRange()
    {
        return attackRange;
    }
    public int GetHealth()
    {
        return health;
    }
    public int GetAttack()
    {
        return attack;
    }

    //La idea aqu� ha sido que haya un MoveUnit gen�rico que compruebe qu� clase es y ya llame
    //al especifico (en verdad esto es m�s donde ir�a lo de la IA y luego el moverlo en s�
    //ser�a maybe al final de los if o algo, y que la IA llame a esto pero el jugador sea
    //directamente solo comprobar si es posible con los movimientos y si lo es se mueve sin m�s

        //AL FINAL NO HA SIDO UTILIZADO POR EL PLANTEAMIENTO DEL PATHFINDING

    public void MoveUnit(int x, int y)
    {
        Debug.Log("Esta unidad se mueve " + this.movement + " de distancia");

        if (team != 1) //la IA (?) Podr�a haber sido un booleano, pues la verdad es que s�, luego
                           //si eso lo cambio
        {
            if (this.type == "archer")
            {
                MoveArcher(x, y);
            }
            else if (this.type == "infantry")
            {
                MoveInfantry(x, y);
            }
            else if (this.type == "tank")
            {
                MoveTank(x, y);

            }
            else if (this.type == "aerial")
            {
                MoveAerial(x, y);
            }
        }

        //if que compruebe si los movimientos de la unidad lo permiten


        //si lo permiten se mueve a la posicion (si eres el jugador la pos es donde has clicado,
        //si es la IA es la posici�n que calcule con la funci�n especifica)



        //si tiene en rango a una unidad enemiga muestra un bot�n de atacar, si le das llamas a AttackUnit


    }


    

    public void AttackUnit(GameObject rival) 
    {
       
        //comprueba si est� en el rango de ataque / si es voladora y t� melee etc


        //si lo est� se inicia el enfrentamiento entre las unidades
        int rivalHP = rival.GetComponent<CharacterClass>().health;
        int rivalATK = rival.GetComponent<CharacterClass>().attack;
        string rivalUnit = rival.GetComponent<CharacterClass>().type; //esto por si luego se nos va la olla y metemos velocidad/prioridad de ataques

        Debug.Log("La unidad " + type + " ataca a " + rivalUnit);

        rivalHP = rivalHP - attack;

        Debug.Log("El rival ha sufrido " + attack + "puntos de da�o, tiene " + rivalHP + " de vida");

        rival.GetComponent<CharacterClass>().setHealth(rivalHP);

        if (rivalHP <= 0)
        {

            //destruyes al rival
            rival.SetActive(false);
            Debug.Log("La unidad enemiga ha sido derrotada");
        }


    }
    public void setHealth(int currentHealth)
    {
        health = currentHealth;
    }

    public void MoveArcher(int x, int y)
    {

    }

    public void MoveInfantry(int x, int y)
    {

    }

    public void MoveTank(int x, int y)
    {

    }

    public void MoveAerial(int x, int y)
    {

    }


    //Puede que esto lo use, puede que se junte con lo del Move al final, no lo s� a�n
    
    public void InfantryIA()
    {
        //Simplemente comprueba enemigos cercanos y le da prioridad a si ve alguno que podría
        //estar en su rango de ataque. Si es una unidad voladora la ignora ya que no le puede alcanzar.
        //¿Si tiene un tanque y ve que no llegaría a atacarlo intenta evitar escoger su dirección? Para 
        //el futuro a lo mejor que considerase la vida que le queda al enemigo/si sus compañeros podrían ayudar

        /*
         if(playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit()=="aerial"){priority = 0;}
         if(playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit()=="tank"){priority = dist - disadvantge}
         else{priority = dist;}
         */
    }
    public void ArcherIA()
    {
        //Simplemente comprueba enemigos cercanos y le da prioridad a si ve alguno que podría
        //estar en su rango de ataque. Si ve una unidad voladora le dará prioridad siempre, intentando 
        //dentro de lo que cabe quedar demasiado cerca de enemigos que puedan matarlo en el siguiente turno
        //(principalmente tanques, por ejemplo)

        //¿A lo mejor funcionaría mejor que hiciese return de la unidad más "peligrosa" o prioritaria?
        /*
 if(playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit()=="aerial"){priority = float.MaxValue;}
 if(playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit()=="tank"){priority = dist - disadvantge}
 else{priority = dist;}
 */
    }
    public void TankIA()
    {
        //Simplemente comprueba enemigos cercanos y le da prioridad a si ve alguno que podría
        //estar en su rango de ataque. Se mueve hacia la unidad más cercana y evita a los aereos.


        /*
         if(playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit()=="aerial"){priority = 0;}
         else{priority = dist;}
         */

    }
    public void AerialIA()
    {
        //Simplemente comprueba enemigos cercanos y le da prioridad a si ve alguno que podría
        //estar en su rango de ataque. Se mueve hacia la unidad más cercana y si ve un arquero en 
        //su rango de ataque lo prioriza, ¿si no está en su rango de ataque evita ponerse en el suyo?


        /*
         if(playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit()=="archer"){priority = float.MaxValue;}
         if(playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit()=="aerial"){priority = dist + advantge}
         else{priority = dist;}
         */
    }


    public void StatsTell()
    {
        Debug.Log("movement" + movement + ", atk range " + attackRange + " type " + type + ", del equipo "+team);
    }
}



