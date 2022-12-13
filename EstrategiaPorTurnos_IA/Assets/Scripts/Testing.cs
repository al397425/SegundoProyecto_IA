using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Testing : MonoBehaviour {

    public TilemapVisual tilemapVisual;
    private Tilemap tilemap;
    private Tilemap.TilemapObject.TilemapSprite tilemapSprite;
    int x,y;
    static int grid_x_size = 20;
    static int grid_y_size = 20;
    static int total_size = grid_x_size * grid_y_size;
    private void Start() {
        tilemap = new Tilemap(grid_x_size, grid_y_size, 10f, Vector3.zero);

        tilemap.SetTilemapVisual(tilemapVisual);
        GenerateMap();
        GenerateCharacters("archer");
    }

    private void Update() {
        /*//Debug.Log(PerlinNoise());
        if (Input.GetMouseButton(0)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            tilemap.SetTilemapSprite(mouseWorldPosition, tilemapSprite);
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.None;
            CMDebug.TextPopupMouse(tilemapSprite.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Y)) {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Ground;
            CMDebug.TextPopupMouse(tilemapSprite.ToString());
        }
        if (Input.GetKeyDown(KeyCode.U)) {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
            CMDebug.TextPopupMouse(tilemapSprite.ToString());
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Dirt;
            CMDebug.TextPopupMouse(tilemapSprite.ToString());
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
            CMDebug.TextPopupMouse(tilemapSprite.ToString());
        }
        //Debug.Log(UtilsClass.GetMouseWorldPosition()+"mouseWorldPosition");
*/
    }

    private void GenerateCharacters(string unitType) {
        GameObject unitPrefab = GameObject.Find("CharacterPrefab");
        Vector3 v = new Vector3(10, 10, 0);
        GameObject characterUnit = Instantiate(unitPrefab, v, Quaternion.identity);
        characterUnit.GetComponent<CharacterClass>().type = unitType;
        characterUnit.GetComponent<CharacterClass>().SetStats();
        characterUnit.transform.SetParent(GameObject.Find("Units").transform, false);


    }

    private void GenerateMap(){
        //Ground -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Ground;
        //Path  -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
        //Water -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
        //Dirt -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Dirt;
        //Mountain -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mountain;
        GenerateGround();
        GenerateDirt();
        GenerateMountains();
        GenerateWater();
        GeneratePath();
        
    }
    private void GenerateGround(){
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Ground;
        for(int i = 0; i <total_size;i++){
            for(int z = 0; z < total_size;z++){
            Vector3 RandomNumber = new Vector3(i,z);
            tilemap.SetTilemapSprite(RandomNumber, tilemapSprite);
            }
        }
    }
    private void GenerateDirt(){
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Dirt;
        Vector3 newcoords;
        int RandomDirt = Random.Range(total_size/6, total_size/4 + total_size/8); //Random Quantity
        for(int i = 0; i < RandomDirt; i++){ //Random position
        Vector3 RandomNumber = new Vector3(Random.Range(5, total_size),
        Random.Range(5, total_size),Random.Range(5, total_size));
            tilemap.SetTilemapSprite(RandomNumber, tilemapSprite);
            //Tiles arround the original random tile
            for(int z = 0; z < Random.Range(5,10); z++){
                int random = Random.Range(1,6);
                if(random == 1){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 1, 1), tilemapSprite);
                }else if(random == 2){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 1, 1), tilemapSprite);
                }else if(random == 3){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(0, 1, 1), tilemapSprite);
                
                }else if(random == 4){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(0, 1, 1), tilemapSprite);
                
                }else if(random == 5){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 0, 1), tilemapSprite);
                
                }else if(random == 6){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 0, 1), tilemapSprite);
                }
        }
        }
    }
    private void GenerateMountains(){
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mountain;
        Vector3 newcoords;
        int RandomMountains = Random.Range(total_size/8, total_size/4 ); //Random Quantity
         for(int i = 0; i < RandomMountains; i++){ //Random position
        Vector3 RandomNumber = new Vector3(Random.Range(10, total_size/2 + total_size/8),
        Random.Range(10, total_size/2 + total_size/8),Random.Range(10, total_size/2 + total_size/8));
            tilemap.SetTilemapSprite(RandomNumber, tilemapSprite);
            //Tiles arround the original random tile
            for(int z = 0; z < Random.Range(4,6); z++){
                int random = Random.Range(1,6);
                if(random == 1){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 1, 1), tilemapSprite);
                }else if(random == 2){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 1, 1), tilemapSprite);
                }else if(random == 3){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(0, 1, 1), tilemapSprite);
                
                }else if(random == 4){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(0, 1, 1), tilemapSprite);
                
                }else if(random == 5){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 0, 1), tilemapSprite);
                
                }else if(random == 6){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 0, 1), tilemapSprite);
                }
        }
        }
            
    }

    private void GenerateWater(){
        Vector3 newcoords;
        int RandomWater = Random.Range(total_size/8, total_size/4); //Random Quantity
         for(int i = 0; i < RandomWater; i++){ //Random position
         tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
        Vector3 RandomNumber = new Vector3(Random.Range(10, total_size/2 + total_size/8),
        Random.Range(10, total_size/2 + total_size/8),Random.Range(10, total_size/2 + total_size/8));
            tilemap.SetTilemapSprite(RandomNumber, tilemapSprite);
            //Tiles arround the original random tile
            for(int z = 0; z < Random.Range(1,3); z++){
                int random = Random.Range(1,6);
                if(random == 1){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 1, 1), tilemapSprite);
                    //tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
                    //tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(Random.Range(-1,1), Random.Range(-1,1), 1), tilemapSprite);
                }else if(random == 2){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 1, 1), tilemapSprite);

                }else if(random == 3){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(0, 1, 1), tilemapSprite);
                
                }else if(random == 4){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(0, 1, 1), tilemapSprite);
                
                }else if(random == 5){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 0, 1), tilemapSprite);
                
                }else if(random == 6){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 0, 1), tilemapSprite);
                }
        }
        }
    }
    private void GeneratePath(){
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
        Vector3 newcoords;
        int RandomMountains = Random.Range(total_size/16, total_size/8 ); //Random Quantity
         for(int i = 0; i < RandomMountains; i++){ //Random position
        Vector3 RandomNumber = new Vector3(Random.Range(10, total_size/2 + total_size/8),
        Random.Range(10, total_size/2 + total_size/8),Random.Range(10, total_size/2 + total_size/8));
            tilemap.SetTilemapSprite(RandomNumber, tilemapSprite);
            //Tiles arround the original random tile
            int random = Random.Range(1,4);
            for(int z = 0; z < Random.Range(4,6); z++){
                if(random == 1){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(z, 0, 1), tilemapSprite);
                }else if(random == 2){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(z, 0, 1), tilemapSprite);
                }else if(random == 3){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(0, z, 1), tilemapSprite);
                
                }else if(random == 4){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(0, z, 1), tilemapSprite);
        }
        }
            
    }
}
}
