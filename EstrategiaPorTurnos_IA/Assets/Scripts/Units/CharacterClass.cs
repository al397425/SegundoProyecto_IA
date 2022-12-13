using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharacterClass:MonoBehaviour, ICharacter 
{
    public int movement; //número casillas que se podrá desplazar
    public int attackRange;
    public int health;
    public int attack;
    public string type; //tipo de unidad
    public bool wasMoved; //para saber si la unidad ya fue usada
    public int id;
    public int[] position = { 0, 0 };
    public int team;
    public UnitSelection unitSel;
    public Button attackButton;

    public void Awake()
    {

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
            unitSel.activateUnit(gameObject);

            //¿Habría que meterlos todos en una lista/recorrerlos para deshabilitarlos? Eso o en el game manager más fácil

        }
     
    }

    public void SetStats()
    {
        if (this.type == "archer")
        {
            movement = 3;
            attackRange = 3;
            health = 1;
            attack = 2;
            type = "archer";
        }
        else if (this.type == "infantry")
        {
            movement = 3;
            attackRange = 1;
            health = 3;
            attack = 2;
            type = "infantry";
        }
        else if (this.type == "tank")
        {
            movement = 2;
            attackRange = 2;
            health = 4;
            attack = 3;
            type = "tank";

        }
        else if (this.type == "aerial")
        {
            movement = 5;
            attackRange = 2;
            health = 2;
            attack = 2;
            type = "aerial";
        }
        else
        {
            Debug.Log("No hay tipo, es "+ this.type);
        }
        StatsTell();


    }

    //La idea aquí ha sido que haya un MoveUnit genérico que compruebe qué clase es y ya llame
    //al especifico (en verdad esto es más donde iría lo de la IA y luego el moverlo en sí
    //sería maybe al final de los if o algo, y que la IA llame a todo esto pero el jugador sea
    //directamente solo comprobar si es posible con los movimientos y si lo es se mueve sin más

    public void MoveUnit(int x, int y)
    {
        Debug.Log("Esta unidad se mueve " + this.movement + " de distancia");

        if (team != 1) //la IA (?) Podría haber sido un booleano, pues la verdad es que sí, luego
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
        //si es la IA es la posición que calcule con la función especifica)



        //si tiene en rango a una unidad enemiga muestra un botón de atacar, si le das llamas a AttackUnit


    }


    

    public void AttackUnit(GameObject rival) 
    {
       
        //comprueba si está en el rango de ataque / si es voladora y tú melee etc


        //si lo está se inicia el enfrentamiento entre las unidades
        int rivalHP = rival.GetComponent<CharacterClass>().health;
        int rivalATK = rival.GetComponent<CharacterClass>().attack;
        string rivalUnit = rival.GetComponent<CharacterClass>().type; //esto por si luego se nos va la olla y metemos velocidad/prioridad de ataques

        Debug.Log("La unidad " + type + " ataca a " + rivalUnit);

        rivalHP = rivalHP - attack;

        Debug.Log("El rival ha sufrido " + attack + "puntos de daño, tiene " + rivalHP + " de vida");

        if (rivalHP <= 0)
        {
            //destruyes al rival
            Destroy(rival);
            Debug.Log("La unidad enemiga ha sido derrotada");
        }
        else
        {
            health = health - rivalATK;

            Debug.Log("Tu unidad sufre " + rivalATK + " de daño, tienes " + health + "puntos de vida");

            if (health <= 0) {
                Debug.Log("Tu unidad ha sido derrotada");
                Destroy(this.gameObject);
            }
            
        }


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


    //Puede que esto lo use, puede que se junte con lo del Move al final, no lo sé aún
    /*
    public void InfantryIA() { }
    public void ArcherIA() { }
    public void TankIA() { }
    public void AerialIA() { }
    */

    public void StatsTell()
    {
        Debug.Log("movement" + movement + ", atk range " + attackRange + " type " + type);
    }
}



//Esto es cómo lo tenía antes por si luego todo explota 
/*
public class CharacterClass : Character
{
    public BoxCollider2D collision;
    public int movement; //número casillas que se podrá desplazar
    public int attackRange;
    public int health;
    public int attack;
    public string type; //tipo de unidad
    public bool wasMoved; //para saber si la unidad ya fue usada
    public int id;
    public int[] position = { 0, 0 };

    // Start is called before the first frame update


    // Update is called once per frame
    public override void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //coge la posicion que has clicado

        BoxCollider2D collision = this.GetComponent<BoxCollider2D>();

        if (collision.OverlapPoint(mousePos) && Input.GetMouseButtonDown(0))
        {
            this.StatsTell();
        }
    }

    public override void SetStats()
    {
        if (this.type == "archer")
        {
            movement = 3;
            attackRange = 3;
            health = 1;
            attack = 2;
            type = "archer";
        }
        else if (this.type == "infantry")
        {
            movement = 3;
            attackRange = 1;
            health = 3;
            attack = 2;
            type = "infantry";
        }
        else if (this.type == "tank")
        {
            movement = 2;
            attackRange = 2;
            health = 4;
            attack = 3;
            type = "tank";

        }
        else if (this.type == "aerial") 
        {
            movement = 5;
            attackRange = 2;
            health = 2;
            attack = 2;
            type = "aerial";
        }
        else
        {
            Debug.Log("No hay tipo");
            this.type = "none";
        }


    }

    public override void MoveUnit(int x, int y)
    {
        Debug.Log("Esta unidad se mueve " + this.movement + " de distancia");
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
            MoveTank(x,y);

        }
        else if (this.type == "aerial")
        {
            MoveAerial(x,y);
        }
        //aquí código 
    }
    public override void AttackUnit()
    {
        Debug.Log("Esta unidad se mueve " + this.attackRange + " de distancia");
        //comprueba si está en el rango de ataque 


        //si lo está se inicia el enfrentamiento entre las unidades
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



    public void InfantryIA() { }
    public void ArcherIA() { }
    public void TankIA() { }
    public void AerialIA() { }

}
*/

/*
public class Archer : Character
    {
        
        public override void setStats() {
            movement = 3;
            attackRange = 3;
            health = 1;
            attack = 2;
            type = "archer";
        }

        public override void Update()
        {

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //coge la posicion que has clicado

            BoxCollider2D collision = this.GetComponent<BoxCollider2D>();

            if (collision.OverlapPoint(mousePos) && Input.GetMouseButtonDown(0))
            {
                this.statsTell();
            }
        }

        public override void moveUnit(int x, int y, int id) //tendría que indicarle a qué tile se mueve (?)
                                                            //cada unidad que se cree que tenga un id y al mover la unidad mira qué id tiene el pj y a qué tile lo quieres mover
                                                            //entonces cambia en el mapa la posicion (y se guarda cuales son sus coordenadas actuales maybe?)
        {
            //se muestra los posibles destinos

            Debug.Log("Esta unidad se mueve " + movement + " de distancia");
            //aquí código (en otro script vendría la IA en sí que una vez decidiese el movimiento llamaría a esta función)
            //tiene que comprobar si el tile al que lo quieres mover es viable en base a su rango de movimiento (aunque eso maybe se hace más con el pathfinding), si lo es lo mueve, si no no hace nada


            //poner
        }
        public override void attackUnit()
        {
            
            //comprueba si está en el rango de ataque


            //si lo está se inicia el enfrentamiento entre las unidades
        }

    }

    public class Infantry : Character
    {
        public override void setStats()
        {
            movement = 3;
            attackRange = 1;
            health = 3;
            attack = 2;
            type = "infantry";
        }

        public override void moveUnit(int x, int y, int id)
        {

            Debug.Log("Esta unidad se mueve " + movement + " de distancia");
            //aquí código 
        }
        public override void attackUnit()
        {
            attackRange = 1;
            //comprueba si está en el rango de ataque


            //si lo está se inicia el enfrentamiento entre las unidades
        }
    }

    public class Tank : Character
    {
        public override void setStats()
        {
            movement = 2;
            attackRange = 2;
            health = 4;
            attack = 3;
            type = "tank";
        }
        public override void moveUnit(int x, int y, int id)
        {

            Debug.Log("Esta unidad se mueve " + movement + " de distancia");
            //aquí código 
        }
        public override void attackUnit()
        {
            attackRange = 2;
            //comprueba si está en el rango de ataque


            //si lo está se inicia el enfrentamiento entre las unidades
        }
    }

    public class Aerial : Character
    {
        public override void setStats()
        {
            movement = 5;
            attackRange = 2;
            health = 2;
            attack = 2;
            type = "aerial";
        }
        public override void moveUnit(int x, int y, int id)
        {
            movement = 4;
            Debug.Log("Esta unidad se mueve " + movement + " de distancia");
            //aquí código 
        }
        public override void attackUnit()
        {
            attackRange = 2;
            //comprueba si está en el rango de ataque


            //si lo está se inicia el enfrentamiento entre las unidades
        }
    }


*/

