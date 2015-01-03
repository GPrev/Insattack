#pragma once

#include <vector>
#include <map>

#define DIST_FROM_EDGES 1

#define OUTDOOR 0
#define AMPHI 1
#define TD 2
#define INFO 3

class Map
{
public:
	Map();
	~Map();
	void init(int size, int nbPlayers, int nbAmphiTiles, int nbTDTiles, int nbInfoTiles);
	inline int getSize() { return m_tiles.size(); };
	inline std::vector<std::pair<int, int>> getStartingPos() { return m_startingPos; };
	inline void addStartingPos(int x, int y) { addStartingPos(std::pair<int, int>(x, y)); }
	inline void addStartingPos(std::pair<int, int> pos) { m_startingPos.push_back(pos); }

	inline std::vector<int>& operator[] (const int x) { return m_tiles[x]; }

private:
	std::vector<std::vector<int>> m_tiles;
	std::vector<std::pair<int, int>> m_startingPos;

};

