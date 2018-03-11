using UnityEngine;
using System.Collections;

public class Colony : MonoBehaviour{
	public static int width = 20;	   	// grid width,  used for path finding
	public static int height = 20; 		// grid height, used for path finding
	public static int alpha = 3;       	// pheromone influence factor
	public static int beta  = 2;       	// adjacent node distance influence
	public static double rho = 0.01;   	// pheromone decrease factor
	public static double Q = width/4.0;      	// pheromone increase factor
	public static int nAnts = 5;
	public static int foodSize = 1000; 	// how much can be taken from a single food item
	public static int nFoods = 1;		// how many foods in the grid
	public static int foodCounter = 0;
	public static float waitTime = 0.0f;

    private Ant[] ants;
	private bool creatingFood;
	private bool sendingAnts;

	void Start(){
		new Grid();
		Grid.assignColony(gameObject);
		ants = new Ant[Colony.nAnts];
		for (int n = 0; n < Colony.nAnts; n++) {
			ants[n] = new Ant();
		}
		Colony.foodCounter = 0;
		creatingFood = false;
		sendingAnts = false;
	}

	void Update(){
		if(Colony.foodCounter < Colony.nFoods && !creatingFood){
			StartCoroutine("generateFood");
		}
		if (!sendingAnts){
			StartCoroutine("sendAnts");
		}
	}

	IEnumerator sendAnts(){
		sendingAnts = true;
		foreach (Ant ant in ants) {
			ant.findFood();
		}
		Grid.evaporateCells();
		foreach(Ant ant in ants){
			if (ant.hasFood()){
				int length = ant.trail.Count;
				foreach (Vector3 pos in ant.trail){
					Cell c = Grid.getCell(pos);
					c.pheromone.increase(length);
				}
			}
		}
		yield return new WaitForSeconds(Colony.waitTime);
		sendingAnts = false;
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