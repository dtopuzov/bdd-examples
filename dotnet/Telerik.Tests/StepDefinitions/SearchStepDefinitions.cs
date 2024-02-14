using Telerik.Tests.Drivers;
using Telerik.Tests.PageObjects;

namespace Telerik.Tests.StepDefinitions
{
    [Binding]
    public class SearchStepDefinitions
    {
        private readonly SearchPage searchPage;

        public SearchStepDefinitions(BrowserDriver browserDriver)
        {
            searchPage = new SearchPage(browserDriver.Current);
        }

        [Given(@"I am on the search page")]
        public void GivenIAmOnTheSearchPage()
        {
            searchPage.Open();
        }

        [When(@"I search for (.*)")]
        public void WhenISearchFor(string text)
        {
            searchPage.SearchFor(text);
        }

        [Then(@"I should see (.*) in results")]
        public void ThenIShouldSeeLinkInResults(string link)
        {
            searchPage.WaitForResult(link);
        }
    }
}
