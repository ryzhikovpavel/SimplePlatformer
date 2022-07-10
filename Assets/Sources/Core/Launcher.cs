using Sources.UI;
using UnityEngine;

namespace Sources
{
    public static class Launcher
    {
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            Game.UI.Find<ScreenMenu>().Show();
        }
    }
}