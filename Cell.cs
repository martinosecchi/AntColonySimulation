using UnityEngine;

public class Cell : MonoBehaviour{
    
    public Pheromone pheromone;
    public Food food;

	private bool _isColony;

    public Cell(){
		this._isColony = false;
        this.pheromone = new Pheromone();
        this.food = null;
    }

	public Vector3 position (){
		return gameObject.transform.position;
	}

	public bool isColony(){
		return _isColony;
	}
	public void setAsColony (GameObject colonyGmObj){
		// TODO give it a sprite for colony
		_isColony = true;
		gameObject.GetComponent<SpriteRenderer>().color = Color.black;
	}

    public bool hasFood(){
        return this.food != null;
    }
	public void takeFood(){
		if (hasFood()){
			food.take ();
			if (food.amount <= 1){
				Colony.foodCounter--;
				food = null;
				gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
			}
		}
	}

    public void placeFood(Food f){
		// TODO give it sprite for food
        this.food = f;
		gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    public void Start(){}

    public void Update(){
		if (!_isColony)
        	updateColor();
    }

    private void updateColor(){
        // depending how much pheromone is in this cell, color the cell
		if (pheromone.pheromone > Pheromone.MIN){
			double perc = (pheromone.pheromone - Pheromone.MIN) / (Pheromone.MAX - Pheromone.MIN);
			gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.red, (float) perc);
		}	                                   
    }
}