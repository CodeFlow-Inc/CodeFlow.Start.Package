﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CodeFlow.Start.Package.Config;

/// <summary>
/// Configures the Swagger UI options for dynamic version handling and UI settings.
/// </summary>
public class SwaggerUIVersionConfig : IConfigureOptions<SwaggerUIOptions>
{
    /// <summary>
    /// Configures the Swagger UI options.
    /// </summary>
    /// <param name="options">The Swagger UI options to configure.</param>
    public void Configure(SwaggerUIOptions options)
    {
        // Register dynamic version handling in Swagger UI
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpensesControl API v1");

        // Additional Swagger UI settings
        options.DefaultModelsExpandDepth(-1); // Hides schema by default
        options.DisplayRequestDuration(); // Displays the request duration
        options.EnableDeepLinking(); // Enables deep linking within the Swagger UI

        options.RoutePrefix = "swagger"; // Sets the Swagger UI route prefix
    }
}
