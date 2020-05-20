#pragma once

void ReadIndexHeader(std::string &DataFileName, unsigned long long &RootNodePos, int &keyLength);

void WriteFirstEntryInLeafBlock(char  TempBuf[1024], std::string &key, unsigned long long &dataLoc);

void UpdateIndexHeader(char  TempBuf[1024], std::string &DataFileName, int &keyLength, unsigned long long &RootNodePos);
