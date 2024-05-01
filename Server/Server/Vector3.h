
#ifndef GEO_VECTOR3
#define GEO_VECTOR3


class Vector3
{
public:
	float x;
	float y;
	float z;

public:
	Vector3();
	Vector3(float x, float y, float z);
	Vector3(char* bytes);
	~Vector3();

	void Encode(char* buff);
};


#endif