#pragma once

#include <vector>

struct Vertex
{
	int _id;
	int _value;
};

class SudokuGraph
{
private:
	std::vector<Vertex> _vertices;
	std::vector<std::vector<int>> _adj_lists;
public:
	SudokuGraph();
	bool insertValue(int id, int value);
	const std::vector<Vertex>& getVertices() const;
	const std::vector<std::vector<int>>& getAdjLists() const;
};