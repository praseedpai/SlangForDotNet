# SlangForDotNet
The Source code of the Slang4.net Compiler 

The Project aims to show how one can build a compiler using C# and the Reflection.Emit namespace. The name of the language is SLANGFOR.NET ( Simple LANGuage FOR .net )

SLANGFOR.net is an attempt to teach Compiler Construction using C# and the .NET platform. The Programming language was created for a BarCamp session which was held @ Technopark, Trivandrum , INDIA. The Project has been hosted in CodePlex as part of the Presentation (on SLANGFOR.net ) for Community Tech Day event @ Kochi ,Kerala , INDIA(which was held on 30th january 2010).

The Compiler is written in stages to demonstrate the process involved . The Software aims to demonstrate
Compiler engineering as a software engineering excersise. You can consider this Project as an example of
how .NET Reflection API and Reflection.Emit API can be used to build a Compiler backend.

The Original version of this compiler backend was written in the Year 2003 for a Business Process
customization language which generates the code on the fly. This can be considered as an example of
"safe" self modifying code. This method will give native code generation facility (via JIT) along with the
security sandbox of Common Language Runtime ( CLR )

The Author of the compiler is Praseed Pai K.T , who is the author of ".NET Design Patterns" and "C++ Reactive Programming"
published by Packt Publishing.

An E-book titled "The Art of Compiler Construction using C#" is available in the "CompilerEbook" folder of the download.

The Compiler has been used by College students and Academia for their Academic Projects. The kind of
projects on top of this undertaken include Slang to C# translator , Addition of features like
Call by reference ,Embedding the backend into a host system and Interoperability with native code etc.

The Compiler supports Scalar variables , Assignment statements , if/while statement ,
user defined procedures (functions),recursion , .NET IL code generation etc. The author does not want to
add new features to the code base hosted hereto make this a easy learning tool. To add new features ,
a tree walking interpreter is also available in the system.


P.S :- In the download section , if you download a step you will get the source code of all the step before as well.

The Port of compiler to other languages are

*C++: https://github.com/pradeep-subrahmanion/SLANG4CPP
*Java Interpreter: https://github.com/aashiks/Slang4Java
*Python Interpreter: https://github.com/faisalp4p/slang-python
