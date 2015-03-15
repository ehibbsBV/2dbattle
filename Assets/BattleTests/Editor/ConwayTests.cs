using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Acrocatic.Scripts.Environments.Generation;
using System;

[TestFixture]
public class ConwayTests
{
	[Test]
	public void testNeighborCount ()
	{
		CellStates[,] grid = new CellStates[,]{
		
			{CellStates.ALIVE, CellStates.ALIVE, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.ALIVE, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.ALIVE, CellStates.ALIVE}
		};
		
		Conway con = new Conway (3, 3, 3, 3, 3);
		con.CellGrid = grid;
		
		Assert.True (con.GetNumLiveNeighbors (1, 1) == 8); 
	}
	
	[Test]
	public void testIterations ()
	{
		
		CellStates[,] grid = new CellStates[,]{
			
			{CellStates.ALIVE, CellStates.DEAD, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.DEAD, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.ALIVE, CellStates.ALIVE}
		};
		
		string[,] expectedNextGrid = new string[,]{
			{ "o", "o", "o" },
			{ "x", "x", "x" },
			{ "o", "x", "o" }
		};
		
		Conway con = new Conway (3, 6, 8, 3, 8);
		con.CellGrid = grid;
		
		con.NextIteration ();
		
		CellStates[,] nextGrid = con.CellGrid;
		
		
		
		Assert.True (doesGridMatchStringGrid (nextGrid, expectedNextGrid));
		
	}
	
	[Test]
	public void testNextStepDowsntFillTheGrid ()
	{
		Conway conway = new Conway (10, 6, 8, 3, 8, 10);
		conway.NextIteration ();
		Console.WriteLine (conway.ToDebugString ());
		
		int numDeadCells = 0;
		
		for (int col = 0; col < 10; col++) {
			for (int row = 0; row < 10; row++) {
				if (conway.CellGrid [col, row] == CellStates.DEAD)
					numDeadCells++;
			}
		}
		
		Assert.True (numDeadCells > 0);
	}
	
	[Test]
	public void testGridMatching ()
	{
	
		CellStates[,] gridA = new CellStates[,]{
			
			{CellStates.ALIVE, CellStates.DEAD, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.DEAD, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.ALIVE, CellStates.ALIVE}
		};	
		
		CellStates[,] gridB = new CellStates[,]{
			
			{CellStates.ALIVE, CellStates.DEAD, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.DEAD, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.ALIVE, CellStates.ALIVE}
		};
		
		Assert.True (doGridsMatch (gridA, gridB));
	
	}
	
	[Test]
	public void testStringMatching ()
	{
		Assert.True (doesCellStateMatchString (CellStates.ALIVE, "X"));
		Assert.True (doesCellStateMatchString (CellStates.DEAD, "O"));
		
		CellStates[,] gridA = new CellStates[,]{
			
			{CellStates.ALIVE, CellStates.DEAD, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.DEAD, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.ALIVE, CellStates.ALIVE}
		};	
		
		string[,] tooShort = new string[,]{
			{"X", "O"},
			{"x", "o"}	
		};
		
		string[,] badContent = new string[,]{
			{"X", "O", "X"},
			{"x", "x", "x"},
			{"X", "X", "X"}
		};
		
		string[,] shouldMatch = new string[,]{
			{"X", "O", "X"},
			{"x", "o", "x"},
			{"X", "X", "X"}
		};
		
		// false on length
		Assert.False (doesGridMatchStringGrid (gridA, tooShort));
		
		// false on content
		Assert.False (doesGridMatchStringGrid (gridA, badContent));
		
		// true
		Assert.True (doesGridMatchStringGrid (gridA, shouldMatch));
	}
	
	[Test]
	public void testGridFromStringArray ()
	{
		string[,] inputGrid = new string[,]{
			{"X", "O", "X"},
			{"x", "o", "x"},
			{"X", "X", "X"}
		};
		
		CellStates[,] expectedOutput = new CellStates[,]{
			
			{CellStates.ALIVE, CellStates.DEAD, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.DEAD, CellStates.ALIVE},
			{CellStates.ALIVE, CellStates.ALIVE, CellStates.ALIVE}
		};
		
		Assert.True (doGridsMatch (expectedOutput, getGridFromStringArray (inputGrid)));
		
	}
	
	private CellStates[,] getGridFromStringArray (string[,] stringArray)
	{
		CellStates[,] grid = new CellStates[stringArray.GetLength (0), stringArray.GetLength (0)];
		
		for (int row = 0; row < stringArray.GetLength(0); row++) {
			for (int col = 0; col < stringArray.GetLength(0); col++) {
				grid [col, row] = stringArray [col, row].ToLower ().Equals ("x") ? CellStates.ALIVE : CellStates.DEAD;
			}
		}
		
		return grid;
	}
	
	
	private bool doGridsMatch (CellStates[,] gridA, CellStates[,] gridB)
	{
		if (gridA.GetLength (0) != gridB.GetLength (0)) {
			return false;
		}
		
		for (int row = 0; row < gridA.GetLength(0); row++) {
			for (int col = 0; col < gridA.GetLength(0); col++) {
				if (gridA [col, row] != gridB [col, row]) {
					return false;
				}
			}
		}
		return true;
	}
	
	private bool doesGridMatchStringGrid (CellStates[,] stateGrid, string[,] stringGrid)
	{
		if (stateGrid.GetLength (0) != stringGrid.GetLength (0)) {
			return false;
		}
		
		for (int row = 0; row < stateGrid.GetLength(0); row++) {
			for (int col = 0; col < stateGrid.GetLength(0); col++) {
				if (!doesCellStateMatchString (stateGrid [col, row], stringGrid [col, row])) {
					return false;
				}
			}
		}
		return true;
	}
	
	/*
	* ALIVE == 'X'
	* DEAD == 'O'
	*/
	private bool doesCellStateMatchString (CellStates state, string stateString)
	{
		if (state == CellStates.ALIVE) {
			return stateString.ToLower ().Equals ("x") ? true : false;
		} else {
			return stateString.ToLower ().Equals ("o") ? true : false;
		}
		
	}
}
