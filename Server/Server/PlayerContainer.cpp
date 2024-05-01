#include "PlayerContainer.h"

PlayerContainer::PlayerContainer()
{
	this->_players = std::list<Player*>();
}

PlayerContainer::~PlayerContainer()
{
}

Player* PlayerContainer::GetPlayer(int id)
{
	if (this->_players.size() == 0)
	{
		return nullptr;
	}
	auto it = this->_players.begin();
	while (it != this->_players.end())
	{
		if (id == (*it)->id)
		{
			return *it;
		}
		it++;
	}
	return nullptr;
}
