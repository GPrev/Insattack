#include "MapGenerator.h"

Map& MapGenerator::computeFoo()
{
	return m_map;
}

MapGenerator* MapGenerator_new()
{
	return new MapGenerator();
}

void MapGenerator_delete(MapGenerator* MapGenerator)
{
	delete MapGenerator;
}

Map& MapGenerator_computeMapGenerator(MapGenerator* MapGenerator)
{
	return MapGenerator->computeFoo();
}