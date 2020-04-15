////////////////////////////////////////////////
//
// A simple Slang Script ...can only run with 
//   STEP 7 or above
//
// This code will print Fibonacci series between 1
// and 1000

FUNCTION BOOLEAN MAIN()
 NUMERIC newterm;
 NUMERIC prevterm;
 NUMERIC currterm;
 
 currterm = 1;
 prevterm = 0;

 newterm = currterm + prevterm;

 PrintLine newterm; 

 while ( newterm <  1000 )
   
   prevterm = currterm;
   currterm = newterm;
   newterm  = currterm + prevterm; 
   PrintLine newterm;
   
 wend

 
END
