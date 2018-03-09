
// just a gameObject
public class Food{
    public int amount;

    public void take(){
        amount -= 1;
        if (amount <= 0){
            Destroy(this);  //TODO or whatever it is
        }
    }
    
}