#include "Vector3.h"

Vector3::Vector3()
{
	this->x = 0;
	this->y = 0;
	this->z = 0;
}

Vector3::Vector3(float x, float y, float z)
{
	this->x = x;
	this->y = y;
	this->z = z;
}

Vector3::Vector3(char* bytes)
{
	this->x = * ( (int*)bytes );
	this->y = * ( (int*)bytes + 1);
	this->z = * ( (int*)bytes + 2);
}

Vector3::~Vector3()
{
}

void Vector3::Encode(char* buff)
{
	* ( (int*)buff ) = this->x;
	* ( (int*)buff + 1) = this->y;
	* ( (int*)buff + 2) = this->z;
}
