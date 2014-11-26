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
	public:
		WrapperMapGenerator() { MapGenerator = MapGenerator_new(); }
		~WrapperMapGenerator() { MapGenerator_delete(MapGenerator); }
		MapDataModel::MapData^ computeFoo() { return gcnew MapDataModel::MapData(); }//MapGenerator->computeFoo(); }
	};
}
#endif