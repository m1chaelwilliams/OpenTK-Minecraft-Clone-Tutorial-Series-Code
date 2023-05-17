namespace Minecraft_Clone_Tutorial_Series_videoproj
{
    public class Program
    {
        // Entry point of the program
        static void Main(string[] args)
        {
            // Creates game object and disposes of it after leaving the scope
            using(Game game = new Game(500, 500))
            {
                // running the game
                game.Run();
            }
        }
    }
}