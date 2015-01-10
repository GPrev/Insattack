#include "HintGiver.h"

std::vector<int> HintGiver::giveHint(std::vector<SquareInfo>& choices)
{
	std::vector<int> res;

	//suggests free tiles (free tile = free point)
	for (unsigned int i = 0; i < choices.size(); ++i)
	{
		if (choices[i].isFree()) //if the tile is free, suggest it
		{
			res.push_back(i);
			if (res.size() == 3) //if the max nb o tiles to suggest was reached end there
				return res;
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
				if (std::find(res.begin(), res.end(), i) == res.end()) //if the tile is not suggested yet
				{
					weakerEnemy = i;
					minHP = choices[i].m_enemyHP;
				}
			}
		}
		//suggests weaker enemy
		if (weakerEnemy != -1)
		{
			res.push_back(weakerEnemy);
			if (res.size() == 3) //if the max nb o tiles to suggest was reached end there
				return res;
		}
	} while (weakerEnemy != -1); //while we can find an enemy that was not suggested yet

	return res;
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