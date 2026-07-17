#include "SudokuGraph.h"
#include <vector>

SudokuGraph::SudokuGraph()
{
    int num_vers = 81;

	_vertices.resize(num_vers);
	_adj_lists.resize(num_vers);

	for (int i = 0; i < num_vers; ++i)
	{
		_vertices[i]._id = i;
		_vertices[i]._value = 0;
	}

    for (int u = 0; u < num_vers; ++u) 
    {
        for (int v = 0; v < num_vers; ++v) 
        {
            if (u == v) continue;

            int r1 = u / 9, c1 = u % 9;
            int r2 = v / 9, c2 = v % 9;

            bool same_row = (r1 == r2);
            bool same_col = (c1 == c2);
            bool same_block = ((r1 / 3) == (r2 / 3)) && ((c1 / 3) == (c2 / 3));

            if (same_row || same_col || same_block) _adj_lists[u].push_back(v);
        }
    }
}

bool SudokuGraph::insertValue(int id, int value)
{
    if (id < 0 || id > 80) return false;
    if (value < 0 || value > 9) return false;

    _vertices[id]._value = value;

    return true;
}

const std::vector<Vertex>& SudokuGraph::getVertices() const
{
    return _vertices;
}

const std::vector<std::vector<int>>& SudokuGraph::getAdjLists() const
{
    return _adj_lists;
}