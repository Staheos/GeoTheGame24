#include "PacketHandler.h"

PacketHandler::PacketHandler() = default;

PacketHandler::PacketHandler(const char* address, u_short port)
{
	this->queue_positionPacket = std::list<PositionPacket*>();

	// Initialize WSA variables
	WSADATA wsaData;
	int wsaerr;
	WORD wVersionRequested = MAKEWORD(2, 2);
	wsaerr = WSAStartup(wVersionRequested, &wsaData);

	// Check for initialization success
	if (wsaerr != 0)
	{
		std::cout << "The Winsock dll not found!" << std::endl;
		exit(0);
	}
	else
	{
		std::cout << "The Winsock dll found" << std::endl;
		std::cout << "The status: " << wsaData.szSystemStatus << std::endl;
	}

	// Continue with the server setup...
	// (See the full code here: https://github.com/Tharun8951/cpp-tcp-server/")

	// Create a socket
	this->_serverSocket = INVALID_SOCKET;
	this->_serverSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	// Check for socket creation success
	if (this->_serverSocket == INVALID_SOCKET)
	{
		std::cout << "Error at socket(): " << WSAGetLastError() << std::endl;
		WSACleanup();
		exit(0);
	}
	else
	{
		std::cout << "Socket is OK!" << std::endl;
	}

	// Bind the socket to an IP address and port number
	this->_service.sin_family = AF_INET;
	this->_service.sin_addr.s_addr = inet_addr(address);  // Replace with your desired IP address
	this->_service.sin_port = htons(port);  // Choose a port number
}

PacketHandler::~PacketHandler()
{

}

void PacketHandler::Listen()
{
	// Use the bind function
	if (bind(this->_serverSocket, reinterpret_cast<SOCKADDR*>(&this->_service), sizeof(this->_service)) == SOCKET_ERROR)
	{
		std::cout << "bind() failed: " << WSAGetLastError() << std::endl;
		closesocket(this->_serverSocket);
		WSACleanup();
		exit(0);
	}
	else
	{
		std::cout << "bind() is OK!" << std::endl;
	}

	// Listen for incoming connections
	if (listen(this->_serverSocket, 1) == SOCKET_ERROR)
	{
		std::cout << "listen(): Error listening on socket: " << WSAGetLastError() << std::endl;
	}
	else
	{
		std::cout << "listen() is OK! I'm waiting for new connections..." << std::endl;
	}

	// Accept incoming connections
	this->_acceptSocket = accept(this->_serverSocket, nullptr, nullptr);
	// Check for successful connection
	if (this->_acceptSocket == INVALID_SOCKET) {
		std::cout << "accept failed: " << WSAGetLastError() << std::endl;
		WSACleanup();
		exit(-1);
	}
	else
	{
		std::cout << "accept() is OK!" << std::endl;
	}

	std::thread* player = new std::thread(&PacketHandler::ClientHandler, this->_acceptSocket);

	player->join();
}

void PacketHandler::ClientHandler(SOCKET clientSocket)
{
	// Receive data from the client
	char receiveBuffer[200];
	int rbyteCount = recv(clientSocket, receiveBuffer, 200, 0);
	if (rbyteCount < 0)
	{
		std::cout << "Server recv error: " << WSAGetLastError() << std::endl;
		exit(0);
	}
	else
	{
		std::cout << "Received data: " << receiveBuffer << std::endl;
	}

	// Send a response to the client
	char buffer[200];
	std::cout << "Enter the message: ";
	std::cin.getline(buffer, 200);
	int sbyteCount = send(clientSocket, buffer, 200, 0);
	if (sbyteCount == SOCKET_ERROR)
	{
		std::cout << "Server send error: " << WSAGetLastError() << std::endl;
		exit(-1);
	}
	else
	{
		std::cout << "Server: Sent " << sbyteCount << " bytes" << std::endl;
	}

	while (true)
	{
		int rbyteCount = recv(clientSocket, receiveBuffer, 200, 0);
		if (rbyteCount < 0)
		{
			std::cout << "Server recv error: " << WSAGetLastError() << std::endl;
			exit(0);
		}
		else
		{
			//Packet* packet;

			PacketType packetType = (PacketType)( * ((int*)receiveBuffer));
			int byteCount = *((int*)receiveBuffer + 1);
			if (packetType == PacketType::POSITION)
			{
				PositionPacket* packet = new PositionPacket((char*)receiveBuffer + 8);
				//Vector3 position = Vector3((char*)receiveBuffer + 8);
				std::cout << packet->playerID << ' ' << packet->position.x << ' ' << packet->position.y << ' ' << packet->position.z << '\n';
			}
		}
	}
}
