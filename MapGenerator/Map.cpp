#include "Map.h"

Map::Map() : m_tiles(), m_startingPos()
{
}

Map::~Map()
{
}

void Map::init(int size)
{
	m_tiles.clear();
	for (int i = 0; i < size; ++i)
	{
		m_tiles.push_back(std::vector<int>());
		for (int j = 0; j < size; ++j)
		{
			m_tiles[i].push_back(0);
		}
	}

	m_startingPos.clear();
}