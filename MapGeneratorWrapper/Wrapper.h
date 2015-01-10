#ifndef __WRAPPER__
#define __WRAPPER__
#include "../MapGenerator/MapGenerator.h"
#include "../MapGenerator/HintGiver.h"
//#using <MapDataModel/bin/Debug/MapDataModel.dll>
// Le bon chemin pour �tre "../Debug/nomProjetLib.lib"
#pragma comment(lib, "../INSAttack/Debug/MapGenerator.lib")
using namespace System;
namespace Wrapper {
	public ref class WrapperMapGenerator {
	private:
		MapGenerator* MapGenerator;
		HintGiver* HintGiver;

		MapDataModel::MapData^ translate(Map& m);
		std::vector<SquareInfo> translate(System::Collections::Generic::List<Tuple<MapDataModel::Tile^, bool, int>^>^ data);
		System::Collections::Generic::List<int>^ translate(std::vector<int>& v);
	public:
		WrapperMapGenerator() { MapGenerator = MapGenerator_new(); HintGiver = HintGiver_new(); }
		~WrapperMapGenerator() { MapGenerator_delete(MapGenerator); HintGiver_delete(HintGiver); }
		MapDataModel::MapData^ makeMap(int size, int nbPlayers, int nbAmphiTiles, int nbTDTiles, int nbInfoTiles) { return translate(MapGenerator_makeMap(MapGenerator, size, nbPlayers, nbAmphiTiles, nbTDTiles, nbInfoTiles)); }
		System::Collections::Generic::List<int>^ giveHint(System::Collections::Generic::List<Tuple<MapDataModel::Tile^, bool, int>^>^ data) { return translate(HintGiver_giveHint(HintGiver, translate(data))); }
	};
}
#endif