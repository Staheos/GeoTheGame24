
#ifndef GEO_PACKET_HANDLER
#define GEO_PACKET_HANDLER

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <iostream>

#include <list>
#include <thread>
#include <winsock2.h>
#pragma comment(lib, "Ws2_32.lib")

#include "PacketMacros.h"
#include "Vector3.h"

class PacketHandler
{
private:
	SOCKET _serverSocket;
	sockaddr_in _service;
	SOCKET _acceptSocket;

	std::list<PositionPacket*> queue_positionPacket;

public:
	PacketHandler();
	PacketHandler(const char* address, u_short port);
	~PacketHandler();

	void Listen();

	static void ClientHandler(SOCKET clientSocket);
};

#endif