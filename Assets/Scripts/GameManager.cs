using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script
    public BoardManager boardScript; //Store a reference to our BoardManager which will set up the level.
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;


    private int level = 3;  //Current level number, expressed in game as "Day 1".


    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null) //if instance is null use this gamemanager
            instance = this;
        else if (instance != this) //If instance already exists and it's not this:
            Destroy(gameObject);//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.



        DontDestroyOnLoad(gameObject); //persists scene to keep track of score
        boardScript = GetComponent<BoardManager>();        //Get a component reference to the attached BoardManager script
        InitGame();
    }


    // Use this for initialization
    void InitGame () {
        boardScript.SetupScene(level);  //Call the SetupScene function of the BoardManager script, pass it current level number.
    }
	public void GameOver()
    {
        enabled = false;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
