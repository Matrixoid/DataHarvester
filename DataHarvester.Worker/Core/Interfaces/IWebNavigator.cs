using DataHarvester.Worker.Core.Models;

namespace DataHarvester.Worker.Core.Interfaces
{
    /// <summary>
    /// Контракт для движка веб-навигации и сбора данных.
    /// </summary>
    public interface IWebNavigator
    {
        /// <summary>
        /// Асинхронно извлекает данные с указанного веб-ресурса.
        /// </summary>
        /// <param name="Url">URL страницы, с которой происходит сбор данных.</param>
        /// <param name="cancellationToken">Токен для отмены операции.</param>
        /// <returns>Собранные данные в формате <see cref="HarvestData"/>.</returns>
        Task<HarvestData> ExtractDataAsync(string Url, CancellationToken cancellationToken);
    }
}
