#ifndef __WRAPPER__
#define __WRAPPER__
#include "../MapGenerator/MapGenerator.h"
#include "../MapGenerator/HintGiver.h"
//#using <MapDataModel/bin/Debug/MapDataModel.dll>
// Le bon chemin pour être "../Debug/nomProjetLib.lib"
#pragma comment(lib, "../INSAttack/Debug/MapGenerator.lib")
using namespace System;
namespace Wrapper {
	public ref class WrapperMapGenerator {
	private:
		MapGenerator* MapGenerator;

		MapDataModel::MapData^ translate(Map& m);
	public:
		WrapperMapGenerator() { MapGenerator = MapGenerator_new(); }
		~WrapperMapGenerator() { MapGenerator_delete(MapGenerator); }
		MapDataModel::MapData^ makeMap(int size, int nbPlayers, int nbAmphiTiles, int nbTDTiles, int nbInfoTiles, int nbRestaurantTile) { return translate(MapGenerator_makeMap(MapGenerator, size, nbPlayers, nbAmphiTiles, nbTDTiles, nbInfoTiles, nbRestaurantTile)); }
	};

	public ref class WrapperHintGiver {
	private:
		HintGiver* HintGiver;

		std::vector<SquareInfo> translate(System::Collections::Generic::List<Tuple<MapDataModel::Tile^, bool, int>^>^ data);
		System::Collections::Generic::List<int>^ translate(std::vector<int>& v);
	public:
		WrapperHintGiver() { HintGiver = HintGiver_new(); }
		~WrapperHintGiver() { HintGiver_delete(HintGiver); }
		System::Collections::Generic::List<int>^ giveHint(System::Collections::Generic::List<Tuple<MapDataModel::Tile^, bool, int>^>^ data) { return translate(HintGiver_giveHint(HintGiver, translate(data))); }
	};
}
#endif