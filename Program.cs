using System;
using System.Linq;

namespace brainfuck {
    public static class Program {
        public static void Main(string[] args) {
            // <+[>+[<->[->+<]>+[-<+<+>>]<<[.+].>]< ]    =>   <+[>+[<->[-.>+.<]>+[-.<+.<+>>]<<[.+].>]<+.]
            string path = "\0";
            bool debug = false;

            if(args.Length != 0)
                (path, debug) = Args(args);
            else {
                Console.WriteLine("Type --help or -h for more Information.");
                Environment.Exit(1);
            }

            string code = usefull.ReadFile(path);
            if (code == "\0") {
                Console.WriteLine("This file does not exsist or is with higer Permision protectet.");
                Environment.Exit(404);
            }

            int exitCode = Run(code, debug);
            Environment.Exit(exitCode);
        }

        public static (string, bool) Args(string[] args) {
            string path = "\0";
            bool debug = false;
            ArgType type = ArgType.none;
            
            for(int i = 0; i < args.Length || type != ArgType.none; i++) {
                if(type != ArgType.none) {

                    switch(type) {
                        case ArgType.help:
                            i--;
                            Console.WriteLine();
                            Console.WriteLine("---Help---");
                            Console.WriteLine("\n\rInformation: ");
                            Console.WriteLine("This Programm executes Brainfuck Code. It Compiles to a simulare Language and than Interpretes it. For more Information about Brainfuck use the Wiki: 'https://de.wikipedia.org/wiki/Brainfuck'");
                            Console.WriteLine("\n\rArguments:          Information: ");
                            Console.WriteLine("     -h or help     this.");
                            Console.WriteLine("     -f or file     The File that this Programm shuldn execute.");
                            Console.WriteLine("     -d or debug    Enable Debug mode and Shows the extuley Compiled Code.");
                            Environment.Exit(0);
                            break;
                        case ArgType.file:
                            if (i < args.Length)
                                path = args[i];
                            break;
                        case ArgType.debug:
                            debug = true;
                            i--;
                            break;
                        default:
                            break;
                    }

                    type = ArgType.none;
                    continue;
                }

                switch(args[i].ToLower()) {
                    case "--help" or "-h":
                        type = ArgType.help;
                        break;
                    case "--file" or "-f":
                        type = ArgType.file;
                        break;
                    case "--debug" or "-d":
                        type = ArgType.debug;
                        break;
                    default:
                        type = ArgType.none;
                        break;
                }

            }

            return (path, debug);
        }

        private enum ArgType {
            file,
            debug,
            help,
            none
        }

        static int Run(string c, bool debug) {
            Debug.isDebugMode = debug;
            AssemblyCode.Code code = new AssemblyCode.Code(c);

            Debug.WriteLine("Compile Code to Assembly...");
            code.Compile();

            Debug.WriteLine("\n\r***Debug***");

            // Write The Original Code without the ASCI Controle characters. => \n, \r, \0, ...
            Debug.WriteLine("\n\rCode: ");
            if(debug) {
                string deb = new string(c.Where(c => !char.IsControl(c)).ToArray());
                if (deb.Length != 0)
                    Debug.WriteLine(deb);
                else
                    Debug.WriteLine("-");
            }

            Debug.WriteLine("\n\rAssembly Code:");
            Debug.PrintAssemlyCode(code.operations);
            Debug.WriteLine("\n\rAssembly Loops: ");
            Debug.PrintAssemlyLoops(code.loops);
            Debug.WriteLine($"\n\rRunnable: {code.runnable}");
            Debug.WriteLine("\n\r***Debug***");

            Debug.WriteLine("\n\rRun Assembly Code...");
            code.Run();

            Debug.WriteLine("\n\n\rProgramm stoped.\n\rExit Code(Current Value at ptr Position): " + code.exitCode);
            return code.exitCode;
        }
    }
}
