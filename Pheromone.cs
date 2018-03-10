using UnityEngine;

public class Pheromone {
    public  double pheromone;
    public static readonly double MAX = 100000.0;
    public static readonly double MIN = 0.0001;

	private double _rho;
	private double _Q;

    public Pheromone(){
        _rho = Colony.rho;
        _Q = Colony.Q;
		this.pheromone = Pheromone.MIN;
    }
    public void evaporate(){
        pheromone = (1.0 - _rho) * pheromone;
    }
    public void increase(double length){
        // we want shortest path, so there will be more pheromone on shorter paths
        pheromone += _Q / length;
    }
}