using OpenQA.Selenium;
using System.Globalization;

namespace Ingestion.PageObjects
{
    public class MemberListItem : PageObject
    {
        private readonly IWebElement _memberListItemElem;

        private IWebElement NameText => _memberListItemElem.FindElement(By.CssSelector("td:nth-child(1) > span:nth-child(2)"));
        private IWebElement XPText => _memberListItemElem.FindElement(By.CssSelector("td:nth-child(2)"));
        private IWebElement KillsText => _memberListItemElem.FindElement(By.CssSelector("td:nth-child(3)"));
        private IWebElement DeathsText => _memberListItemElem.FindElement(By.CssSelector("td:nth-child(4)"));
        private IWebElement KDRText => _memberListItemElem.FindElement(By.CssSelector("td:nth-child(5)"));

        public MemberListItem(IWebElement memberListItemElem, IWebDriver webDriver, string url) : base(webDriver, url)
        {
            _memberListItemElem = memberListItemElem;
        }

        public string Name()
        {
            return NameText.Text;
        }

        public int Xp()
        {
            return int.Parse(XPText.Text, NumberStyles.Integer | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
        }

        public int Kills()
        {
            return int.Parse(KillsText.Text, NumberStyles.Integer | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
        }

        public int Deaths()
        {
            return int.Parse(DeathsText.Text, NumberStyles.Integer | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
        }

        public void Click()
        {
            _memberListItemElem.Click();
        }

        protected override void AssertOnScreenImpl()
        {
            throw new System.NotImplementedException();
        }
    }
}