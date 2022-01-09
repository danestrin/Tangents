using System;

namespace Tangents
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Tangents())
                game.Run();
        }
    }
}
