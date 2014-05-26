using System;
using System.Linq;
using System.Reflection;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Responses;
using Nancy.TinyIoc;
using Nancy.ViewEngines;

namespace OSHNWVBA
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        #region Part 1

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(OnConfigurationBuilder); }
        }

        private static void OnConfigurationBuilder(NancyInternalConfiguration config)
        {
            config.ViewLocationProvider = typeof (ResourceViewLocationProvider);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            var assembly = GetType().Assembly;

            // Assembly cannot begin with Nancy or it will reject request.
            ResourceViewLocationProvider.RootNamespaces.Add(assembly, assembly.GetName().Name);
        }

        #endregion

        #region Part 2

        // Important to server various items like .js .png etc.

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);
            var assembly = GetType().Assembly;
            conventions.StaticContentsConventions.Add(AddStaticResourcePath("static", assembly, assembly.GetName().Name));
        }

        private static Func<NancyContext, string, Response> AddStaticResourcePath(string staticPath, Assembly assembly, string namespacePrefix)
        {
            return (context, s) =>
            {
                var path = context.Request.Path;
                var name = path.Replace('/', '.').TrimStart('.');
                var resourcePath = namespacePrefix + "." + staticPath;

                // Important to return null so that it passes through to the other resource providers
                if (!assembly.GetManifestResourceNames().Contains(resourcePath + "." + name))
                {
                    return null;
                }

                var response = new EmbeddedFileResponse(assembly, resourcePath, name);

                // Check ETag values to generate 304NotModified
                string currentFileEtag;
                if (response.Headers.TryGetValue("ETag", out currentFileEtag))
                {
                    if (context.Request.Headers.IfNoneMatch.Contains(currentFileEtag))
                    {
                        return new Response {StatusCode = HttpStatusCode.NotModified, ContentType = response.ContentType};
                    }
                }

                return response;
            };
        }

        #endregion
    }
}