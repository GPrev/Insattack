#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

#include "Map.h"

class DLL MapGenerator {
public:
	MapGenerator() {}
	~MapGenerator() {}
	void buildMap(int size, int nbPlayers, int nbAmphiTiles, int nbTDTiles, int nbInfoTiles, int nbRestaurantTiles); //only works between 2 and 4 players
	Map& getMap();
private:
	Map m_map;
};
// A ne pas implémenter dans le .h !
EXTERNC DLL MapGenerator* MapGenerator_new();
EXTERNC DLL void MapGenerator_delete(MapGenerator* MapGenerator);
EXTERNC DLL Map& MapGenerator_makeMap(MapGenerator* MapGenerator, int size, int nbPlayers, int nbAmphiTiles, int nbTDTiles, int nbInfoTiles, int nbRestaurantTiles);