using System.Collections.Generic;
using UnityEngine;


public enum Direction{
    left,
    top,
    bottom,
    right
}

public class DrawBasicMap : MonoBehaviour{
    private const int BOARD_SCALE = 4;

    //Public parameters
    public int boardWidth = 100;

    public int boardHeight = 100;
    public IntRange roomsCount = new IntRange(10, 12);
    public IntRange roomSize = new IntRange(3, 10);
    public IntRange corridorLength = new IntRange(3, 10);
    public IntRange EnemiesCount = new IntRange(10, 20);

    private SparseArray<ReachableMapElement> rooms = new SparseArray<ReachableMapElement>();

    //Private parameters
    public int[][] boardMatrix;

    public GameObject TrapGO;
    public GameObject Character;
    public GameObject Camera;
    public GameObject EnemyPrefab;
    public GameObject ChildrenPrefab;

    void Start(){
        initBoardMatrix();
        createRoomsAndCorridors();
        instantitateTiles();
    }

    void initBoardMatrix(){
        boardMatrix = new int[boardWidth][];
        for (int i = 0; i < boardWidth; i++){
            boardMatrix[i] = new int[boardHeight];
            for (int j = 0; j < boardHeight; j++){
                boardMatrix[i][j] = 0;
            }
        }
    }

    void createRoomsAndCorridors(){
        int roomCount = roomsCount.Random();
        int x = boardWidth / 2;
        int y = boardHeight / 2;
        int xi = x;
        int yi = y;
        for (int i = 0; i < roomCount; i++){
            int currRoomSize = roomSize.Random();
            int buildFactorX = 1;
            int buildFactorY = 1;
            if (xi + currRoomSize >= boardMatrix.Length){
                buildFactorX = -1;
            }
            if (yi + currRoomSize >= boardMatrix[0].Length){
                buildFactorY = -1;
            }

            for (int w = 0; w < currRoomSize; w++){
                for (int p = 0; p < currRoomSize; p++){
                    try{
                        boardMatrix[xi + (w * buildFactorX)][yi + (p * buildFactorY)] = i;
                    }
                    catch (System.Exception e){
                        Debug.Log("x: " + (xi + (w * buildFactorX)) + " y: " + (yi + (p * buildFactorY)));
                    }
                }
            }

            Direction direction = (Direction) Random.Range(0, 4);
            int corridorlenght = corridorLength.Random() + currRoomSize / 2;
            xi += currRoomSize / 2;
            yi += currRoomSize / 2;
            for (int k = 0; k < corridorlenght; k++){
                switch (direction){
                    case Direction.left:
                        xi -= 1;
                        break;
                    case Direction.right:
                        xi += 1;
                        break;
                    case Direction.top:
                        yi += 1;
                        break;
                    case Direction.bottom:
                        yi -= 1;
                        break;
                }

                if (xi < boardMatrix.Length && yi < boardMatrix[0].Length)
                    if (boardMatrix[xi][yi] == 0){
                        boardMatrix[xi][yi] = -1;
                    }
            }
        }
    }


    void instantitateTiles(){
        for (int i = 0; i < boardWidth; i++){
            for (int j = 0; j < boardHeight; j++){
                try{
                    if (boardMatrix[i - 1][j] != 0
                        || boardMatrix[i + 1][j] != 0
                        || boardMatrix[i][j - 1] != 0
                        || boardMatrix[i][j + 1] != 0){
                        int code = boardMatrix[i][j];
                        GameObject obj;
                            if(code>0 && i%2==0 && j%3==0) {
                            obj = Instantiate(TrapGO);
                        }else{
                            obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        }
                        obj.transform.position = new Vector3(j * BOARD_SCALE, 0, i * BOARD_SCALE);
                        obj.transform.localScale = new Vector3(1f * BOARD_SCALE, 1f, 1f * BOARD_SCALE);
                        if (code > 0){
                            //We have a room here
                            applyColorToGameObject(obj, Utils.getRandomColor(boardMatrix[i][j]));
                            ReachableMapElement element = rooms.get(code);
                            if (element == null){
                                element = new ReachableMapElement();
                                rooms.add(code, element);
                            }
                            element.addGameObject(obj);
                        }
                        else if (code == 0){
                            //We have a wall here
                            applyColorToGameObject(obj, Color.black);
                            obj.transform.localScale = new Vector3(1f * BOARD_SCALE, 2* BOARD_SCALE, 1f * BOARD_SCALE);
                        }
                        else if (code < 0){
                            //We have a corridor here
                            applyColorToGameObject(obj, Color.cyan);
                        }
                    }
                }
                catch (System.Exception e){
                    Debug.Log(e.ToString());
                }
            }
        }

        Vector3 randomVector  = rooms.getRandomElement().getRandomVector3(1);
        randomVector.y = 1;
        Character.transform.position = randomVector;
        randomVector.y = Camera.transform.position.y;
        randomVector.z -= 10;
        Camera.transform.position = randomVector;

        foreach (ReachableMapElement reachableRoom in rooms){

        }
        for (int i = 0; i < 80; i++){
            if (i % 3 == 0){
                Instantiate(ChildrenPrefab, rooms.get(Random.Range(0, rooms.Size() - 1)).getRandomVector3(2),
                    transform.rotation);

            }
            else{
                if (EnemyPrefab != null)
                    Instantiate(EnemyPrefab, rooms.get(Random.Range(0, rooms.Size() - 1)).getRandomVector3(2),
                        transform.rotation);
                else{
                    Debug.Log("Enemy prefab is null?");
                }

            }
        }

    }

    void applyColorToGameObject(GameObject obj, Color col){
        obj.GetComponent<Renderer>().material.color = col;
    }
}
