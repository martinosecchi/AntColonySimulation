using UnityEngine;

public class Food{
	public int amount;
	public Food(){
		amount = Colony.foodSize;
	}
    public void take(){
        amount -= 1;
    }
    
}