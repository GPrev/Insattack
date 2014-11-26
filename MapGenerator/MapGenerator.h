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
	Map& computeFoo();
private:
	Map m_map;
};
// A ne pas implémenter dans le .h !
EXTERNC DLL MapGenerator* MapGenerator_new();
EXTERNC DLL void MapGenerator_delete(MapGenerator* MapGenerator);
EXTERNC DLL Map& MapGenerator_computeMapGenerator(MapGenerator* MapGenerator);