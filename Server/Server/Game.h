
#ifndef GEO_GAME
#define GEO_GAME

#include "PacketHandler.h"
#include "PlayerContainer.h"

class Game
{
private:
	PacketHandler _packetHandler;
	PlayerContainer _playerContainer;
public:
	Game();
	~Game();

	void Start();
};

#endif 