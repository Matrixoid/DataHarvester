namespace DataHarvester.Contracts
{
    /// <summary>
    /// Контракт сообщения для передачи задачи на сбор данных через брокер сообщений.
    /// </summary>
    /// <param name="JobId">Уникальный идентификатор задачи.</param>
    /// <param name="Url">URL страницы, с которой будут собираться данные.</param>
    public record JobMessage(Guid JobId, string Url);
}
