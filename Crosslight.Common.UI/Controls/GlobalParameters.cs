namespace Crosslight.Common.UI.Controls
{
    public class GlobalParameters
    {
        public static GlobalParameters Instance => _instance;

        public int WheelScrollLines { get; }

        private GlobalParameters()
        {
            WheelScrollLines = 3;
        }

        private static readonly GlobalParameters _instance = new GlobalParameters();
    }
}
