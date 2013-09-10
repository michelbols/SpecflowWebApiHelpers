using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TechTalk.SpecFlow;

namespace WebApiTests.HelperSteps
{
  [Binding]
  public class HttpClientSteps
  {

    public const string CURRENTURI = "HttpClientSteps_CurrentUri";
    public const string CURRENTRESPONSE = "HttpClientSteps_CurrentResponse";

    [Given(@"I make a new request to (.*) on port (\d+)")]
    public void IMakeANewHttpRequestToHostOnPort(string host, int port)
    {
      UriBuilder builder = new UriBuilder();
      builder.Host = host;
      builder.Port = port;
      ScenarioContext.Current.Add(CURRENTURI, builder);
      ScenarioContext.Current.Remove(CURRENTRESPONSE);
    }

    [Given(@"the path is (.*)")]
    public void ThePathIs(string path)
    {
      UriBuilder builder = ScenarioContext.Current.Get<UriBuilder>(CURRENTURI);
      builder.Path = path;
    }

    [Given(@"I add the following fields as query parameters")]
    public void IAddTheFollowingFieldsAsQueryParameters(Table fields)
    {
      UriBuilder builder = ScenarioContext.Current.Get<UriBuilder>(CURRENTURI);
      StringBuilder sBuild = new StringBuilder();
      foreach (var row in fields.Rows)
      {
        sBuild.Append(string.Format("{0}={1}&", HttpUtility.UrlEncode(row["key"]), HttpUtility.UrlEncode(row["value"])));
      }
      builder.Query += sBuild;
    }

    [When(@"the request has compeleted")]
    public void TheRequestHasCompleted()
    {
      UriBuilder builder = ScenarioContext.Current.Get<UriBuilder>(CURRENTURI);
      using (HttpClient client = new HttpClient())
      {
        HttpResponseMessage response = client.GetAsync(builder.Uri).Result;
        ScenarioContext.Current.Add(CURRENTRESPONSE, response);
      }
    }

    [Then(@"the status code should be (\d+)")]
    public void TheStatusCodeShouldBe(int statusCode)
    {
      var response = ScenarioContext.Current.Get<HttpResponseMessage>(CURRENTRESPONSE);

      Assert.Equal(statusCode, (int)response.StatusCode);
    }


  }
}
