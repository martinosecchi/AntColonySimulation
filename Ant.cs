using System.Collection.Generic;

public class Ant{
    public bool hasFood;
    private Cell startCell;
    private Random random;
    private bool[] visited;

    
    public Vector3[] trail;
    // only update trail when the new position of the ant gives a new grid cell
    // use Grid.getPosition to find the Cell positions
    // use Grid.getCell to get the Cell given a position

    // we don't need to see the ants, just the pheromone trails on the cells

    public Ant(){
        this.random = new Random(0);
    }

    // an ant is just a path, from the colony to a food. 
    // once the food is reached, the ant comes back from the same path, so really it's just a 1-way path TO the food
    public void findFood(){
        this.hasFood = false;
        this.visited = new bool[Grid.width * Grid.height];
        LinkedList trail = new LinkedList<Vector3>();

        // start from Colony cell
        Cell current = startCell;
        
        //add a neighboring cell (if not visited) until current cell contains food
        //choose next cell depending on probabilities computed with alpha and cell pheromone
        while(!current.hasFood()){
            trail.AddLast(current.position);
            visited[Grid.getIndex(current.position)] = true;
            try {
                current = nextCell(current);
            } catch(Exception e){
                break;
            }
        }
        if (current.hasFood()){
            current.food.take();
            this.hasFood = true;
            this.trail = trail.toArray(); // or something
        } else {
            this.trail = null; // or something
        }
    }

    private Cell nextCell(Cell current){
        Cell[] available = Grid.getNeighbors(current);
        // exclude visited
        // if no available, give an exception so we stop searching
        // compute probabilities
        // pick a random.NextDouble() and return your next move
    }
}