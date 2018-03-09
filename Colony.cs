public class Colony{
    public int alpha = 3;       // pheromone influence factor
    public int beta  = 2;       // adjacent node distance influence
    public double rho = 0.01;   // pheromone decrease factor
    public double Q = 2.0;      // pheromone increase factor
    public int width = 10;      // width of the grid
    public int height = 10;     //height of the grid
    private Ant[] ants;
    private Food[] foods;
    private Grid grid;
}