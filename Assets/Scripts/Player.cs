using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
public class Player : MovingObject {

    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;

    private Animator animator;
    private int food;


    //Start overrides the Start function of MovingObject
    protected override void Start ()
    {
        //Get a component reference to the Player's animator component
        animator.GetComponent<Animator>();

        //Get the current food point total stored in GameManager.instance between levels.
        food = GameManager.instance.playerFoodPoints;

        //Call the Start function of the MovingObject base class.
        base.Start();

    }

    private void OnDisabled()
    {
        GameManager.instance.playerFoodPoints = food;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
