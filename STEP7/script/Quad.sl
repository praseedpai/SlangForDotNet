FUNCTION NUMERIC Quad( NUMERIC a , NUMERIC b , NUMERIC c )
   NUMERIC n;

   n = b*b - 4*a*c;
   IF ( n < 0 ) THEN
        return 0;
   ELSE 

     IF ( n == 0 ) THEN
         return 1;
     ELSE
         return 2;
     ENDIF
 
   ENDIF 
   return 0;


END


FUNCTION BOOLEAN MAIN()
   NUMERIC d;
   d= Quad(1,0-6,9);

   IF ( d == 0 ) then
         PRINT "No Roots";
   ELSE
       IF ( d  == 1 ) then
         PRINT  "Discriminant is zero";
       ELSE
         PRINT  "Two roots are available";
       ENDIF
   ENDIF
END
