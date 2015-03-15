using Acrocatic.Scripts.Environments.Generation;
using System;

namespace Acrocatic.Scripts.Environments.Generation
{
	public class GenerationUtils
	{

		public static CellStates[,] GenerateSeed (int gridSize, int percentageToSpawnWall)
		{
			CellStates[,] grid = new CellStates[gridSize, gridSize];
			Random random = new Random ();

			for (int row = 0; row < gridSize; row++) {
				for (int col = 0; col < gridSize; col++) {
					if (random.Next (0, 100) < percentageToSpawnWall) {
						grid [col, row] = CellStates.ALIVE;
					} else {
						grid [col, row] = CellStates.DEAD;
					}
				}
			}

			return grid;
		}

	}
}