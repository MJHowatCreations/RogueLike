
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    [Serializable] //When storing the contents of a object to a file the bytes must be converted to a different format. This is serialization. Allows the state of an object to be saved. 
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8; //Amount of columns and rows. Will make a box.
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5); //Details how many inner wall and food objects will be spawned
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTile; //Containers for the different sprite tiles.

    private Transform boardHolder; //Establishs a hierarchy ?? 
    private List<Vector3> gridPositions = new List<Vector3>(); //The positions on the grid

    void InitializeList ()
    {
        gridPositions.Clear();
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        } 
        //Creates a vector list where enemies, food and walls will spawn. Initialized to 1 and columns / rows - 1 so there will always be a way to the exit
    }
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toIntantiate = floorTiles[Random.Range(0, floorTiles.Length)]; //Creates a floor tile gameobject with a random sprite. It is floorTiles.Length so we can add more or different sprites to the pack and have it still work 
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toIntantiate = outerWallTile[Random.Range(0, outerWallTile.Length)];
                }//Sets up the outer walls at -1x and -1y and rows / columns and selects a tiles from the outerWallTiles at random. 

                GameObject instance = Instantiate(toIntantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                //Instantiates the tiles at the correct vector with no rotation (quaternion.identity) and casts it as a game object
                instance.transform.SetParent(boardHolder);

            }
        }
        
    }
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        //Finds a random vector position from our inner board to spawn a enemy, food object or wall and then removes that vector so nothing else can be spawned there. Returns this vector location.
        return randomPosition;
    }
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }

    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitializeList();
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0F), Quaternion.identity);
    }

}
