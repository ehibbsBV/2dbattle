namespace Acrocatic.Scripts.Environments.Generation
{
	public class Conway
	{

		public CellStates[,] CellGrid {
			get{ return cellGrid; }
			set{ cellGrid = value; }
		}
		// a live cell can only live with 2 or 3 neighbors
		// a dead cell can only come to life with 3 neighbors
		private CellStates[,] cellGrid;
		private int gridSize;
		private int bTop, bBot, sTop, sBot;
    
		public Conway (int size, int bornBot, int bornTop, int surviveBot, int surviveTop)
		{
			gridSize = size;
			bBot = bornBot;
			bTop = bornTop;
			sBot = surviveBot;
			sTop = surviveTop;
			cellGrid = GenerationUtils.GenerateSeed (gridSize, 10);
		}
		
		public Conway (int size, int bornBot, int bornTop, int surviveBot, int surviveTop, int percentageOfWalls)
		{
			gridSize = size;
			bBot = bornBot;
			bTop = bornTop;
			sTop = surviveTop;
			sBot = surviveBot;
			cellGrid = GenerationUtils.GenerateSeed (gridSize, percentageOfWalls);
		}
		
		/**
		run the next iteration of the simulation
		*/
		public void NextIteration ()
		{
			CellStates[,] nextGrid = new CellStates[gridSize, gridSize];
			
			for (int col = 0; col < gridSize; col++) {
				for (int row = 0; row < gridSize; row++) {
					nextGrid [col, row] = EvaluateCell (col, row);
				}
			}
			
			cellGrid = nextGrid;
		}
		
		/**
    evaluate whether a cell should live or die
     */
		private CellStates EvaluateCell (int xPos, int yPos)
		{

			var numLiveNeighbors = GetNumLiveNeighbors (xPos, yPos);

			var state = cellGrid [xPos, yPos];

			// cell is currently alive
			if (state == CellStates.ALIVE) {
				if (numLiveNeighbors >= sBot && numLiveNeighbors <= sTop) {
					return CellStates.ALIVE;
				}
			}
      		// cell is currently dead
      		else {
				if (numLiveNeighbors >= bBot && numLiveNeighbors <= bTop) {
					return CellStates.ALIVE;
				}
			}
			return CellStates.DEAD;
		}

		public string ToDebugString ()
		{
			string output = "";
			for (int row = 0; row < gridSize; row++) {
				for (int col = 0; col < gridSize; col++) {
					if (cellGrid [col, row] == CellStates.ALIVE) {
						output += "X";
					} else {
						output += "O";
					}
					if (col < gridSize - 1) {
						output += " ";
					}
				}
				output += "\n";	
			}
			
			return output;
		}

		// Visible For Testing
		public int GetNumLiveNeighbors (int xPos, int yPos)
		{

			var numLiveNeighbors = 0;

			// look at 8 neighbors
      
			
			if (yPos > 0) {
				// up 
				if (cellGrid [xPos, yPos - 1] == CellStates.ALIVE) {
					numLiveNeighbors++;
				}
        
				// up-left    
				if (xPos > 0) {
					if (cellGrid [xPos - 1, yPos - 1] == CellStates.ALIVE) {
						numLiveNeighbors++;
					}
				}  
      	
				// up-right
				if (xPos < gridSize - 1) {
					if (cellGrid [xPos + 1, yPos - 1] == CellStates.ALIVE) {
						numLiveNeighbors++;
					}
				}
			}
			
			// down
			if (yPos < gridSize - 1) {
				if (cellGrid [xPos, yPos + 1] == CellStates.ALIVE)
					numLiveNeighbors++;
          
				// down-left
				if (xPos > 0) {
					if (cellGrid [xPos - 1, yPos + 1] == CellStates.ALIVE) {
						numLiveNeighbors++;
					}
				}
          
				// down right
				if (xPos < gridSize - 1) {
					if (cellGrid [xPos + 1, yPos + 1] == CellStates.ALIVE)
						numLiveNeighbors++;
				}
			}
			// left
			if (xPos > 0) {
				if (cellGrid [xPos - 1, yPos] == CellStates.ALIVE)
					numLiveNeighbors++;
			}
			
			// right
			if (xPos < gridSize - 1) {
				if (cellGrid [xPos + 1, yPos] == CellStates.ALIVE)
					numLiveNeighbors++;
			}

			return numLiveNeighbors;
		}

	}

	public enum CellStates
	{
		DEAD,
		ALIVE
	}
}