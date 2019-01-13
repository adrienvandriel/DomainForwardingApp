using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace DomainForwardingApp
{
    public class Startup
    {
        private static IDictionary<string, string> Domains { get; set; }

        public Startup()
        {
            RegisterDomains();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(context =>
            {
                string originDomain = context.Request.Host.Value;
                bool isRegisteredDomain = TryGetTargetDomain(originDomain, out string targetDomain);
                if (isRegisteredDomain)
                {
                    context.Response.Redirect(targetDomain, permanent: true);
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
                return Task.FromResult<object>(null);
            });
        }

        private static bool TryGetTargetDomain(string originDomain, out string targetDomain)
        {
            bool isRegisteredDomain;
            if (Domains.ContainsKey(originDomain))
            {
                targetDomain = Domains[originDomain];
                isRegisteredDomain = true;
            }
            else
            {
                targetDomain = null;
                isRegisteredDomain = false;
            }

            return isRegisteredDomain;
        }

        private static void RegisterDomains()
        {
            const string mainDomain = "https://www.maindomain.com";

            //Key: origin domain, Value: target domain
            Domains = new Dictionary<string, string>();
            Domains.Add("otherdomain.com", mainDomain);
            Domains.Add("www.otherdomain.com", mainDomain);
        }
    }
}
