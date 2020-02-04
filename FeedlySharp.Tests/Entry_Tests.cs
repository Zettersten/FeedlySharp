using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FeedlySharp.Tests
{
    public class Entry_Tests
    {
        [Fact]
        public void Should_get_single_entry()
        {
            var feedlySharp = Mocks.MockFeedlyHttpClient;
            var entry = feedlySharp.GetEntryContents("U0B9hzMPYzqby9veraV2nWqKr9ZiyWt5hu6xQaFsoPA=_16fe63b32f3:460aa57:fd9c96c2").GetAwaiter().GetResult();

            Assert.StartsWith("Advanced-Interview-Question", entry.Title);
        }

        [Fact]
        public void Should_get_two_entries()
        {
            var feedlySharp = Mocks.MockFeedlyHttpClient;
            var entry = feedlySharp.GetEntryContents("U0B9hzMPYzqby9veraV2nWqKr9ZiyWt5hu6xQaFsoPA=_16fe63b32f3:460aa57:fd9c96c2", "+mZBOJxcVsi38VOzTzRj0Ozru3ydXPuFDUHMMpHPbPc=_16ff335c76c:594070c:31d4c877").GetAwaiter().GetResult();

            Assert.True(entry.Count == 2);
        }
    }
}
