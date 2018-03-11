using UnityEngine;

public class Pheromone {
    public  double pheromone;
    public static readonly double MAX = 1000.0;
    public static readonly double MIN = 0.001; //needs to be > 0
	public static readonly double MINCOLOR = 1.0;
	public static readonly double MAXCOLOR = 30.0;

	private double _rho;
	private double _Q;

    public Pheromone(){
        _rho = Colony.rho;
        _Q = Colony.Q;
		this.pheromone = Pheromone.MIN;
    }
    public void evaporate(){
        pheromone = (1.0 - _rho) * pheromone;
		checkAmount();
    }
    public void increase(double length){
        // we want shortest path, so there will be more pheromone on shorter paths
        pheromone += _Q / length;
//		Debug.Log (pheromone);
		checkAmount();
    }
	private void checkAmount(){
		if (pheromone < Pheromone.MIN)
			pheromone = Pheromone.MIN;
		if (pheromone > Pheromone.MAX)
			pheromone = Pheromone.MAX;
	}
}