namespace DataHarvester.Worker.Core.Models
{
    /// <summary>
    /// Модель данных, представляющая результат сбора данных с веб-страницы.
    /// </summary>
    /// <param name="Title">Заголовок страницы.</param>
    /// <param name="Links">Коллекция всех гиперссылок, найденных на странице.</param
    public record HarvestData(string Title, List<string> Links);
}
