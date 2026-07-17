#include "SudokuGraph.h"
#include "Solver.h"

#define API __declspec(dllexport)

extern "C"
{
	API SudokuGraph* CreateSudokuGraph()
	{
		return new SudokuGraph();
	}

	API void DisposeSudokuGraph(SudokuGraph* graph)
	{
		if (graph) delete graph;
	}

	API void InsertValue(SudokuGraph* graph, int id, int value)
	{
		if (graph) graph->insertValue(id, value);
	}

	API bool SolveSodokuGraph(SudokuGraph* graph)
	{
		if (graph) return Solver::solve(*graph);
	}
}