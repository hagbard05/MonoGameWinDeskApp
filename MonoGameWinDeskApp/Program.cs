using System;
using System.Threading.Tasks;

namespace MyFirstGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static async Task Main(string[] args)
        {
            bool trialMode = Array.Exists(args, argument => string.Equals(argument, "--trial", StringComparison.OrdinalIgnoreCase));
            if (!trialMode)
            {
                trialMode = await StoreLicense.IsTrialAsync();
            }

            using (MainGame game = new MainGame(trialMode))
            {
                game.Run();
            }
        }
    }
}

