#include "HintGiver.h"

std::vector<int>& HintGiver::giveHint(std::vector<SquareInfo>& choices)
{
	m_hints.clear();

	//suggests free tiles (free tile = free point)
	for (unsigned int i = 0; i < choices.size(); ++i)
	{
		if (choices[i].isFree()) //if the tile is free, suggest it
		{
			m_hints.push_back(i);
			if (m_hints.size() == 3) //if the max nb o tiles to suggest was reached end there
				return m_hints;
		}
	}

	//suggests the weakest enemies
	int enemySquaresSuggested = 0;
	int weakerEnemy = -1;
	do
	{
		int minHP = 9001; //supposed larger than anything
		weakerEnemy = -1;
		//look for weaker enemy
		for (unsigned int i = 0; i < choices.size(); ++i)
		{
			if (choices[i].isEnemy() && choices[i].m_enemyHP < minHP) //new weakest enemy
			{
				if (std::find(m_hints.begin(), m_hints.end(), i) == m_hints.end()) //if the tile is not suggested yet
				{
					weakerEnemy = i;
					minHP = choices[i].m_enemyHP;
				}
			}
		}
		//suggests weaker enemy
		if (weakerEnemy != -1)
		{
			m_hints.push_back(weakerEnemy);
			if (m_hints.size() == 3) //if the max nb o tiles to suggest was reached end there
				return m_hints;
		}
	} while (weakerEnemy != -1); //while we can find an enemy that was not suggested yet

	return m_hints;
}

HintGiver* HintGiver_new()
{
	return new HintGiver();
}

void HintGiver_delete(HintGiver* HintGiver)
{
	delete HintGiver;
}

std::vector<int> HintGiver_giveHint(HintGiver* HintGiver, std::vector<SquareInfo>& choices)
{
	return HintGiver->giveHint(choices);
}