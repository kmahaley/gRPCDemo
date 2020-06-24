#include "Account.h"
#include "Transaction.h"

using namespace std;

Account::Account() : balance(0) {}

vector<string> Account::report() {
	vector<string> list;
	list.push_back("balance is " + to_string(balance));
	list.push_back("transactions: -----");
	for (auto t : log)
	{
		list.push_back(t.report());

	}
	list.push_back("-----");
	return list;
}

bool Account::deposit(int amt) {
	
	if (amt > 0) {
		balance = balance + amt;
		log.push_back(Transaction(amt, "deposit"));
	}
	else
	{
		return false;
	}
}

bool Account::withdraw(int amt) {
	if (amt > 0) {
		if (balance >= amt) {
			balance = balance - amt;
			log.push_back(Transaction(amt, "withdraw"));
			return true;
		}
		return false;
	}
	else
	{
		return false;
	}
}

//int Account::getBalance() {
//	return balance;
//}