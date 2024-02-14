using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Telerik.Tests.PageObjects
{
    /// <summary>
    /// Search Page Object
    /// </summary>
    public class SearchPage
    {
        private const string Url = "https://www.telerik.com/search";
        private readonly IWebDriver _webDriver;
        public const int DefaultWaitInSeconds = 20;

        public SearchPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private IWebElement SearchInput => _webDriver.FindElement(By.CssSelector("input[type=search]"));
        private IWebElement ResultElement => _webDriver.FindElement(By.CssSelector("ul.TK-Search-Results-List li:nth-of-type(1) a.TK-Search-Results-List-Item-A"));

        public void Open()
        {
            _webDriver.Url = Url;
        }

        public void SearchFor(string text)
        {
            SearchInput.Clear();
            SearchInput.SendKeys(text);
            SearchInput.SendKeys(Keys.Enter);
        }

        public string WaitForResult(string link)
        {
            //Wait for the result to be not empty
            return WaitUntil(() =>
            ResultElement.GetAttribute("href"), result => result == link);
        }

        /// <summary>
        /// Helper method to wait until the expected result is available on the UI
        /// </summary>
        /// <typeparam name="T">The type of result to retrieve</typeparam>
        /// <param name="getResult">The function to poll the result from the UI</param>
        /// <param name="isResultAccepted">The function to decide if the polled result is accepted</param>
        /// <returns>An accepted result returned from the UI. If the UI does not return an accepted result within the timeout an exception is thrown.</returns>
        private T WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted) where T : class
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
            Func<IWebDriver, T> condition = driver =>
                        {
                            var result = getResult();
                            if (!isResultAccepted(result))
                            {
                                return default;
                            }

                            return result;
                        };
            return wait.Until(condition);
        }
    }
}