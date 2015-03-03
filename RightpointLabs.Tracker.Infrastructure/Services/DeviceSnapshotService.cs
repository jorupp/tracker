using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using RightpointLabs.Tracker.Domain;
using RightpointLabs.Tracker.Domain.Models;
using RightpointLabs.Tracker.Domain.Services;

namespace RightpointLabs.Tracker.Infrastructure.Services
{
    public class DeviceSnapshotService : IDeviceSnapshotService
    {
        private readonly string _serverUrl;
        private readonly string _username;
        private readonly string _password;
        private readonly string _siteId;

        public DeviceSnapshotService(string serverUrl, string username, string password, string siteId)
        {
            _serverUrl = serverUrl;
            _username = username;
            _password = password;
            _siteId = siteId;
        }

        public async Task<DeviceSnapshot> TakeSnapshot()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;

            using (var h = new HttpClientHandler())
            {
                h.UseCookies = true;
                var cookies = new CookieContainer();
                h.CookieContainer = cookies;

                using (var ctx = new HttpClient(h))
                {
                    var loginUri = new Uri(new Uri(_serverUrl), "/LOGIN");
                    var loginResult = await ctx.PostAsync(loginUri, new FormUrlEncodedContent(new Dictionary<string, string>()
                    {
                        {"credential_0", _username},
                        {"credential_1", _password},
                        {"destination", "/index.html"},
                    }));
                    if (!loginResult.IsSuccessStatusCode)
                    {
                        throw new ApplicationException(await loginResult.Content.ReadAsStringAsync());
                    }
                
                    var url = new Uri(new Uri(_serverUrl), "/visualrf/site/" + _siteId + "?clients=1");
                    // it only talks to browsers, so... let's pretend to be Chrome
                    ctx.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.115 Safari/537.36");
                    var result = await ctx.GetAsync(url);
                    if (!result.IsSuccessStatusCode)
                    {
                        throw new ApplicationException(await result.Content.ReadAsStringAsync());
                    }

                    using (var s = await result.Content.ReadAsStreamAsync())
                    {
                        var xml = XElement.Load(s);

                        return new DeviceSnapshot()
                        {
                            Timestamp = DateTime.UtcNow,
                            Devices = xml.XPathSelectElements("//client").Select(ParseDeviceState).ToArray(),
                        };
                    }
                }
            }
        }

        private DeviceState ParseDeviceState(XElement e)
        {
            /*
            client [ ap_id=5 ap_name=AP03 ap_site_id=890d3ebd-629f-4e26-b06a-e14e3388e48e building=29 N Wacker campus=Chicago catalog_device_type=default 
             * client-health=53 floor=Floor 4 group=Rightpoint Consulting 
             * ip=10.10.1.252 mac=40:0E:85:13:0E:03 monitoring=false 
             * name=android-dc35fd789c5343a1 operating_phy=n site_id=890d3ebd-629f-4e26-b06a-e14e3388e48e vendor= vendor_device_type=Android ]
             * */
            return new DeviceState()
            {
                Date = (DateTime) e.Element("date"),
                AccessPointName = (string) e.Attribute("ap_name"),
                ClientHealth = (int) e.Attribute("client-health"),
                IpAddress = (string) e.Attribute("ip"),
                MacAddress = (string) e.Attribute("mac"),
                Name = (string) e.Attribute("name"),
                Vendor = (string) e.Attribute("vendor"),
                VendorDeviceType = (string) e.Attribute("vendor_device_type"),
                Bandwidth = (int) e.Element("bandwidth"),
                Signal = (int) e.Element("signal"),
                X = e.Element("x").ChainIfNotNull(i => (decimal?)i),
                Y = e.Element("y").ChainIfNotNull(i => (decimal?)i),
            };
        }
    }
}
    