/*
 Index.cpp : This file contains the 'main' function. Program execution begins and ends there.

 The main aim of this program is to prepare a simple index on a text file.
 This program generates an executable Index.exe.
 This executable can be run from the command prompt in one of the below ways:
	Index.exe -c <input_data_file> <output_index_file> <key_length>
	Index.exe -l <input_index_file> <input_data_file> <key_length>

 the value of key_length decides the number of first "key_length" characters to be treated as index.
 Using the second command (i.e. '-l') displays the output on the command prompt and also createsa file
 named "indexed_output.txt" which contains the data in the required sorted order.
*/


#include "pch.h"
#include <iostream>
#include <fstream>
#include <sys/stat.h>
#include <vector>
#include <string>
#include <bitset>
#include <algorithm>
#include <sstream>

using namespace std;

//typedef struct
//{
//	string index;
//	string data;
//}data_struct;

//structure to store the index and data-position
struct index_struct
{
	string index;
	string dataPos;
	unsigned long dataPos2;

	index_struct(const std::string& i, const std::string& d, unsigned long d2) : index(i), dataPos(d), dataPos2(d2)
	{

	}

	index_struct() : index("4"), dataPos("0"), dataPos2(0)
	{

	}

	bool operator < (const index_struct& instance) const
	{
		return (index < instance.index);
	}
};

//a list to store the index and the data-position before sorting it and writing it to the file
vector<index_struct> indexRec;

//accepts commandline arguments. 
//'noOfArguments' indicates the number of arguments input by the user
// arguments[] contains the arguments
int main(int noOfArguments, char* arguments[])
{
	if (noOfArguments != 5)
	{
		printf("Invalid commandline arguments\n");
		printf("Accepted arguments need to in one of the below formats:\n");
		printf("\t1. Index.exe -c <input_file> <output_file> <key_length>\n");
		printf("\t2. Index.exe -l <index_file> <data_file> <key_length>\n");
		//getchar();
		return 0;
	}

	int index_length = atoi(arguments[4]);
	unsigned long dataptr = 0;
	string tempRecord;

	//checks if the action requested is creating a index or listing based on the index
	if (strcmp(arguments[1], "-c") == 0)
	{
		ifstream inpFile(arguments[2], ios::in | ios::binary);		//data
		ofstream outFile(arguments[3], ios::out | ios::binary);		//index file to be created

		while (getline(inpFile, tempRecord))
		{
			string a = tempRecord.substr(0, index_length);
			string b = std::bitset<32>(dataptr).to_string();

			indexRec.push_back(index_struct(tempRecord.substr(0, index_length), b, dataptr));
			dataptr = inpFile.tellg();
		}

		sort(indexRec.begin(), indexRec.end());


		for (std::vector<index_struct>::const_iterator it = indexRec.begin(), end = indexRec.end();
			it != end; ++it)
		{
			outFile.write(it->index.c_str(), index_length);
			outFile.write(it->dataPos.c_str(), it->dataPos.size());
			outFile.write("\n", 1);
		}

		inpFile.close();
		outFile.close();
		printf("the output file has been generated and is named %s", arguments[3]);
	}
	else if (strcmp(arguments[1], "-l") == 0)
	{


		ifstream inpFile(arguments[2], ios::in | ios::binary);	// index
		ifstream inpFile2(arguments[3], ios::in | ios::binary);	//data

		ofstream outFile("indexed_output.txt", ios::out | ios::binary);	 //file that saves output

		unsigned long temp = 0;
		while (getline(inpFile, tempRecord))
		{
			char buffer[1000];

			string a = tempRecord.substr(0, index_length);
			string b = tempRecord.substr(index_length, tempRecord.length() - index_length);

			temp = std::bitset<32>(b).to_ulong();
			inpFile2.seekg(temp, ios::beg);

			string tempRecord2;
			getline(inpFile2, tempRecord2);			

			printf("%s\n", tempRecord2.c_str());
			outFile.write(tempRecord2.c_str(), tempRecord2.length());

			temp = 0;
		}
		outFile.close();
		inpFile.close();
		inpFile2.close();
		getchar();
	}
	else
	{
		printf("Invalid commandline arguments\n");
		printf("Accepted arguments need to in one of the below formats:\n");
		printf("\t1. Index.exe -c <input_file> <output_file> <key_length>\n");
		printf("\t2. Index.exe -l <index_file> <data_file> <key_length>\n");
		//getchar();
		return 0;
	}
	//getchar();
}
