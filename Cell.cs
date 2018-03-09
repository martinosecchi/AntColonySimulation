public class Cell{
    
    public Pheromone pheromone;
    public Food food;
    public Vector3 position;

    private Color color;
    
    public Cell(){
        this.pheromone = Pheromone();
        this.color = Color();
        this.food = null;
    }

    public bool hasFood(){
        return this.food != null;
    }

    public void placeFood(Food f){
        this.food = f;
    }

    public void Start(){}

    public void Update(){
        updateColor();
    }

    private void updateColor(){
        // depending how much pheromone is in this cell, color the cell
    }
}