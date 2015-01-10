#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

#include <vector>

class SquareInfo
{
public:
	SquareInfo(int tile, bool ally = false, int enemyHP = 0) : m_tile(tile), m_ally(ally), m_enemyHP(enemyHP) {};
	bool isEnemy() { return m_enemyHP > 0; };
	bool isFree() { return !m_ally && !isEnemy(); };
	int m_tile;
	bool m_ally;
	int m_enemyHP;
};

class DLL HintGiver
{
public:
	std::vector<int> giveHint(std::vector<SquareInfo>& choices);
};
// A ne pas implémenter dans le .h !
EXTERNC DLL HintGiver* HintGiver_new();
EXTERNC DLL void HintGiver_delete(HintGiver* HintGiver);
EXTERNC DLL std::vector<int> HintGiver_giveHint(HintGiver* HintGiver, std::vector<SquareInfo>& choices);

