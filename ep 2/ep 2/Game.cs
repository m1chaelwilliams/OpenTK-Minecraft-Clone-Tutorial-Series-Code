using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Clone_Tutorial_Series_videoproj
{
    internal class Game : GameWindow
    {
        // width and height of screen
        int width, height;
        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.width = width;
            this.height = height;

            // center window
            CenterWindow(new Vector2i(width, height));
        }
    }
}
