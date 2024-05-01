#pragma once

#include "Packet.h"
#include "Vector3.h"

class PositionPacket : Packet
{
public:
	int playerID;
	Vector3 position;

public:
	PositionPacket(char* data);
	PositionPacket(Vector3 position, int playerID);

	void Encode() override;
};

