using UnityEngine;
using System;
using System.Collections.Generic;

public class Ant{
    private System.Random random;
    private bool[] visited;
	private bool foodFlag;
	
    public LinkedList<Vector3> trail;
    // only update trail when the new position of the ant gives a new grid cell
    // use Grid.getPosition to find the Cell positions
    // use Grid.getCell to get the Cell given a position

    // we don't need to see the ants, just the pheromone trails on the cells

    public Ant(){
        this.random = new System.Random();
		this.foodFlag = false;
    }

	public bool hasFood(){
		return foodFlag;
	}

    // an ant is just a path, from the colony to a food. 
    // once the food is reached, the ant comes back from the same path, so really it's just a 1-way path TO the food
    public void findFood(){
//		while (!hasFood()){
			this.foodFlag = false;
			this.trail = new LinkedList<Vector3>();
	        this.visited = new bool[Grid.getWidth() * Grid.getHeight()];
	        this.trail = new LinkedList<Vector3>();

	        // start from Colony cell
	        Cell current = Grid.colonyCell;
	        
	        //add a neighboring cell (if not visited) until current cell contains food or no path is available
	        //choose next cell depending on probabilities computed with alpha and cell pheromone
			while(current!=null && !current.hasFood()){
            	trail.AddLast(current.position());
            	visited[Grid.getIndex(current.position())] = true;
            	current = nextCell(current);
        	}
			// found food
	        if (current && current.hasFood()){
	            current.takeFood();
				this.foodFlag = true;
			}
//		}	
    }

    private Cell nextCell(Cell current){
        Cell[] available = Grid.getNeighbors(current);
		Cell next = null;
		int count = available.Length;
		for( int i=0; i< available.Length; i++){
			if (available[i] && visited[Grid.getIndex(available[i].position())])
				available[i] = null;
			if (available == null)
				count--;
		}
		if (count == 0)
			return null;
		double[] probs = getProbabilities(available, current);
		double[] cumul = new double[probs.Length + 1];
		for (int i = 0; i < probs.Length; i++){
			cumul[i + 1] = cumul[i] + probs[i];
			// consider setting cumul[cuml.Length-1] to 1.00
		}
		
		double p = random.NextDouble();
		
		for (int i = 0; i <= cumul.Length - 2; i++){
			if (p >= cumul[i] && p < cumul[i + 1]){
				next = available[i];
				break;
			}
		}
		return next;
	}
	
	private double[] getProbabilities (Cell[] available, Cell current){
		double[] probs = new double[available.Length];
		double[] taueta = new double[probs.Length];
		double sum = 0.0;
		Cell next;
		for(int i=0; i < taueta.Length; i++) {
			next = available[i];
			if (next == null){
				taueta[i] = 0.0; // if no path prob is 0
			} else {
				// tau = trail, eta = attractiveness
				taueta[i] = Math.Pow(next.pheromone.pheromone, Colony.alpha) * Math.Pow((1.0/Grid.Distance(current, next)), Colony.beta);
				if (taueta[i] < 0.0001){
					taueta[i] = 0.0001;
				}
				else if (taueta[i] > (double.MaxValue / (Grid.numCells() * 100))){
					taueta[i] = double.MaxValue / (Grid.numCells() * 100);
				}
			}
			sum += taueta[i];
		}

		for (int i = 0; i < probs.Length; i++){
			probs[i] = taueta[i] / sum; // sum should be > 0 if we have available cells with at least min pheromone>0
		}
		return probs;
		}
}