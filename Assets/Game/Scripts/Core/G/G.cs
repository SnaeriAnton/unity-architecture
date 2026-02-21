namespace Core.GSystem
{
    public static class G
    {
        public static Main Main { get; private set; }
        public static bool Initialized => Main != null;
        
        public static void Init(Main main) => Main = main;
        
        public static void Reset()
        {
            Main?.Dispose();
            Main = null;
        }
    }
}