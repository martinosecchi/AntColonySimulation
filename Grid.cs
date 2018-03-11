using UnityEngine;
using System;
using System.Collections.Generic;

public class Grid{
	internal class InternalGrid {
		public Cell[] cells;
		public InternalGrid(){
			cells = new Cell[Grid.width * Grid.height];
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++) {
					GameObject o = new GameObject("cell "+i+j);
					o.transform.localScale += new Vector3(5f, 5f, 0);
					SpriteRenderer sr = o.AddComponent<SpriteRenderer>();
					sr.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
					o.transform.position = new Vector3(i, j, 0);
					Cell c = o.AddComponent<Cell>();
					cells[getIndex(i,j)] = c;
				}
			}
		}
	}
	public static Cell colonyCell = null; 

	private static int width = Colony.width;
	private static int height = Colony.height;
	private static InternalGrid grid = null;
	private static System.Random rand;

	public Grid(){
		if (Grid.grid == null){
			Grid.grid = new InternalGrid();
		}
		Grid.rand = new System.Random();
	}

	public static void assignColony (GameObject colony){
		colonyCell = getRandomCell();
		colonyCell.setAsColony(colony);
	}

	public static Food createFood(){
		Cell cell = getRandomCell();
		while (cell.isColony() || cell.hasFood()){
			cell = getRandomCell();
		}
		cell.placeFood(new Food());
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
	public static int numCells(){
		return width * height;
	}	

	public static double Distance (Cell current, Cell next){
		return (double) Vector3.Distance(current.position(),next.position());
	}

    public static Cell getCell(Vector3 position){
        return grid.cells[getIndex(position)];
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

	public static void evaporateCells(){
		foreach (Cell c in grid.cells){
			c.pheromone.evaporate();
		}
	}

    public static Cell[] getNeighbors(Cell cell){
		if (cell == null)
			return null;
		Cell[] neighbors = new Cell[9];
		int row = (int) cell.position().x;
		int col = (int) cell.position().y;
		int c = 0;
        for (int i=-1; i<=1; i++){
            for (int j=-1; j<=1; j++){
				neighbors[c] = null;
				if(isInside(row+i, col+j))
					neighbors[c] = grid.cells[getIndex(row+i, col+j)];
				c++;
			}
        }
		return neighbors;
    }
}
