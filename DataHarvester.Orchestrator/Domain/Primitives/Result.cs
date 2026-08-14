namespace DataHarvester.Orchestrator.Domain.Primitives
{
    public class Result
    {
        /// <summary>
        /// Успешно ли выполнена операция.
        /// </summary>
        public bool IsSuccess { get; }
        /// <summary>
        /// Сообщение об ошибке, если операция выполнена неуспешно.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Инициализирует экземпляр класса, задавая ему значения <paramref name="isSuccess"/> и <paramref name="error"/>.
        /// </summary>
        /// <param name="isSuccess">Успешно ли выполнена операция.</param>
        /// <param name="error">Сообщение об ошибке, если операция выполнена неуспешно.</param>
        /// <exception cref="InvalidOperationException">Бросается, когда успешное выполнение содержит сообщение об ошибке, либо, когда оно отсутствует
        /// в случае неуспешного выполнения.</exception>
        protected Result(bool isSuccess, string error = "")
        {
            if (isSuccess && !string.IsNullOrEmpty(error))
            {
                throw new InvalidOperationException("Успешный результат не может содержать текст ошибки.");
            }

            if (!isSuccess && string.IsNullOrEmpty(error))
            {
                throw new InvalidOperationException("Неудачный результат должен содержать информацию об ошибке.");
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Создаёт экземпляр успешно выполненной операции.
        /// </summary>
        /// <returns><see cref="Result"/>.</returns>
        public static Result Success() => new Result(true);

        /// <summary>
        /// Создаёт экземпляр провальной операции.
        /// </summary>
        /// <param name="error">Сообщение об ошибке.</param>
        /// <returns><see cref="Result"/>.</returns>
        public static Result Failure(string error) => new Result(false, error);
    }
}
