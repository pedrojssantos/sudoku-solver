#include "Solver.h"
#include "SudokuGraph.h"

bool Solver::solve(SudokuGraph& graph)
{
    auto& vers = graph.getVertices();

    for (int i = 0; i < 81; ++i)
    {
        if (vers[i]._value == 0)
        {
            for (int atp = 1; atp <= 9; ++atp)
            {
                if (isValid(graph, i, atp))
                {
                    graph.insertValue(i, atp);

                    if (solve(graph)) return true;

                    graph.insertValue(i, 0);
                }
            }

            return false;
        }
    }

    return true;
}

bool Solver::isValid(SudokuGraph& graph, int vertex_id, int num)
{
    auto& vers = graph.getVertices();
    auto& adj_ls = graph.getAdjLists();

    for (int i = 0; i < 20; ++i)
    {
        if (vers[adj_ls[vertex_id][i]]._value == num)
        {
            return false;
        }
    }

    return true;
}