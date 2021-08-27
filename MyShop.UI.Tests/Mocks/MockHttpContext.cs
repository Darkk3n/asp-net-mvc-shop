using System.Web;

namespace MyShop.UI.Tests.Mocks
{
	public class MockHttpContext : HttpContextBase
	{
		private MockRequest request;
		private MockResponse response;
		private HttpCookieCollection cookies;

		public MockHttpContext() {
			cookies = new HttpCookieCollection();
			request = new MockRequest(cookies);
			response = new MockResponse(cookies);
		}

		public override HttpRequestBase Request => request;

		public override HttpResponseBase Response => response;
	}

	public class MockResponse : HttpResponseBase
	{
		private readonly HttpCookieCollection cookies;

		public MockResponse(HttpCookieCollection cookies) {
			this.cookies = cookies;
		}

		public override HttpCookieCollection Cookies => cookies;
	}

	public class MockRequest : HttpRequestBase
	{
		private readonly HttpCookieCollection cookies;

		public MockRequest(HttpCookieCollection cookies) {
			this.cookies = cookies;
		}

		public override HttpCookieCollection Cookies => cookies;
	}
}