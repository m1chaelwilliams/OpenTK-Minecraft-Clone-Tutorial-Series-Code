
namespace openTK_Minecraft_Clone_Tutorial_Series
{

    class Program
    {
        static void Main(string[] args)
        {
            using(Game game = new Game(500,500))
            {
                game.Run();
            }
        }
    }

}