/*
  This program is written by Amith Kumar Matapady (axm180029) at The University of Texas at Dallas
  starting October 28, 2018, as a part of the Database Design course (CS6360.004) Fall 2018.

  The main aim of this program is to prepare a simple index on a text file.
  This program generates an executable Index.exe.
  This executable can be run from the command prompt in one of the below ways:
 	Index.exe -c <input_data_file> <output_index_file> <key_length>
 	Index.exe -l <input_index_file> <input_data_file> <key_length>

  the value of key_length decides the number of first "key_length" characters to be treated as index.

  Using the second command (i.e. '-l') displays the output on the command prompt and also creates a file
  named "indexed_output.txt" which contains the data in the required sorted order.  
*/