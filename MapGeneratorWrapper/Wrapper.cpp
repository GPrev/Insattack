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

	std::vector<SquareInfo> WrapperMapGenerator::translate(System::Collections::Generic::List<Tuple<MapDataModel::Tile^, bool, int>^>^ data)
	{
		std::vector<SquareInfo> res;
		for (int i = 0; i < data->Count; ++i)
		{
			int tile = -1;
			if (data->default[i]->Item1 == MapDataModel::TileFactory::Instance->OutdoorTile)
				tile = OUTDOOR;
			if (data->default[i]->Item1 == MapDataModel::TileFactory::Instance->AmphiTile)
				tile = AMPHI;
			if (data->default[i]->Item1 == MapDataModel::TileFactory::Instance->InfoTile)
				tile = INFO;
			if (data->default[i]->Item1 == MapDataModel::TileFactory::Instance->TdTile)
				tile = TD;
			
			res.push_back(SquareInfo(tile, data->default[i]->Item2, data->default[i]->Item3));
		}
		return res;
	}

	System::Collections::Generic::List<int>^ WrapperMapGenerator::translate(std::vector<int>& v)
	{
		System::Collections::Generic::List<int>^ res;
		for (int i : v)
			res->Add(i);
		return res;
	}
}