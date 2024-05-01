
#ifndef GEO_PLAYERS_CONTAINER
#define GEO_PLAYERS_CONTAINER

#include "Player.h"
#include <list>

class PlayerContainer
{
private:
	std::list<Player*> _players;
public:
	PlayerContainer();
	~PlayerContainer();

	Player* GetPlayer(int id);
};

#endif