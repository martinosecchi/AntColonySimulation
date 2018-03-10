using UnityEngine;
using System.Collections;

public class Colony : MonoBehaviour{
    public static int alpha = 3;       	// pheromone influence factor
    public static int beta  = 2;       	// adjacent node distance influence
    public static double rho = 0.01;   	// pheromone decrease factor
    public static double Q = 2.0;      	// pheromone increase factor
	public static int width = 10;	   	// grid width,  used for path finding
	public static int height = 10; 		// grid height, used for path finding
	public static int nAnts = 1;
	public static int foodSize = 100; 	// how much can be taken from a single food item
	public static int nFoods = 2;		// how many foods in the grid
	public static int foodCounter = 0;
	public static int RATE = 60; 		// ~ 1 sec

	private int frameCounter;
    private Ant[] ants;
	private bool creatingFood;

	void Start(){
		new Grid();
		Grid.assignColony(gameObject);
		ants = new Ant[Colony.nAnts];
		for (int n = 0; n < Colony.nAnts; n++) {
			ants[n] = new Ant();
		}
		frameCounter = RATE;
		Colony.foodCounter = 0;
		creatingFood = false;
	}

	void Update(){
		if(Colony.foodCounter < Colony.nFoods && !creatingFood){
			StartCoroutine("generateFood");
		}
		if (frameCounter == Colony.RATE){
			StartCoroutine("sendAnts");
			frameCounter = 0;
		}
		frameCounter++;
	}

	IEnumerator sendAnts(){
		Debug.Log("sending ants");
		foreach (Ant ant in ants) {
			ant.findFood();
		}
		yield return null;
	}

	IEnumerator generateFood(){
		creatingFood = true;
		while(Colony.foodCounter < Colony.nFoods){
			Grid.createFood();
			Colony.foodCounter++;
		}
		creatingFood = false;
		yield return null;
	}
}