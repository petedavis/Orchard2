﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Orchard.ContentTypes.Editors;
using Orchard.ContentTypes.RecipeSteps;
using Orchard.ContentTypes.Services;
using Orchard.Environment.Navigation;
using Orchard.Recipes;
using Orchard.Security.Permissions;

namespace Orchard.ContentTypes
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IContentDefinitionService, ContentDefinitionService>();
            services.AddScoped<IStereotypesProvider, DefaultStereotypesProvider>();
            services.AddScoped<IStereotypeService, StereotypeService>();
            services.AddScoped<IContentDefinitionDisplayHandler, ContentDefinitionDisplayCoordinator>();
            services.AddScoped<IContentDefinitionDisplayManager, DefaultContentDefinitionDisplayManager>();
            services.AddScoped<IContentPartDefinitionDisplayDriver, ContentPartSettingsDisplayDriver>();
            services.AddScoped<IContentTypeDefinitionDisplayDriver, ContentTypeSettingsDisplayDriver>();
            services.AddScoped<IContentTypeDefinitionDisplayDriver, DefaultContentTypeDisplayDriver>();
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, ContentTypePartSettingsDisplayDriver>();

            // TODO: Put in its own feature to be able to execute this recipe without having to enable
            // Content Types management UI
            services.AddRecipeExecutionStep<ContentDefinitionStep>();
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaRoute(
                name: "EditField",
                areaName: "Orchard.ContentTypes",
                template: "Admin/ContentTypes/EditField/{id}/{name}",
                defaults: new { controller = "Admin", action = "EditField" }
            );

            routes.MapAreaRoute(
                name: "EditTypePart",
                areaName: "Orchard.ContentTypes",
                template: "Admin/ContentTypes/Edit/{id}/{name}",
                defaults: new { controller = "Admin", action = "EditTypePart" }
            );

            routes.MapAreaRoute(
                name: "RemovePart",
                areaName: "Orchard.ContentTypes",
                template: "Admin/ContentTypes/RemovePart/{id}/{name}",
                defaults: new { controller = "Admin", action = "RemovePart" }
            );

            routes.MapAreaRoute(
                name: "AdminContentTypes",
                areaName: "Orchard.ContentTypes",
                template: "Admin/ContentTypes/{action}",
                defaults: new { controller = "Admin" }
            );
        }
    }
}
