using CommandLine;

namespace GameOfLifeConsole
{
    public class Options
    {
        [Option('w', "width", Required = false, Default = 16, HelpText = "The width of the grid. The default is set to 16")]
        public int Width { get; set; }

        [Option('h', "height", Required = false, Default = 16, HelpText = "The height of the grid. The default is set to 16")]
        public int Height { get; set; }

        [Option('s', "seed", Required = false, Default = 4, HelpText = "The pattern which initializes the grid. \r\n"+
                                                                       " 0 = Blinker \r\n" +
                                                                       " 1 = Toad \r\n" +
                                                                       " 2 = Beacon \r\n" +
                                                                       " 3 = Glider \r\n" +
                                                                       " 4 = Random\r\n" + 
                                                                       "The default is set to 'Random'")]
        public int Seed { get; set; }

        [Option('t', "ticks", Required = false, Default = 2, HelpText = "The maximum number of generations until the program ends. The default is set to infinite")]
        public int Ticks { get; set; }

        [Option('o', "output", Required = false, Default = "", HelpText = "Name of the output file to save the final state of the grid. By default, no output file is written")]
        public string OutputFileName { get; set; }
    }
}