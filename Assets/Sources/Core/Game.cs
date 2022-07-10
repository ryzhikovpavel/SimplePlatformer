using Sources.UI;

namespace Sources
{
    public static class Game
    {
        public static UiManager UI => SceneReference<UiManager>.Get();
        public static World World => SceneReference<World>.Get();
        public static Sound Sound => SceneReference<Sound>.Get();
        public static MobileInput MobileInput => SceneReference<MobileInput>.Get();
    }
}