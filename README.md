# DomainForwardingApp
Simple domain forwarding app using ASP.NET Core in combination with Azure Web Apps (can work on plain IIS as well) that can forward one domain to another domain.

How to use:
1. Add your domains to the domain register in Startup.cs
2. Create an Azure Web App with Custom Domains, add the to-forward domains to your list of custom domains of the Azure Web App. 
Add DNS records to enable this (see Azure Docs).
3. Publish DomainForwardingApp to your Azure Web App.
4. Incoming requests will be analyzed on hostname and forwarded if hostname exists in the domain list. 
5. If you have a frequently changing list of domains, change the data source for the domain list from a dictionary to a dynamic source (e.g. database) if you want to avoid publishing the project every time you add/update a domain.

Read my blogpost for a more detailed explanation at https://www.vandriel.me/forwarding-domain-names-with-aspnet-core-and-azure-web-apps