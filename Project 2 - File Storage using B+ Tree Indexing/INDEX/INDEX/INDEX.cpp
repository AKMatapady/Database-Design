/*
-------------------------------------------------------------------------------------------------------------------------------
	This program implements B+ Tree indexing.
	It will read a text file containing the data and build the index, treating the first 'n' columns as the key.
	This 'n' will be specified by the user in the command-line arguments.
	'n' lies in the range [1,40]

	This program can perform the below four tasks:
	1. Creating an Index
	2. Finding a Record by its Key
	3. Inserting a new text Record
	4. Listing sequential Records

	Creating an Index
	--------------------------------
		Takes four parameters:
		A. -create
		B. data file-name to be indexed
		C. Output file-name(.indx)
		D. n - the number of first 'n' bytes to be considered as key - range from 1 to 40

		example:
		INDEX.exe -create data.txt MyIndex.indx 30

		The above command will create an index with a key length of 30 bytes from the data.txt file and save it in index.indx.

		Duplicate keys from input file will not be inserted to the output file.
		This program will list the duplicate keys and the position in the data file where the indexed record occurs.

	Finding a Record by Key
	--------------------------------
		Finds a record by its key, displays it and gives its position
		If not found, it will display "Record/Key Not found".

		Takes three parameters:
		A. -find
		B. index file-name(.indx) to be searched
		C. Key of the record

		example:
		INDEX.exe -find MyIndex.indx 6578686ABCD

		The above command would search for the record with the key "6578686ABCD".
		if found, it would display it as:
			"At <position>, record: 6578686ABCD<followed by rest of record>"
		If not found, it would display
			"Key not found"

		if key supplied is larger than the key of the b+ tree indexing, the excess bytes are truncated.
		If the key is shorter, pad it on the right with blanks

	Inserting a new Text Record
	---------------------------------
		Inserts a record into the data file and creates its index in the index file specified.

		Takes three parameters:
		A. -insert
		B. name of the index file(.indx) where the record is to be inserted
		C. Record to be inserted - enclosed in double quotes

		example:
		INDEX.exe -insert MyIndex.indx "6578686ABCD a new Record"

		For the above command, the program first looks into the index file "MyIndex.indx" and finds the key length
		from the header of the index file.
		Then it looks for the key of the new record, in the index file.
		if not found, then it writes the new record at the end of the data file and creates an index for it.
		if the key is already present, then the record is not added to the data file and the index structure,
		and an error message is displayed.

	Listing Sequential Records
	-----------------------------------
		Takes four parameters:
		A. -list
		B. index file-name(.indx)
		C. Starting key
		D. count

		Lists "count" number of records specified by the user, following the record whose key is provided.
		If the key specified was not found, the records that contain the next larger key will be shown and 
		a message will be displayed that "Given Key was not found".

-----------------------------------------------------------------------------------------------------------------------------

	Comments:
	-----------------------------------------

	Each block representing the node begins with 4 bytes indicating the number of elements in the block.
	This is followed by a char which tells us if the block represents a leaf-node(L) or not(N).

	If the block is not a leaf node, it will next begin with a left pointer and then be followed by the pair of 
	key and a right pointer. it will end with a pointer to next leaf block. If 0 then it is the last block.

	If the block is a leaf node, it will next begin with the key and then followed by the data-pointer and so on until 
	the end of the block where a pointer will indicate the position of block with next key.

-------------------------------------------------------------------------------------------------------------------------------
	Project Details:
	------------------------------------------

	Start date of this project: 10th Nov 2018
	NetID: axm180029

	This program was written by Amith Kumar Matapady as a part of the course "CS6360.004 Database Design" fall 2018
	taught by Prof. John Cole

------------------------------------------------------------------------------------------------------------------------------

*/

// INDEX.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <string>
#include <fstream>
#include <conio.h>
#include "INDEX.h"

#define BLOCK_SIZE			 1024
#define MAX_KEY_LENGTH		 40

using namespace std;

// structure to store the header data;
// can have a max size of 1024 Bytes
// current size: 256 + 4 + 8 = 268 Bytes
struct Index_Meta
{
	char FileName[256];
	int keyLength;
	unsigned long long RootNodePosition;
};

//structure used to return a new intermediate node to be added to the parent node
struct NodeElement
{
	unsigned long long PrevPointer;		//redundant - but  used for simplicity
	string Key;
	unsigned long long NextPointer;

	NodeElement() : PrevPointer(0), Key(""), NextPointer(0)
	{

	}

};

int MaxNoOfKeysInALeafBlock = 0;
int MaxNoOfKeysInANonLeafBlock = 0;

//functions
void CreateIndex(string dataFileName, string indexFileName, int keyLength);
void FindRecord(string indexFileName, string keyOfRecord);
string GetALargeRecordFromDataFile(string DataFileName, unsigned long long DataPointer);
string GetARecordFromDataFile(string DataFileName, unsigned long long DataPointer);
NodeElement InsertKey(string DataFileName, string indexFileName, unsigned long long blockPointer, string key, string recordToBeInserted, unsigned long long &pointer, unsigned long long dataLoc = 0);
void InsertRecord(string indexFileName, string recordToBeInserted, unsigned long long dataLoc = 0, int keyLength = 0);
void ListNRecords(string indexFileName, unsigned long long NodeStartingPointer, int currentElementNo, int count, int keyLength, string DataFileName);
void ListRecord(string indexFileName, string startingKey, int noOfRecordsToList);
void ReadIndexHeader(std::string &indexFileName, std::string &DataFileName, unsigned long long &RootNodePos, int &keyLength);
void SplitFullLeafNode(string indexFileName, char  Tempbuf2[1024], char &BlockT, char  TempBuf[1024], std::string &key, NodeElement &ReturnStruct, unsigned long long blockPointer);
void SplitFullNonLeafNode(string indexFileName, char Tempbuf2[1024], char &BlockT, char  TempBuf[1024], std::string &key, NodeElement &ReturnStruct, unsigned long long blockPointer);
void UpdateIndexHeader(std::string indexFileName, std::string &DataFileName, int &keyLength, unsigned long long &RootNodePos);
void WriteFirstElementInNonLeafBlock(std::string indexFileName, NodeElement &newElement, unsigned long long seekPointer);
void WriteFirstEntryInLeafBlock(std::string indexFileName, std::string &key, unsigned long long &dataLoc, unsigned long long seekPointer);


//get a record from the given data file and return it.
//The record follows below assumptions
//Assumption 1: Record is terminated by '\n'
//Assumption 2: Record has a max length of 1024 Bytes
string GetARecordFromDataFile(string DataFileName, unsigned long long DataPointer)
{
	
	char TempRecord[1024];		
	fstream DataFile(DataFileName.c_str(), ios::in | ios::out | ios::binary);
	DataFile.seekg(DataPointer);

	DataFile.read(TempRecord, 1024);		//assume the record has a max length of 1024
	int pos = 0;
	while (TempRecord[pos] != '\n')
		pos++;

	TempRecord[pos + 1] = '\0';
	string ReturnString(TempRecord);

	DataFile.close();
	return ReturnString;
}

//get a record from the given data file and return it.
//The record follows below assumptions
//Assumption: Record is terminated by '\n'
string GetALargeRecordFromDataFile(string DataFileName, unsigned long long DataPointer)
{
	string ReturnString;
	char TempRecord[1024];
	fstream DataFile(DataFileName.c_str(), ios::in | ios::out | ios::binary);
	DataFile.seekg(DataPointer);

	while (DataFile)
	{
		DataFile.read(TempRecord, 1024);
		int pos = 0;
		while (pos < 1024  && TempRecord[pos] != '\n')
			pos++;

		if (pos > 100)
		{
			int check = 1;
		}

		if (pos < 1022)
		{
			TempRecord[pos + 1] = '\0';
		}
		else
		{
			int check = 1;
		}

		string TempString(TempRecord);
		ReturnString += TempString;
		if (pos < 1024)
		{
			break;
		}
	}

	DataFile.close();
	return ReturnString;
}

//Writes the first key-pointer pair to the LEAF block
void WriteFirstEntryInLeafBlock(std::string indexFileName, std::string &key, unsigned long long &dataLoc, unsigned long long seekPointer)
{
	char TempBuf[BLOCK_SIZE] = { '\0' };

	//memset(TempBuf, 0x00, BLOCK_SIZE);

	fstream TempIndexFile(indexFileName.c_str(), ios::in | ios::out | ios::binary);

	TempIndexFile.seekp(seekPointer);

	int Count = 1;
	memcpy(TempBuf, &Count, sizeof(Count));

	char BlockT = 'L';
	memcpy(TempBuf + sizeof(Count), &BlockT, sizeof(BlockT));
	memcpy(TempBuf + sizeof(Count) + sizeof(BlockT), key.c_str(), key.length());	
	memcpy(TempBuf + sizeof(Count) + sizeof(BlockT) + key.length(), &dataLoc, sizeof(dataLoc));

	unsigned long long NextLeafNodePointer = 0;
	//the pointer to next leaf node - since there is none we write 0
	memcpy(TempBuf + sizeof(Count) + sizeof(BlockT) + key.length() + sizeof(dataLoc), &NextLeafNodePointer, sizeof(NextLeafNodePointer));

	TempIndexFile.write(TempBuf, BLOCK_SIZE);
	TempIndexFile.close();
}

//Writes the first Intermediate/ root node to a non-leaf block
void WriteFirstElementInNonLeafBlock(std::string indexFileName, NodeElement &newElement, unsigned long long seekPointer)
{
	char TempBuf[BLOCK_SIZE] = { '\0' };

	//memset(TempBuf, 0x00, BLOCK_SIZE);

	fstream TempIndexFile(indexFileName.c_str(), ios::in | ios::out | ios::binary);
	TempIndexFile.seekp(seekPointer);

	int Count = 1;
	memcpy(TempBuf, &Count, sizeof(Count));

	char BlockT = 'N';
	memcpy(TempBuf + sizeof(Count), &BlockT, sizeof(BlockT));

	memcpy(TempBuf + sizeof(Count) + sizeof(BlockT), &newElement.PrevPointer, sizeof(newElement.PrevPointer));
	memcpy(TempBuf + sizeof(Count) + sizeof(BlockT) + sizeof(newElement.PrevPointer), newElement.Key.c_str(), newElement.Key.length());
	memcpy(TempBuf + sizeof(Count) + sizeof(BlockT) + sizeof(newElement.PrevPointer) + newElement.Key.length(), &newElement.NextPointer, sizeof(newElement.NextPointer));

	TempIndexFile.write(TempBuf, BLOCK_SIZE);
	TempIndexFile.close();
}

//Updates the first block of the index file with the header information
void UpdateIndexHeader(std::string indexFileName, std::string &DataFileName, int &keyLength, unsigned long long &RootNodePos)
{
	fstream TempIndexFile(indexFileName.c_str(), ios::in | ios::out | ios::binary);
	char TempBuf[BLOCK_SIZE] = { '\0' };

	//memset(TempBuf, 0x00, BLOCK_SIZE);

	memcpy(TempBuf, DataFileName.c_str(), 256);
	memcpy(TempBuf + 256, &keyLength, sizeof(int));
	memcpy(TempBuf + 256 + sizeof(int), &RootNodePos, sizeof(unsigned long long));

	TempIndexFile.write(TempBuf, BLOCK_SIZE);
	TempIndexFile.close();
}

//Reads the header from the first block of index file
void ReadIndexHeader(std::string &indexFileName, std::string &DataFileName, unsigned long long &RootNodePos, int &keyLength)
{
	char TempBuf[BLOCK_SIZE] = { '\0' };
	char TempBuf2[BLOCK_SIZE] = { '\0' };

	fstream TempindexFile(indexFileName.c_str(), ios::in | ios::out | ios::binary);

	TempindexFile.seekg(0);
	TempindexFile.read(TempBuf, BLOCK_SIZE);	//reads the header

	strncpy(TempBuf2, TempBuf, 256);

	DataFileName.operator=(TempBuf2);

	memcpy(&RootNodePos, TempBuf + 256 + sizeof(int), sizeof(unsigned long long));

	if (keyLength < 1)		//insert a single record case
	{
		memcpy(&keyLength, TempBuf + 256, sizeof(int));
	}
	TempindexFile.close();
}

//Finds a record with "keyOfRecord" as the key in the index file "indexFileName"
void FindRecord(string indexFileName, string keyOfRecord)
{
	//int fd = -1;
	int keyLength = 0;
	int UserKeyLength = keyOfRecord.length();
	if (UserKeyLength > 40)
		UserKeyLength = 40;
	int KeyLengthToUse = UserKeyLength;
	string DataFileName = "";
	unsigned long long RootNodePos = 0;
	unsigned long long NextNode = 0;

	fstream IndexFile(indexFileName, ios::in | ios::out | ios::binary);
	char TempBuf[BLOCK_SIZE] = { '\0' };
	char DummyBuf[256] = { '\0' };
	IndexFile.read(TempBuf, BLOCK_SIZE);	//reads the header

	memcpy(DummyBuf, TempBuf, 256);
	DataFileName.operator=(DummyBuf);
	memcpy(&keyLength, TempBuf + 256, sizeof(int));
	memcpy(&RootNodePos, TempBuf + 256 + sizeof(int), sizeof(unsigned long long));

	NextNode = RootNodePos;

	if (keyLength > UserKeyLength)
	{
		//printf("Finding Record: The length of key entered by user (%d) is less than file key length (%d)\n", UserKeyLength, keyLength);
	}
	else if (keyLength < UserKeyLength)
	{
		UserKeyLength = keyLength;
		KeyLengthToUse = keyLength;
	}

	while (IndexFile && (NextNode > 0))
	{
		IndexFile.seekg(NextNode);
		IndexFile.read(TempBuf, BLOCK_SIZE);

		int NoOfElements = 0;
		char BlockType;
		int ReadBytes = 0;
		memcpy(&NoOfElements, TempBuf, sizeof(int));
		ReadBytes += sizeof(int);
		memcpy(&BlockType, TempBuf + sizeof(int), sizeof(char));
		ReadBytes += sizeof(char);		

		if (BlockType == 'L')
		{
			unsigned long long RightPointer = 0;
			unsigned long long DataPointer = 0;
			string KeyOfRecord = "";
			string RestOfRecord = "";

			int CurElement = 0;
			for (CurElement = 0; CurElement < NoOfElements; CurElement++)
			{
				char DumBuf[40];			//keylength has a max value of 40
				memcpy(DumBuf, TempBuf + ReadBytes, keyLength);
				DumBuf[keyLength] = '\0';
				KeyOfRecord.operator=(DumBuf);
				ReadBytes += keyLength;

				memcpy(&DataPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
				ReadBytes += sizeof(unsigned long long);

				if (_stricmp(KeyOfRecord.c_str(), keyOfRecord.c_str()) == 0)
				{
					NextNode = 0;

					printf("Record found at %llu\n", DataPointer);

					//get data from datafile until we encounter '\n' and then display it
					printf("Record: %s\n", GetALargeRecordFromDataFile(DataFileName, DataPointer).c_str());

					break;
				}
				else if (_stricmp(KeyOfRecord.c_str(), keyOfRecord.c_str()) > 0)
				{
					NextNode = 0;
					printf("Record/Key Not found");
					break;
				}

				if (CurElement == (NoOfElements - 1))
				{
					memcpy(&RightPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
					ReadBytes += sizeof(unsigned long long);
					NextNode = RightPointer;
				}
			}
		}
		else
		{
			unsigned long long LeftPointer = 0;
			unsigned long long RightPointer = 0;
			string KeyOfRecord = "";

			memcpy(&LeftPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
			ReadBytes += sizeof(unsigned long long); 

			NextNode = LeftPointer;

			int CurElement = 0;
			for (CurElement = 0; CurElement < NoOfElements; CurElement++)
			{
				char DumBuf[40];			//keylength has a max value of 40
				memcpy(DumBuf, TempBuf + ReadBytes, keyLength);
				DumBuf[keyLength] = '\0';
				KeyOfRecord.operator=(DumBuf);
				ReadBytes += keyLength;

				memcpy(&RightPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
				ReadBytes += sizeof(unsigned long long);

				if (_stricmp(KeyOfRecord.c_str(), keyOfRecord.c_str()) <= 0)
				{
					NextNode = RightPointer;
				}
				else
				{
					break;
				}
			}
		}
	}
	IndexFile.close();
}

//Split the Filled Leaf node into two and return the first element of the right node to parent
void SplitFullLeafNode(string indexFileName, char  Tempbuf2[1024], char &BlockT, char  TempBuf[1024], std::string &key, NodeElement &ReturnStruct, unsigned long long blockPointer)
{
	//need to split the block into two here
	int NoB1Elements = (MaxNoOfKeysInALeafBlock / 2);
	int NoB2Elements = MaxNoOfKeysInALeafBlock - NoB1Elements;

	memset(Tempbuf2, 0x00, BLOCK_SIZE);			//the new right node will be stored here

	memcpy(Tempbuf2, &NoB2Elements, sizeof(int));
	memcpy(Tempbuf2 + sizeof(int), &BlockT, sizeof(char));
	memcpy(Tempbuf2 + sizeof(int) + sizeof(char), TempBuf + sizeof(int) + sizeof(char) + (NoB1Elements * (key.length() + 8)), BLOCK_SIZE - (sizeof(int) + sizeof(char) + (NoB1Elements * (key.length() + 8))));


	fstream TempIndexFile(indexFileName.c_str(), ios::in | ios::out | ios::binary);

	TempIndexFile.seekp(0, ios_base::end);
	unsigned long long EOIF = TempIndexFile.tellp();

	ReturnStruct.PrevPointer = blockPointer;
	ReturnStruct.NextPointer = EOIF;

	char Tempbuf3[BLOCK_SIZE] = { '\0' };		//contains the new left leaf node
	memcpy(Tempbuf3, &NoB1Elements, sizeof(int));
	memcpy(Tempbuf3 + sizeof(int), &BlockT, sizeof(char));
	memcpy(Tempbuf3 + sizeof(int) + sizeof(char), TempBuf + sizeof(int) + sizeof(char), (NoB1Elements * (key.length() + 8)));
	memcpy(Tempbuf3 + sizeof(int) + sizeof(char) + (NoB1Elements * (key.length() + 8)), &(ReturnStruct.NextPointer), sizeof(ReturnStruct.NextPointer));		//store the pointer to the right leaf node created above

	char DumBuf[40] = { '\0' };
	memcpy(DumBuf, Tempbuf2 + sizeof(int) + sizeof(char), key.length());
	DumBuf[key.length()] = '\0';
	ReturnStruct.Key.operator=(DumBuf);

	TempIndexFile.write(Tempbuf2, BLOCK_SIZE);

	TempIndexFile.seekp(blockPointer);
	TempIndexFile.write(Tempbuf3, BLOCK_SIZE);
	TempIndexFile.close();
}

//Split the Filled intermediate node into two and return the middle element of the original filled node to parent
//the new nodes will not contain the key/element being returned to the parent
void SplitFullNonLeafNode(string indexFileName, char Tempbuf2[1024], char &BlockT, char  TempBuf[1024], std::string &key, NodeElement &ReturnStruct, unsigned long long blockPointer)
{
	//split the node
	//need to split the block into two here
	int NoB1Elements = (MaxNoOfKeysInANonLeafBlock / 2);
	int NoB2Elements = MaxNoOfKeysInANonLeafBlock - NoB1Elements - 1;

	memset(Tempbuf2, 0x00, BLOCK_SIZE);			//the new right Non-Leaf node will be stored here

	memcpy(Tempbuf2, &NoB2Elements, sizeof(int));
	memcpy(Tempbuf2 + sizeof(int), &BlockT, sizeof(char));
	memcpy(Tempbuf2 + sizeof(int) + sizeof(char), TempBuf + sizeof(int) + sizeof(char) + ((NoB1Elements + 1) * (key.length() + 8)), BLOCK_SIZE - (sizeof(int) + sizeof(char) + ((NoB1Elements + 1) * (key.length() + 8))));

	fstream TempIndexFile(indexFileName.c_str(), ios::in | ios::out | ios::binary);

	TempIndexFile.seekp(0, ios_base::end);
	unsigned long long EOIF = TempIndexFile.tellp();

	ReturnStruct.PrevPointer = blockPointer;
	ReturnStruct.NextPointer = EOIF;


	char Tempbuf3[BLOCK_SIZE] = { '\0' };		//contains the new left Non-leaf node
	memcpy(Tempbuf3, &NoB1Elements, sizeof(int));
	memcpy(Tempbuf3 + sizeof(int), &BlockT, sizeof(char));
	memcpy(Tempbuf3 + sizeof(int) + sizeof(char), TempBuf + sizeof(int) + sizeof(char), (NoB1Elements * (key.length() + sizeof(unsigned long long))) + sizeof(unsigned long long));

	char DumBuf[40] = { '\0' };
	memcpy(DumBuf, TempBuf + sizeof(int) + sizeof(char) + (NoB1Elements * (key.length() + sizeof(unsigned long long))) + sizeof(unsigned long long), key.length());
	DumBuf[key.length()] = '\0';
	ReturnStruct.Key.operator=(DumBuf);

	TempIndexFile.write(Tempbuf2, BLOCK_SIZE);

	TempIndexFile.seekp(blockPointer);
	TempIndexFile.write(Tempbuf3, BLOCK_SIZE);
	TempIndexFile.close();
}

//Insert a key "key" into the B+Tree index file "indexFileName"
//Contains a recursive call to populate the index file
NodeElement InsertKey(string DataFileName, string indexFileName, unsigned long long blockPointer, string key, string recordToBeInserted, unsigned long long &pointer, unsigned long long dataLoc)
{
	fstream TempIndexFile(indexFileName.c_str(), ios::in | ios::out | ios::binary);
	NodeElement ReturnStruct;
	TempIndexFile.seekg(blockPointer);

	int ReadBytes = 0;
	char TempBuf[BLOCK_SIZE] = { '\0' };
	TempIndexFile.read(TempBuf, BLOCK_SIZE);

	int Count = 0;
	memcpy(&Count, TempBuf, sizeof(int));

	char BlockT;
	memcpy(&BlockT, TempBuf + sizeof(int), sizeof(char));

	ReadBytes = sizeof(char) + sizeof(int);
	int index = 0;

	if (BlockT == 'L')		//leaf Block
	{
		#pragma region Leaf Node
		
		for (index = 0; index < Count; index++)
		{
			
			char DumBuf[40] = { '\0' };
			memcpy(DumBuf, TempBuf + ReadBytes, key.length());
			DumBuf[key.length()] = '\0';
			string ComPKey(DumBuf);
			ReadBytes += key.length();

			if (key < ComPKey)
			{
				//move the rest of the data to the right and add this key and data-pointer.
				//if the no of elements (i.e. 'Count+1') equals 'MaxNoOfKeysInALeafBlock' then split the blocks and return the middle element so that it can be added to the previous parent block.
				char Tempbuf2[BLOCK_SIZE] = { '\0' };
				memcpy(Tempbuf2, TempBuf + (ReadBytes - key.length()), BLOCK_SIZE - (ReadBytes - key.length()));

				if (dataLoc > 0)
				{
					memcpy(TempBuf + (ReadBytes - key.length()), key.c_str(), key.length());
					memcpy(TempBuf + ReadBytes, &dataLoc, sizeof(dataLoc));
					memcpy(TempBuf + ReadBytes + sizeof(dataLoc), Tempbuf2, BLOCK_SIZE - (ReadBytes + sizeof(dataLoc)));					

				}
				else						//data needs to be inserted to the end of datafile - this is insert after the index is created - not during the creation
				{
					fstream DataFile(DataFileName.c_str(), ios::in | ios::out | ios::binary);
					DataFile.seekp(0, ios_base::end);
					unsigned long long EOIF = DataFile.tellp();

					//writing data at the end of data file
					DataFile.write(recordToBeInserted.c_str(), recordToBeInserted.length());
					DataFile.write("\n", 1);

					memcpy(TempBuf + (ReadBytes - key.length()), key.c_str(), key.length());
					memcpy(TempBuf + ReadBytes, &EOIF, sizeof(EOIF));
					memcpy(TempBuf + ReadBytes + sizeof(EOIF), Tempbuf2, BLOCK_SIZE - (ReadBytes + sizeof(EOIF)));
				}

				if (Count + 1 == MaxNoOfKeysInALeafBlock)
				{
					SplitFullLeafNode(indexFileName, Tempbuf2, BlockT, TempBuf, key, ReturnStruct, blockPointer);					
				}
				else
				{
					int NewCount = Count + 1;
					memcpy(TempBuf, &NewCount, sizeof(int));
					TempIndexFile.seekp(blockPointer);
					TempIndexFile.write(TempBuf, BLOCK_SIZE);
				}
				TempIndexFile.close();
				return ReturnStruct;

			}
			else if (_stricmp(key.c_str(), ComPKey.c_str()) == 0)
			{
				//key already exits
				unsigned long long CompDataPointer = 0;
				memcpy(&CompDataPointer, TempBuf + ReadBytes, sizeof(unsigned long long));

				char SubString[1024] = { '\0' };
				if (recordToBeInserted.length() < 1025)
				{
					strncpy(SubString, recordToBeInserted.c_str(), recordToBeInserted.length() - 2);
					printf("'%s' was not added as the given key %s already exits at the (byte) location %llu \n", SubString, key.c_str(), CompDataPointer);
				}
				else
				{
					strncpy(SubString, recordToBeInserted.c_str(), 1023);
					printf("'%s...' was not added as the given key %s already exits at the (byte) location %llu \n", SubString, key.c_str(), CompDataPointer);
				}
				TempIndexFile.close();
				return ReturnStruct;
			}

			unsigned long long CompDataPointer = 0;
			memcpy(&CompDataPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
			ReadBytes += sizeof(unsigned long long);
		}

		//don't use the next pointer. it is used only for traversing the tree
		unsigned long long CompNextPointer = 0;
		memcpy(&CompNextPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
		ReadBytes += sizeof(unsigned long long);

		// add the key and pointer data to the end of the block and write the next pointer after it.
		//if the no of elements (i.e. 'Count+1') equals 'MaxNoOfKeysInALeafBlock' then split the blocks and return the middle element so that it can be added to the previous parent block.
		memcpy(TempBuf + (ReadBytes - sizeof(unsigned long long)), key.c_str(), key.length());
		memcpy(TempBuf + ReadBytes - sizeof(unsigned long long) + key.length(), &dataLoc, sizeof(dataLoc));
		memcpy(TempBuf + ReadBytes + sizeof(dataLoc) + key.length() - sizeof(unsigned long long), &CompNextPointer, sizeof(unsigned long long));

		if (Count + 1 == MaxNoOfKeysInALeafBlock)
		{
			char Tempbuf2[BLOCK_SIZE] = { '\0' };
			SplitFullLeafNode(indexFileName, Tempbuf2, BlockT, TempBuf, key, ReturnStruct, blockPointer);
			TempIndexFile.close();
			return ReturnStruct;
		}
		else
		{
			int NewCount = Count + 1;
			memcpy(TempBuf, &NewCount, sizeof(int));
			TempIndexFile.seekp(blockPointer);
			TempIndexFile.write(TempBuf, BLOCK_SIZE);
		}

		#pragma endregion
		
	}
	else					//Non-Leaf Block
	{
		#pragma region Non-Leaf Node
		unsigned long long TempLeftPointer = 0;
		unsigned long long TempRightPointer = 0;

		memcpy(&TempLeftPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
		ReadBytes += sizeof(unsigned long long);
		string InterKey;

		for (index = 0; index < Count; index++)
		{
			char DumBuf[40] = { '\0' };
			memcpy(DumBuf, TempBuf + ReadBytes, key.length());
			DumBuf[key.length()] = '\0';
			InterKey.operator=(DumBuf);
			ReadBytes += key.length();

			memcpy(&TempRightPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
			ReadBytes += sizeof(unsigned long long);

			if (key < InterKey)
			{
				NodeElement TempStruct2;
				TempStruct2 = InsertKey(DataFileName, indexFileName, TempLeftPointer, key, recordToBeInserted, pointer, dataLoc);

				if (!TempStruct2.Key.empty())
				{
					char Tempbuf2[BLOCK_SIZE] = { '\0' };
					memcpy(Tempbuf2, TempBuf + (ReadBytes - key.length() - sizeof(unsigned long long)), BLOCK_SIZE - (ReadBytes - key.length() - sizeof(unsigned long long)));

					memcpy(TempBuf + (ReadBytes - key.length() - (2 * sizeof(unsigned long long))), &(TempStruct2.PrevPointer), sizeof(unsigned long long));
					memcpy(TempBuf + (ReadBytes - key.length() - sizeof(unsigned long long)), TempStruct2.Key.c_str(), key.length());				//key.length() == TempStruct2.Key.length()
					memcpy(TempBuf + (ReadBytes - sizeof(unsigned long long)), &(TempStruct2.NextPointer), sizeof(unsigned long long));

					memcpy(TempBuf + ReadBytes, Tempbuf2, BLOCK_SIZE - (ReadBytes));


					if (Count + 1 == MaxNoOfKeysInANonLeafBlock)
					{
						SplitFullNonLeafNode(indexFileName, Tempbuf2, BlockT, TempBuf, key, ReturnStruct, blockPointer);
					}
					else
					{
						int NewCount = Count + 1;
						memcpy(TempBuf, &NewCount, sizeof(int));
						TempIndexFile.seekp(blockPointer);
						TempIndexFile.write(TempBuf, BLOCK_SIZE);
					}
				}
				TempIndexFile.close();
				return ReturnStruct;
			}
			TempLeftPointer = TempRightPointer;
		}

		NodeElement TempStruct;
		TempStruct = InsertKey(DataFileName, indexFileName, TempLeftPointer, key, recordToBeInserted, pointer, dataLoc);

		if (!TempStruct.Key.empty())
		{
			char Tempbuf2[BLOCK_SIZE] = { '\0' };
			memcpy(Tempbuf2, TempBuf + (ReadBytes), BLOCK_SIZE - (ReadBytes));

			memcpy(TempBuf + (ReadBytes - sizeof(unsigned long long)), &(TempStruct.PrevPointer), sizeof(unsigned long long));	//redundant
			memcpy(TempBuf + (ReadBytes), TempStruct.Key.c_str(), key.length());				//key.length() == TempStruct.Key.length()
			memcpy(TempBuf + (ReadBytes + key.length()), &(TempStruct.NextPointer), sizeof(unsigned long long));

			memcpy(TempBuf + ReadBytes +  sizeof(unsigned long long) + key.length(), Tempbuf2, BLOCK_SIZE - (ReadBytes + sizeof(unsigned long long) + key.length()));


			if (Count + 1 == MaxNoOfKeysInANonLeafBlock)
			{
				SplitFullNonLeafNode(indexFileName, Tempbuf2, BlockT, TempBuf, key, ReturnStruct, blockPointer);
			}
			else
			{
				int NewCount = Count + 1;
				memcpy(TempBuf, &NewCount, sizeof(int));
				TempIndexFile.seekp(blockPointer);
				TempIndexFile.write(TempBuf, BLOCK_SIZE);
			}
		}
		TempIndexFile.close();
		return ReturnStruct;
		#pragma endregion

	}
	TempIndexFile.close();
	return ReturnStruct;
}

//This function calls the "InsertKey" function to insert a record into the index file
void InsertRecord(string indexFileName, string recordToBeInserted, unsigned long long dataLoc, int keyLength)
{
	fstream TempIndexFile(indexFileName.c_str(), ios::in | ios::out | ios::binary);
	string DataFileName = "";
	unsigned long long RootNodePos = 0;

	ReadIndexHeader(indexFileName, DataFileName, RootNodePos, keyLength);

	string key = recordToBeInserted.substr(0, keyLength);
	unsigned long long TestPointer = 0;
	
	NodeElement newElement;
	if (RootNodePos > 0)
	{
		newElement = InsertKey(DataFileName, indexFileName, RootNodePos, key, recordToBeInserted, TestPointer, dataLoc);
	}
	else				//first element being added
	{
		RootNodePos = BLOCK_SIZE;

		UpdateIndexHeader(indexFileName, DataFileName, keyLength, RootNodePos);

		WriteFirstEntryInLeafBlock(indexFileName, key, dataLoc, BLOCK_SIZE);
	}

	if (!newElement.Key.empty())
	{
		TempIndexFile.seekp(0, ios_base::end);
		unsigned long long EOIF = TempIndexFile.tellp();
		
		WriteFirstElementInNonLeafBlock(indexFileName, newElement, EOIF);

		RootNodePos = EOIF;

		UpdateIndexHeader(indexFileName, DataFileName, keyLength, RootNodePos);
	}

	TempIndexFile.close();
}

//Gets one record at a time and inserts it into the index file using the "InsertRecord" function
void CreateIndex(string dataFileName, string indexFileName, int keyLength)
{
	unsigned long long RootNodePos = 0;

	fstream TempIndexFile;
	TempIndexFile.open(indexFileName.c_str(), ios::in | ios::out | ios::binary | ios::trunc);

	char TempBuf[BLOCK_SIZE] = { '\0' };
	memcpy(TempBuf, dataFileName.c_str(), 256);
	memcpy(TempBuf + 256, &keyLength, sizeof(int));
	memcpy(TempBuf + 256 + sizeof(int), &RootNodePos, sizeof(unsigned long long));

	TempIndexFile.write(TempBuf, BLOCK_SIZE);
	TempIndexFile.close();

	fstream DataFile(dataFileName.c_str(), ios::in | ios::out | ios::binary);
	unsigned long long CurP = 0;


	struct stat FileSize;

	if (stat(dataFileName.c_str(), &FileSize) == 0)
	{
		// FileSize.st_size contains the size of the file in bytes
		// printf("%ld", FileSize.st_size);
	}

	while (DataFile && CurP < FileSize.st_size)
	{
		unsigned long long DataP = DataFile.tellg();

		string RecordToInsert = GetALargeRecordFromDataFile(dataFileName.c_str(), DataP);
		CurP += RecordToInsert.length();

		InsertRecord(indexFileName, RecordToInsert, DataP, keyLength);
		DataFile.seekg(CurP);
	}
	DataFile.close();
}

//Lists "count" number of records
void ListNRecords(string indexFileName, unsigned long long NodeStartingPointer, int currentElementNo, int count, int keyLength, string DataFileName)
{
	fstream TempIndexFile(indexFileName.c_str(), ios::in | ios::out | ios::binary);
	char TempBuf[BLOCK_SIZE] = { '\0' };
	int Listed = 0;

	unsigned long long RightPointer = 0;
	unsigned long long DataPointer = 0;
	string KeyOfRecord = "";
	string RestOfRecord = "";

	while (Listed < count && NodeStartingPointer > 0)
	{
		TempIndexFile.seekg(NodeStartingPointer);
		TempIndexFile.read(TempBuf, BLOCK_SIZE);

		int NoOfElements = 0;
		char BlockType;
		int ReadBytes = 0;
		memcpy(&NoOfElements, TempBuf, sizeof(int));
		ReadBytes += sizeof(int);
		memcpy(&BlockType, TempBuf + sizeof(int), sizeof(char));
		ReadBytes += sizeof(char);

		ReadBytes += ((sizeof(unsigned long long) + keyLength) * currentElementNo);

		for (; (currentElementNo < NoOfElements) && (Listed < count); currentElementNo++)
		{
			char DumBuf[40] = { '\0' };
			memcpy(DumBuf, TempBuf + ReadBytes, keyLength);
			DumBuf[keyLength] = '\0';
			KeyOfRecord.operator=(DumBuf);
			ReadBytes += keyLength;

			memcpy(&DataPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
			ReadBytes += sizeof(unsigned long long);

			string Rec = GetALargeRecordFromDataFile(DataFileName, DataPointer);
			char SubString[1024] = { '\0' };
			strncpy(SubString, Rec.c_str(), Rec.length() - 2);

			printf("At %llu, Record: %s\n", DataPointer, SubString);

			Listed++;

			if (currentElementNo == (NoOfElements - 1))
			{
				memcpy(&RightPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
				ReadBytes += sizeof(unsigned long long);
				NodeStartingPointer = RightPointer;
			}			
		}
		currentElementNo = 0;
	}
	TempIndexFile.close();
}

// Calls "ListNRecords" function to list "noOfRecordsToList" number of recors following the key "startingKey"
// if the key "startingKey" is not found then it starts listing from the key immediately greater than "startingKey"
void ListRecord(string indexFileName, string startingKey, int noOfRecordsToList)
{
	string DataFileName = "";
	unsigned long long RootNodePos = 0;

	fstream TempIndexFile(indexFileName.c_str(), ios::in | ios::out | ios::binary);

	int keyLength = 0;
	ReadIndexHeader(indexFileName, DataFileName, RootNodePos, keyLength);

	if (keyLength != startingKey.length())
	{
		printf("key length in the index file %d doesn't match with the length of the starting key %d", keyLength, startingKey.length());
	}

	unsigned long long NextNode = 0;
	NextNode = RootNodePos;
	char TempBuf[BLOCK_SIZE] = { '\0' };

	while (TempIndexFile && (NextNode > 0))
	{
		TempIndexFile.seekg(NextNode);
		TempIndexFile.read(TempBuf, BLOCK_SIZE);

		int NoOfElements = 0;
		char BlockType;
		int ReadBytes = 0;
		memcpy(&NoOfElements, TempBuf, sizeof(int));
		ReadBytes += sizeof(int);
		memcpy(&BlockType, TempBuf + sizeof(int), sizeof(char));
		ReadBytes += sizeof(char);

		if (BlockType == 'L')
		{
			unsigned long long RightPointer = 0;
			unsigned long long DataPointer = 0;
			

			int CurElement = 0;
			for (CurElement = 0; CurElement < NoOfElements; CurElement++)
			{
				string KeyOfRecord = "";
				string RestOfRecord = "";
				char DumBuf[40] = { '\0' };
				memcpy(DumBuf, TempBuf + ReadBytes, keyLength);
				DumBuf[keyLength] = '\0';
				KeyOfRecord.operator=(DumBuf);
				ReadBytes += keyLength;

				memcpy(&DataPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
				ReadBytes += sizeof(unsigned long long);

				if (_stricmp(KeyOfRecord.c_str(), startingKey.c_str()) == 0)
				{		

					printf("\nRecord found at %llu\n", DataPointer);

					//get data from datafile until we encounter '\n' and then display it
					printf("Record: %s\n", GetALargeRecordFromDataFile(DataFileName, DataPointer).c_str());

					ListNRecords(indexFileName, NextNode, (CurElement), noOfRecordsToList, keyLength, DataFileName);

					NextNode = 0;
					break;
				}
				else if (_stricmp(KeyOfRecord.c_str(), startingKey.c_str()) > 0)
				{
					
					printf("\nGiven Key was not found\n");

					ListNRecords(indexFileName, NextNode, (CurElement), noOfRecordsToList, keyLength, DataFileName);
					NextNode = 0;
					break;
				}

				if (CurElement == (NoOfElements - 1))
				{
					memcpy(&RightPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
					ReadBytes += sizeof(unsigned long long);
					NextNode = RightPointer;
				}
			}
		}
		else
		{
			unsigned long long LeftPointer = 0;
			unsigned long long RightPointer = 0;
			string KeyOfRecord = "";

			memcpy(&LeftPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
			ReadBytes += sizeof(unsigned long long);

			NextNode = LeftPointer;

			int CurElement = 0;
			for (CurElement = 0; CurElement < NoOfElements; CurElement++)
			{
				char DumBuf[40] = { '\0' };
				memcpy(DumBuf, TempBuf + ReadBytes, keyLength);
				DumBuf[keyLength] = '\0';
				KeyOfRecord.operator=(DumBuf);
				ReadBytes += keyLength;

				memcpy(&RightPointer, TempBuf + ReadBytes, sizeof(unsigned long long));
				ReadBytes += sizeof(unsigned long long);

				if (_stricmp(KeyOfRecord.c_str(), startingKey.c_str()) <= 0)
				{
					NextNode = RightPointer;
				}
				else
				{
					break;
				}
			}

		}
	}
	TempIndexFile.close();
}

// Entry point of the program
// noOfArguments - number of arguments in the commandline
// arguments[] - the actual arguments as an array
int main(int noOfArguments, char* arguments[])
{
	if (noOfArguments < 4)
	{
		printf("\nInvalid argument set\n");
		printf("Accepted arguments need to be in one of the below formats:\n");
		printf("\t1. <program_name> -create <input_data_file> <output_index_file> <key_length>\n");
		printf("\t2. <program_name> -find <input_index_file> <key_of_the_record_to_find>\n");
		printf("\t3. <program_name> -insert <input_index_file> <record_to_be_inserted>\n");
		printf("\t4. <program_name> -list <input_index_file> <starting_key> <no_of_records_to_list>\n");

		printf("\nEnter a character to exit\n");
		getchar();
		return 0;
	}

	if ( _stricmp(arguments[1],"-create") == 0)
	{

		if (noOfArguments != 5)
		{
			printf("\nInvalid argument set\n");
			printf("Accepted arguments need to be in one of the below formats:\n");
			printf("\t1. <program_name> -create <input_data_file> <output_index_file> <key_length>\n");
			printf("\t2. <program_name> -find <input_index_file> <key_of_the_record_to_find>\n");
			printf("\t3. <program_name> -insert <input_index_file> <record_to_be_inserted>\n");
			printf("\t4. <program_name> -list <input_index_file> <starting_key> <no_of_records_to_list>\n");
			return 0;
		}
		int key_length = 0;
		try
		{
			key_length = atoi(arguments[4]);

			if (key_length < 1 || key_length > 40)
			{
				printf("\nInvalid value for Key length. Key length lies in the range [1,40]\n");
				return 0;
			}

			MaxNoOfKeysInALeafBlock = ((1024 - 13) / (key_length+8));
			MaxNoOfKeysInANonLeafBlock = ((1024 - 13) / (key_length + 8));
		}
		catch (const std::exception&)
		{
			printf("Invalid value for key_length\n");
			return 0;
		}
		CreateIndex(arguments[2], arguments[3], key_length);

	}
	else if (_stricmp(arguments[1], "-find") == 0)
	{

		if (noOfArguments != 4)
		{
			printf("\nInvalid argument set\n");
			printf("Accepted arguments need to be in one of the below formats:\n");
			printf("\t1. <program_name> -create <input_data_file> <output_index_file> <key_length>\n");
			printf("\t2. <program_name> -find <input_index_file> <key_of_the_record_to_find>\n");
			printf("\t3. <program_name> -insert <input_index_file> <record_to_be_inserted>\n");
			printf("\t4. <program_name> -list <input_index_file> <starting_key> <no_of_records_to_list>\n");
			return 0;
		}
		FindRecord(arguments[2], arguments[3]);

	}
	else if (_stricmp(arguments[1], "-insert") == 0)
	{

		if (noOfArguments != 4)
		{
			printf("\nInvalid argument set\n");
			printf("Accepted arguments need to be in one of the below formats:\n");
			printf("\t1. <program_name> -create <input_data_file> <output_index_file> <key_length>\n");
			printf("\t2. <program_name> -find <input_index_file> <key_of_the_record_to_find>\n");
			printf("\t3. <program_name> -insert <input_index_file> <record_to_be_inserted>\n");
			printf("\t4. <program_name> -list <input_index_file> <starting_key> <no_of_records_to_list>\n");
			return 0;
		}
		InsertRecord(arguments[2], arguments[3]);

	}
	else if (_stricmp(arguments[1], "-list") == 0)
	{
		if (noOfArguments != 5)
		{
			printf("\nInvalid argument set\n");
			printf("Accepted arguments need to be in one of the below formats:\n");
			printf("\t1. <program_name> -create <input_data_file> <output_index_file> <key_length>\n");
			printf("\t2. <program_name> -find <input_index_file> <key_of_the_record_to_find>\n");
			printf("\t3. <program_name> -insert <input_index_file> <record_to_be_inserted>\n");
			printf("\t4. <program_name> -list <input_index_file> <starting_key> <no_of_records_to_list>\n");
			return 0;
		}
		int count = 0;
		try
		{
			count = atoi(arguments[4]);
		}
		catch (const std::exception&)
		{
			printf("Invalid value for count\n");
			return 0;
		}
		ListRecord(arguments[2], arguments[3], count);

	}
	else
	{
		printf("\nInvalid Operation\n");
		int index = 0;
		printf("Arguments entered: ");
		for (index = 0; index < noOfArguments; index++)
		{
			printf("%s ", arguments[index]);
		}
		printf("\n");
		printf("\nAccepted arguments need to be in one of the below formats:\n");
		printf("\t1. <program_name> -create <input_data_file> <output_index_file> <key_length>\n");
		printf("\t2. <program_name> -find <input_index_file> <key_of_the_record_to_find>\n");
		printf("\t3. <program_name> -insert <input_index_file> <record_to_be_inserted>\n");
		printf("\t4. <program_name> -list <input_index_file> <starting_key> <no_of_records_to_list>\n");

		printf("\nEnter a character to exit\n");
		getchar();
		return 0;
	}

	printf("\nProgram executed successfully\n");
	printf("Press a key to exit\n");
	_getch();
	return 0;
}
