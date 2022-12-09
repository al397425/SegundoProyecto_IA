using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
//using Cinemachine;

public class GameHandler_GridCombatSystem : MonoBehaviour {

    public static GameHandler_GridCombatSystem Instance { get; private set; }

    [SerializeField] private Transform cinemachineFollowTransform;
    //[SerializeField] private MovementTilemapVisual movementTilemapVisual;
    //[SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    //private Grid<GridCombatSystem.GridObject> grid;
    //private MovementTilemap movementTilemap;
    
    public TilemapVisual tilemapVisual;
    private Tilemap tilemap;
    private Tilemap.TilemapObject.TilemapSprite tilemapSprite;
    
    private Tilemap.TilemapObject.TilemapSprite[,] spriteMatrix = new Tilemap.TilemapObject.TilemapSprite[grid_x_size, grid_y_size];    //Matriz para guardar el tipo de tile y pasarlo al pathfinding para calcular pesos
    private Grid<Tilemap.TilemapObject> tilemapGrid;    //El grid del tilemap para poder sacar cada uno de los objetos y comprobar su tipo de tile

    public Pathfinding pathfinding;

    static int grid_x_size = 20;
    static int grid_y_size = 20;
    static int total_size = grid_x_size * grid_y_size;
    float cellSize = 10f;
    Vector3 origin = new(0, 0);
    
    private void Awake() {
        Instance = this;

        
        
        //grid = new Grid<>(grid_x_size, grid_y_size, 10f, origin, (g, x, y) => new PathNode(g, x, y));

        //grid = new Grid<GridCombatSystem.GridObject>(grid_x_size, grid_y_size, cellSize, origin, (Grid<GridCombatSystem.GridObject> g, int x, int y) => new GridCombatSystem.GridObject(g, x, y));

        pathfinding = new Pathfinding(grid_x_size, grid_y_size, spriteMatrix);

        //movementTilemap = new MovementTilemap(grid_x_size, grid_y_size, cellSize, origin);
    }

    private void Start() {
        spriteMatrix = new Tilemap.TilemapObject.TilemapSprite[grid_x_size, grid_y_size];
        tilemap = new Tilemap(grid_x_size, grid_y_size, 10f, Vector3.zero);
        tilemap.SetTilemapVisual(tilemapVisual);
        GenerateMap();
        //movementTilemap.SetTilemapVisual(movementTilemapVisual);
        /*
        movementTilemap.SetAllTilemapSprite(MovementTilemap.TilemapObject.TilemapSprite.Move);
        grid.GetXY(new Vector3(171.5f, 128.5f), out int testX, out int testY);
        FunctionTimer.Create(() => {
            movementTilemap.SetAllTilemapSprite(MovementTilemap.TilemapObject.TilemapSprite.None);
            movementTilemap.SetTilemapSprite(testX + 0, testY + 0, MovementTilemap.TilemapObject.TilemapSprite.Move);
            movementTilemap.SetTilemapSprite(testX + 1, testY + 0, MovementTilemap.TilemapObject.TilemapSprite.Move);
            movementTilemap.SetTilemapSprite(testX - 1, testY + 0, MovementTilemap.TilemapObject.TilemapSprite.Move);
            movementTilemap.SetTilemapSprite(testX + 0, testY + 1, MovementTilemap.TilemapObject.TilemapSprite.Move);
            movementTilemap.SetTilemapSprite(testX + 0, testY - 1, MovementTilemap.TilemapObject.TilemapSprite.Move);

            movementTilemap.SetTilemapSprite(testX + 2, testY + 0, MovementTilemap.TilemapObject.TilemapSprite.Move);
            movementTilemap.SetTilemapSprite(testX + 1, testY + 1, MovementTilemap.TilemapObject.TilemapSprite.Move);
            movementTilemap.SetTilemapSprite(testX + 1, testY - 1, MovementTilemap.TilemapObject.TilemapSprite.Move);

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 5; j++) {
                    movementTilemap.SetTilemapSprite(testX + i, testY + j, MovementTilemap.TilemapObject.TilemapSprite.Move);
                }
            }
        }, 1f);
        */
    }

    private void Update() {
        
    }

    private void GenerateMap(){
        //Ground -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Ground;
        //Path  -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
        //Water -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
        //Dirt -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Dirt;
        //Mountain -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mountain;
        /*Invoke(nameof(GenerateGround), .1f);
        Invoke(nameof(GenerateWater), .1f);
        Invoke(nameof(GenerateMountains), .1f);
        Invoke(nameof(GenerateDirt), .1f);
        Invoke(nameof(GeneratePath), .1f);*/
        /*GenerateGround();
        GenerateMountains();
        GenerateWater();
        GenerateDirt();
        GeneratePath();*/
        
        StartCoroutine(Timer());

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        GenerateGround();
        /*yield return new WaitForSeconds(1f);
        GenerateDirt();*/
        yield return new WaitForSeconds(1f);
        GenerateMountains();
        yield return new WaitForSeconds(1f);
        GenerateLake();
        yield return new WaitForSeconds(1f);
        GeneratePath();
    }

    private void GenerateLake()
    {
        var cantidad = Random.Range(5, 11);   //Cantidad de lagos. Aleatoria?
        int lakeType;
        int originX, originY;  //Origen del primer cuadrado del lago. Se haria aleatoriamente. Quizas entre [2, x-2] [y, 3] 
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
        
        for (int i = 0; i < cantidad; i++)
        {
            lakeType = Random.Range(1, 3);

            switch (lakeType)
            {
                //Lago cuadrado
                case 1:
                    originX = Random.Range(2, grid_x_size - 2);
                    originY = Random.Range(grid_y_size - 1, 3);

                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY, tilemapSprite);
                    spriteMatrix[originX + 1, originY] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY - 1, tilemapSprite);
                    spriteMatrix[originX + 1, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX, originY - 1, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    break;
                //Lago diamante
                case 2:
                    originX = Random.Range(3, grid_x_size - 2);
                    originY = Random.Range(grid_y_size - 1, 3);
                    
                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX, originY + 1, tilemapSprite);
                    spriteMatrix[originX, originY + 1] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY, tilemapSprite);
                    spriteMatrix[originX + 1, originY] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX, originY - 1, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY, tilemapSprite);
                    spriteMatrix[originX - 1, originY] = Tilemap.TilemapObject.TilemapSprite.Water;
                    break;
            }
        }
    }
    
    private void GenerateGround(){
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Ground;
        for(var x = 0; x <grid_x_size; x++)
        {
            for(var y = 0; y < grid_y_size; y++)
            {
                tilemap.SetTilemapSprite(x, y, tilemapSprite);
                spriteMatrix[x, y] = Tilemap.TilemapObject.TilemapSprite.Ground;
            }
        }
    }
    
    /*private void GenerateGround(){
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Ground;
        for(int i = 0; i <total_size;i++){
            for(int z = 0; z < total_size;z++){
                Vector3 RandomNumber = new Vector3(i,z);
                tilemap.SetTilemapSprite(RandomNumber, tilemapSprite);
            
                tilemap.GetTilemapGrid().GetXY(RandomNumber, out int a, out int b);
                if (a < grid_x_size && b < grid_y_size)
                    spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Ground;
            }
        }
    }*/
    
    /*private void GenerateDirt()
    {
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Dirt;
        Vector3 newcoords;
        int RandomDirt = Random.Range(total_size/6, total_size/4 + total_size/8); //Random Quantity
        for(int i = 0; i < RandomDirt; i++){ //Random position
        Vector3 RandomNumber = new Vector3(Random.Range(5, total_size),
        Random.Range(5, total_size),Random.Range(5, total_size));
            tilemap.SetTilemapSprite(RandomNumber, tilemapSprite);
            
            tilemap.GetTilemapGrid().GetXY(RandomNumber, out int a, out int b);
            if (a < grid_x_size && b < grid_y_size)
                spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Dirt;
            
            //Tiles arround the original random tile
            for(int z = 0; z < Random.Range(5,10); z++){
                int random = Random.Range(1,6);
                if(random == 1){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (x < grid_x_size && y < grid_y_size)
                        spriteMatrix[x, y] = Tilemap.TilemapObject.TilemapSprite.Dirt;
                    
                }else if(random == 2){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Dirt;
                    
                }else if(random == 3){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(0, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Dirt;
                
                }else if(random == 4){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(0, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Dirt;
                
                }else if(random == 5){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Dirt;
                
                }else if(random == 6){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Dirt;
                }
        }
        }
    }*/
    private void GenerateMountains(){
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mountain;
        Vector3 newcoords;
        int RandomMountains = Random.Range(total_size/8, total_size/4 ); //Random Quantity
         for(int i = 0; i < RandomMountains; i++){ //Random position
        Vector3 RandomNumber = new Vector3(Random.Range(10, total_size/2 + total_size/8),
        Random.Range(10, total_size/2 + total_size/8),Random.Range(10, total_size/2 + total_size/8));
            tilemap.SetTilemapSprite(RandomNumber, tilemapSprite);
            
            tilemap.GetTilemapGrid().GetXY(RandomNumber, out int a, out int b);
            if (a < grid_x_size && b < grid_y_size)
                spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
            
            //Tiles arround the original random tile
            for(int z = 0; z < Random.Range(4,6); z++){
                int random = Random.Range(1,6);
                if(random == 1){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                }else if(random == 2){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                }else if(random == 3){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(0, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                
                }else if(random == 4){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(0, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                
                }else if(random == 5){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                
                }else if(random == 6){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
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
            
            tilemap.GetTilemapGrid().GetXY(RandomNumber, out int a, out int b);
            if (a < grid_x_size && b < grid_y_size)
                spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
            
            //Tiles arround the original random tile
            for(int z = 0; z < Random.Range(1,3); z++){
                int random = Random.Range(1,6);
                if(random == 1){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
                    //tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
                    //tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(Random.Range(-1,1), Random.Range(-1,1), 1), tilemapSprite);
                }else if(random == 2){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;

                }else if(random == 3){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(0, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
                
                }else if(random == 4){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(0, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
                
                }else if(random == 5){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
                
                }else if(random == 6){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
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
            
            tilemap.GetTilemapGrid().GetXY(RandomNumber, out int a, out int b);
            if (a < grid_x_size && b < grid_y_size)
                spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Path;
            //Tiles arround the original random tile
            int random = Random.Range(1,4);
            for(int z = 0; z < Random.Range(4,6); z++){
                if(random == 1){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(z, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Path;
                    
                }else if(random == 2){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(z, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Path;
                    
                }else if(random == 3){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(0, z, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Path;
                
                }else if(random == 4){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(0, z, 1), tilemapSprite);
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_size)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Path;
                }
            }
            
        }
    }


    /*public Grid<GridCombatSystem.GridObject> GetGrid() {
        return grid;
    }

    public MovementTilemap GetMovementTilemap() {
        return movementTilemap;
    }*/

    

    /*public void ScreenShake() {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 15f;

        FunctionTimer.Create(() => { cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f; }, .1f);
    }*/





    public class EmptyGridObject {

        private Grid<EmptyGridObject> grid;
        private int x;
        private int y;

        public EmptyGridObject(Grid<EmptyGridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;

            Vector3 worldPos00 = grid.GetWorldPosition(x, y);
            Vector3 worldPos10 = grid.GetWorldPosition(x + 1, y);
            Vector3 worldPos01 = grid.GetWorldPosition(x, y + 1);
            Vector3 worldPos11 = grid.GetWorldPosition(x + 1, y + 1);

            Debug.DrawLine(worldPos00, worldPos01, Color.white, 999f);
            Debug.DrawLine(worldPos00, worldPos10, Color.white, 999f);
            Debug.DrawLine(worldPos01, worldPos11, Color.white, 999f);
            Debug.DrawLine(worldPos10, worldPos11, Color.white, 999f);
        }

        public override string ToString() {
            return "";
        }
    }
}
