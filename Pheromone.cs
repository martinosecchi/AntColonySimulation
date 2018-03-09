
public class Pheromone {
    public  double pheromone = 0.01;
    private double _rho;
    private double _Q;
    private double MAX = 100000.0;
    private double MIN = 0.0001;
    private Vector3 position;

    public Pheromone(double rho, double Q){
        _rho = rho;
        _Q = Q;
    }
    public void evaporate(){
        pheromone = (1.0 - _rho) * pheromone;
    }
    public void increase(double length){
        // we want shortest path, so there will be more pheromone on shorter paths
        pheromone += Q / length;
    }
    public void place(Vector3 position){
        this.position = position;
    }
}