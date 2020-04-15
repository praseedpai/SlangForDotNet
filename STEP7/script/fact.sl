////////////////////
//
//  Recursive Routine to Print Factorial of a number
//
//  STEP 7 and above ...test case for recursion ...
//

FUNCTION NUMERIC FACT( NUMERIC d )
    IF ( d <= 0 ) THEN
          return 1;
    ELSE
          return d*FACT(d-1);
    ENDIF

END

////////////////////
//
//
//  Entry Point
//
//
FUNCTION BOOLEAN MAIN()
NUMERIC d;
d=0;
While ( d <= 10 )
    PRINTLINE FACT(d);
    d = d+1;
Wend
END
