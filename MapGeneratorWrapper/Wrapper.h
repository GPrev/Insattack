#ifndef __WRAPPER__
#define __WRAPPER__
#include "../MapGenerator/MapGenerator.h"
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
		MapDataModel::MapData^ makeMap(int size, int nbPlayers, int nbAmphiTiles, int nbTDTiles, int nbInfoTiles) { return translate(MapGenerator_makeMap(MapGenerator, size, nbPlayers, nbAmphiTiles, nbTDTiles, nbInfoTiles)); }
	};
}
#endif