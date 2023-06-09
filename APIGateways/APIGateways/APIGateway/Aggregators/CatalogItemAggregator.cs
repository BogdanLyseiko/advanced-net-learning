using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace APIGateway.Aggregators
{
    public class CatalogItemAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            string one = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            string two = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

            StringBuilder contentBuilder = new();

            // to siplify we will return string only, in real project we will need to return needed mapped object
            contentBuilder.AppendLine(one);
            contentBuilder.AppendLine(two);

            StringContent stringContent = new(contentBuilder.ToString())
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
