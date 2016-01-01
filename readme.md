#Open API Router

Existing efforts to use the Open API format (fka Swagger) in ASP.NET Web API used the library Swashbuckle to generate a swagger document based on the information made available via the ApiExplorer.  Unfortunately due to the various routing mechanisms available in ASP.NET Web API there meta data in API Explorer does not always accurately represent the available resources and the opinions of the Open API specification prevent certain resource URLs from being accurately described.

This library takes a different approach to using the Open API format.  The API description document becomes a primary artifact of the implementation and is edited to describe the API.  A new vendor extension property `x-controller` can be used to identify which Web API controllers will be used to handle requests.  A new routing mechanism is provided to map incoming requests to controllers based on this augmented Open API description document.

The benefit of this approach is that Open API document is always an accurate description of the API.  There is never a mismatch between the Web API routing and the paths described in the document.


This project was inspired by the ideas of https://github.com/swagger-api/swagger-inflector

Current status of this project is just a proof of concept.

Routing is configured by passing a OpenAPI document stream:


         var stream = typeof(WebApiConfig).Assembly
               .GetManifestResourceStream("SampleApi.openapi.json");

            config.Routes.Add("OpenAPIRouter", new OpenApiRouter.OpenApiRouter(stream));

and the Open API paths can be bound to Controller classes using the x-controller property:

		{
		  "swagger": "2.0",
		  "info": {
			"title": "Sample API",
			"description": "",
			"version": "1.0"
		  },
		  "paths": {
			"/": {
			  "x-controller": "home",
			  "get": {
				"description": "Get home discovery doc",
				"operationId": "home",
				"responses": { }
			  }
			},
			"/resource": {
			  "x-controller": "resource",
			  "get": {
				"description": "Simple single resource",
				"operationId": "resource",
				"responses": { }
			  }
			},
			"/resource/{id}": {
			  "x-controller": "resourceclass",
			  "get": {
				"description": "class of resources",
				"operationId": "resourceclass",
				"responses": { }
			  }
			}
		  }
		}  

The actual implementation of the path matching and parameter extraction is based on the Tavis.UriTemplates library and as such supports a significant amount of the RFC6570 syntax.
Query parameters defined in the OpenAPI description are not yet supported.
