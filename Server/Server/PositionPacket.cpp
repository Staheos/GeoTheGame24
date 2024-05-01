#include "PositionPacket.h"

PositionPacket::PositionPacket(char* data)
{
	this->playerID = *((int*)data);
	this->position = Vector3(data + 4);
}

PositionPacket::PositionPacket(Vector3 position, int playerID)
{
	this->position = position;
	this->playerID = playerID;
}

void PositionPacket::Encode()
{
}
