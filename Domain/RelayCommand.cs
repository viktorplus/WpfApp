using System;
using System.Windows.Input;

namespace WpfApp.Domain
{
    // Класс RelayCommand реализует интерфейс ICommand для обработки команд в WPF.
    public class RelayCommand : ICommand
    {
        // Делегат Action<object> представляет метод, который не возвращает значение и принимает объект в качестве параметра.
        private readonly Action<object> _execute;

        // Делегат Predicate<object> представляет метод, который принимает объект в качестве параметра и возвращает логическое значение.
        // Он используется для определения возможности выполнения команды.
        private readonly Predicate<object>? _canExecute;

        // Конструктор класса RelayCommand.
        // Принимает делегат Action<object> для выполнения команды и делегат Predicate<object> для проверки возможности выполнения команды.
        public RelayCommand(Action<object> execute, Predicate<object>? canExecute = null)
        {
            // Проверка, что переданный делегат для выполнения команды не равен null.
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));

            // Присвоение переданного делегата для проверки возможности выполнения команды.
            _canExecute = canExecute;
        }

        // Метод CanExecute проверяет возможность выполнения команды.
        // Вызывается перед выполнением команды.
        public bool CanExecute(object parameter)
        {
            // Если делегат для проверки возможности выполнения команды равен null, то команда всегда может быть выполнена.
            // В противном случае вызывается делегат для проверки возможности выполнения команды с переданным параметром.
            return _canExecute == null || _canExecute(parameter);
        }

        // Метод Execute выполняет команду.
        // Вызывается при выполнении команды.
        public void Execute(object parameter)
        {
            // Вызывается делегат для выполнения команды с переданным параметром.
            _execute(parameter);
        }

        // Событие CanExecuteChanged вызывается при изменении возможности выполнения команды.
        public event EventHandler? CanExecuteChanged;

        // Метод RaiseCanExecuteChanged вызывается для уведомления системы, что возможность выполнения команды изменилась.
        public void RaiseCanExecuteChanged()
        {
            // Вызывается событие CanExecuteChanged.
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
