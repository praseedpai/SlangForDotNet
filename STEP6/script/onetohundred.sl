///////////////////////////////
//
//  Program to Generate Numbers from one to 100
//  This Script can be used with Step 6 compiler only
//


// ---------------- Print from 1 to 100 

NUMERIC I;

I = 0;

WHILE ( I <= 100 )
  PRINTLINE I;
  I = I + 1;
WEND

//----------------- Print all even numbers

I=0;

WHILE ( I <= 100 )
 
   PRINTLINE I;
  
   I = I + 2;

WEND

// ----------------- Testing String Comparison

STRING s1;
STRING s2;

s1="Hello";
s2="Hell"+"o";

if ( s1 == s2 ) then

   PRINTLINE " S1 and S2 are equal ";

endif          

IF !( s1 <> s2 )  then

    PRINTLINE "Tested Logical Not" ;
endif

