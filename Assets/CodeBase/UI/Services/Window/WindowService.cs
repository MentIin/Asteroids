using System;
using CodeBase.UI.Services.Factories;

namespace CodeBase.UI.Services.Window
{
    public class WindowService
    {
        private readonly UIFactory uiFactory;

        public WindowService(UIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }

        public void Open(WindowType window)
        {
            switch (window)
            {
                case WindowType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(window), window, null);
            }
        }
    }
}