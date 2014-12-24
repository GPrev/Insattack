#include "MapGenerator.h"


MapGenerator* MapGenerator_new()
{
	return new MapGenerator();
}

void MapGenerator_delete(MapGenerator* MapGenerator)
{
	delete MapGenerator;
}

Map& MapGenerator_makeMap(MapGenerator* MapGenerator, int size, int nbPlayers, int nbAmphiTiles, int nbTDTiles, int nbInfoTiles)
{
	MapGenerator->buildMap(size, nbPlayers, nbAmphiTiles, nbTDTiles, nbInfoTiles);
	return MapGenerator->getMap();
}

void MapGenerator::buildMap(int size, int nbPlayers, int nbAmphiTiles, int nbTDTiles, int nbInfoTiles)
{
	m_map.init(size, nbPlayers, nbAmphiTiles, nbTDTiles, nbInfoTiles);

	//Generating tiles
	int remainingTot = size*size;
	int nbOutdoorTiles = remainingTot - (nbAmphiTiles + nbInfoTiles + nbTDTiles);
	std::map<int, int> remaining;
	remaining[OUTDOOR] = nbOutdoorTiles;
	remaining[AMPHI] = nbAmphiTiles;
	remaining[TD] = nbTDTiles;
	remaining[INFO] = nbInfoTiles;
	for (int i = 0; i < size; ++i)
	{
		for (int j = 0; j < size; ++j)
		{
			int tile;

			int rng = rand() % remainingTot;
			if (rng < remaining[OUTDOOR])
				tile = OUTDOOR;
			else
			{
				rng -= remaining[OUTDOOR];
				if (rng < remaining[AMPHI])
					tile = AMPHI;
				else
				{
					rng -= remaining[AMPHI];
					if (rng < remaining[TD])
						tile = TD;
					else
						tile = INFO;
				}
			}

			remainingTot--;
			remaining[tile]--;
			m_map[i][j] = tile;
		}
	}

	//Generating starting positions
	m_map.addStartingPos(std::pair<int, int>(DIST_FROM_EDGES, DIST_FROM_EDGES));
	m_map.addStartingPos(std::pair<int, int>(m_map.getSize() - DIST_FROM_EDGES, m_map.getSize() - DIST_FROM_EDGES));
	if (nbPlayers > 2)
	{
		m_map.addStartingPos(std::pair<int, int>(DIST_FROM_EDGES, m_map.getSize() - DIST_FROM_EDGES));
		if (nbPlayers > 3)
			m_map.addStartingPos(std::pair<int, int>(m_map.getSize() - DIST_FROM_EDGES, DIST_FROM_EDGES));
	}
}

Map& MapGenerator::getMap()
{
	return m_map;
}