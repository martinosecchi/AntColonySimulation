
public class Grid{
    private  int width;
    private  int height;

    private Cell[] cells;

    public Grid(){
         cells = new Cell[width * height];
    }

    public Cell getCell(Vector3 position){
        return cells[getIndex(position)];
    }

    public Vector3 getPosition(Vector3 position){
        Vector3 pos = Vector3((int) position.x, (int) position.y, (int) position.z);
        return pos;
    }

    public int getIndex(Vector3 position){
        return getIndex((int) position.x, (int) position.y);
    }
    public int getIndex(int x, int y){
        return (width * x) + y;
    }
    public Cell[] getNeighbors(Cell cell){
        int pos = getIndex(cell.position);
        // TODO something like that:
        int col = (width * pos - pos)/width  - 1;
        int row = (pos - col)/width;
        //now get neighbors in (row+-1, col) (row, col+-1) (row+-1, col+-1)
        // up to 6 neighboring cells
        for (int i=-1; i<=1; i++){
            for (int j=-1; j<=1; j++){
                //getIndex(row+i, col+j)
            }
        }
    }
}
