#include "Transaction.h"
using namespace std;
Transaction::Transaction(int val, std::string kind) : amt(val), type(kind) {
}


string Transaction::report() {
	string report;
	report = report + " " + to_string(amt) + " " + type;

	return report;
}