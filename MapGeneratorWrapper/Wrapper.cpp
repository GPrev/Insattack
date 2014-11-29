#include "Wrapper.h"

namespace Wrapper {
	MapDataModel::MapData^ WrapperMapGenerator::translate(Map& m)
	{
		MapDataModel::MapData^ csMap = gcnew MapDataModel::MapData();

		//size
		csMap->Size = m.getSize();

		//tiles
		//MapDataModel::TileFactory factory;
		for (int i = 0; i < m.getSize(); ++i)
		{
			for (int j = 0; j < m.getSize(); ++j)
			{
				csMap->TileTable[gcnew MapDataModel::Coord(i, j)] = MapDataModel::TileFactory::Instance->getTile(m[i][j]);
			}
		}

		//starting pos
		std::vector<std::pair<int, int>> startingPos = m.getStartingPos();
		for (std::pair<int, int> p : startingPos)
		{
			csMap->StartingPos->Add(gcnew MapDataModel::Coord(p.first, p.second));
		}
		return csMap;
	}
}