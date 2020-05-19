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