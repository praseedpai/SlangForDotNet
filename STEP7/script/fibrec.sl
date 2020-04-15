/////////////////////////////////
//
// Recursive Fibonacci series routine
//
//

FUNCTION NUMERIC FIB( NUMERIC n )
       IF ( n <= 1 ) then
             return 1;
       ELSE
           RETURN FIB(n-1) + FIB(n-2);
       ENDIF   

END



/////////////////////////////////////////
//
//
//  Main routine
//
//

FUNCTION BOOLEAN MAIN()
NUMERIC d;
d=0;
While ( d <= 10 )
    PRINTLINE FIB(d);
    d = d+1;
Wend
END
