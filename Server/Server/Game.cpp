#include "Game.h"

Game::Game()
{
	this->_playerContainer = PlayerContainer();
	this->_packetHandler = PacketHandler("192.168.1.121", 8989);
	this->_packetHandler.Listen();
}

Game::~Game()
{
}

void Game::Start()
{

}
