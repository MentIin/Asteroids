using System;
using CodeBase.UI.Binders.Main;
using TMPro;
using UniRx;

namespace CodeBase.UI.Binders
{
    public class TextBinder : IBinder, IObservable<string>
    {
        private readonly TextMeshProUGUI _textMeshPro;
        private readonly ReactiveProperty<string> _reactiveProperty;

        public TextBinder(TextMeshProUGUI textMeshPro, ReactiveProperty<string> reactiveProperty)
        {
            _textMeshPro = textMeshPro;
            _reactiveProperty = reactiveProperty;
        }
        public void Bind()
        {
            throw new System.NotImplementedException();
        }

        public void Unbind()
        {
            throw new System.NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            throw new NotImplementedException();
        }
    }
}