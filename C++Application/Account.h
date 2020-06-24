#pragma once

#include <vector>
#include <string>
#include "Transaction.h"

class Account
{
private:
	int balance;
	std::vector<Transaction> log;
public:
	Account();
	std::vector<std:: string> report();
	bool deposit(int amt);
	bool withdraw(int amt);
	int getBalance() { return balance; }
};
