using CodeBase.Interfaces.Infrastructure.Services.UI;

namespace CodeBase.UI.Controls
{
    public class MobileInputProvider : IMobileInputProvider
    {
        public IMobileInput MobileInput { get; private set; }
        public void Register(IMobileInput mobileInput)
        {
            MobileInput = mobileInput;
        }
    }
}