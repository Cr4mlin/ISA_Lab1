using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Presenter.ViewModels
{
    /// <summary>
    /// Базовый класс для всех ViewModel с реализацией INotifyPropertyChanged
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие изменения свойства для привязки данных
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Событие готовности ViewModel для создания View
        /// </summary>
        public event EventHandler? ViewModelReady;

        /// <summary>
        /// Вызывает событие изменения свойства
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Устанавливает значение свойства и вызывает событие изменения
        /// </summary>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Вызывает событие готовности ViewModel
        /// </summary>
        protected virtual void RaiseViewModelReady()
        {
            ViewModelReady?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Публичный метод для инициализации ViewModel (вызывает ViewModelReady)
        /// </summary>
        public void Initialize()
        {
            RaiseViewModelReady();
        }
    }
}
