using DataHarvester.Worker.Core.Interfaces;
using DataHarvester.Worker.Core.Models;
using Microsoft.Playwright;

namespace DataHarvester.Worker.Infrastructure.Harvester
{
    /// <summary>
    /// Класс реализующий интерфейс <see cref="IWebNavigator"/>.
    /// </summary>
    public class HarvesterEngine : IWebNavigator
    {
        /// <inheritdoc/>
        public async Task<HarvestData> ExtractDataAsync(string url, CancellationToken cancellationToken)
        {
            string title = "";
            List<string> links = new List<string>();

            using (var playwright = await Playwright.CreateAsync())
            {
                await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
                var context = await browser.NewContextAsync();
                var page = await context.NewPageAsync();
                await page.GotoAsync(url);
                title = await page.TitleAsync();
                foreach (var link in await page.Locator("a").AllAsync())
                {
                    string? href = await link.GetAttributeAsync("href");
                    if (!string.IsNullOrEmpty(href)) {
                        links.Add(href);
                    }
                }

                return new HarvestData(title, links);
            }
        }
    }
}
