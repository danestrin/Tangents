using System;

namespace Tangents
{
    public static class Tangents
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameRoot())
                game.Run();
        }
    }
}
