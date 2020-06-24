#pragma once
#include <string>

class Transaction
{
private:
	int amt;
	std::string type;

public:
	Transaction(int val, std:: string kind);
	std::string report();

};

