#pragma once

#include "SudokuGraph.h"
#include <vector>

class Solver
{
private:
	static bool isValid(SudokuGraph& graph, int vertex_id, int num);
public:
	static bool solve(SudokuGraph& graph);
};