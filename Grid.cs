using UnityEngine;
using System;

public class Grid{
	internal class InternalGrid {
		public Cell[] cells;
		public InternalGrid(){
			cells = new Cell[Grid.width * Grid.height];
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++) {
					GameObject o = new GameObject("cell "+i+j);
					SpriteRenderer sr = o.AddComponent<SpriteRenderer>();
					sr.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
					sr.color = Color.clear;
					o.transform.position = new Vector3(i, j, 0);
					Cell c = o.AddComponent<Cell>();
					cells[getIndex(i,j)] = c;
				}
			}
		}
	}
	private static int width = Colony.width;
	private static int height = Colony.height;
	private static InternalGrid grid = null;
	private static System.Random rand;
	public static Cell colonyCell = null; 

	public Grid(){
		if (Grid.grid == null){
			Grid.grid = new InternalGrid();
		}
		Grid.rand = new System.Random(42);
	}

	public static void assignColony (GameObject colony){
		colonyCell = getRandomCell();
		colonyCell.setAsColony(colony);
	}

	public static Food createFood(){
		Debug.Log ("grid is creating food.");
		Cell cell = getRandomCell();
		while (cell.isColony() || cell.hasFood()){
			cell = getRandomCell();
		}
		cell.placeFood(new Food());
		Debug.Log ("food placed.");
		return cell.food;
	}

	public static Cell getRandomCell(){
		int i = rand.Next(0, width );
		int j = rand.Next(0, height);
		return grid.cells[getIndex(i, j)];
	}

	public static int getWidth(){
		return width;
	}
	public static int getHeight(){
		return height;
	}

    public static Cell getCell(Vector3 position){
        return grid.cells[getIndex(position)];
    }

    public static Vector3 getPosition(Vector3 position){
        Vector3 pos = new Vector3((int) position.x, (int) position.y, (int) position.z);
        return pos;
    }

    public static int getIndex(Vector3 position){
        return getIndex((int) position.x, (int) position.y);
    }
    public static int getIndex(int x, int y){
        return (width * x) + y;
    }

	public static bool isInside (int i, int j){
		return i >= 0 && i < width && j >= 0 && j < height;
	}

    public static Cell[] getNeighbors(Cell cell){
		if (cell == null)
			return null;
		Cell[] neighbors = new Cell[6]; // up to 6 neighboring cells
		int row = (int) cell.position().x;
		int col = (int) cell.position().y;
		int c = 0;        
        for (int i=-1; i<=1; i++){
            for (int j=-1; j<=1; j++){
				if(isInside(row+i, col+j)){
					int index = getIndex(row+i, col+j);
					neighbors[c++] = grid.cells[index];
				}
            }
        }
		return neighbors;

    }
}
