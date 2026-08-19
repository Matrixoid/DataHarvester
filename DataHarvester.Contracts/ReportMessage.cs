using System;
using System.Collections.Generic;
using System.Text;

namespace DataHarvester.Contracts
{
    /// <summary>
    /// Сообщение, хранящее отчёт о проделанной задаче.
    /// </summary>
    /// <param name="JobId">Идентификатор задачи.</param>
    /// <param name="IsSuccess">Числовой показатель результата. 0 - неудача, 1 - успех.</param>
    /// <param name="Title">Название страницы, с которой собирали данные.</param>
    /// <param name="LinksCount">Число собранных ссылок.</param>
    /// <param name="ErrorMessage">Сообщение об ошибке.</param>
    public record ReportMessage(Guid JobId, bool IsSuccess = false, string Title = "", int LinksCount = 0, string? ErrorMessage = null);
}
