#Contract Router

This library provides an alternative routing mechanism for ASP.NET Web API.  The routes are created based on a API description contract.  Currently, Open API is the only supported format but the library is designed to be able to support alternative API Description formats like API Blueprint, WADL, RAML, RADL, etc.

This library is intended to support the notion of "contract first" API design.  The idea behind contract first is that you create a contract that describes the API before creating the implementation and ensure that the API only supports what is defined in the API.  This approach makes it easier to track where the client/server coupling exists because it is defined in the contract.  

One of the advantages of this approach over alternative approaches that infer a contract from the implementation is that it is easy to create resources in ASP.NET Web API that cannot be described by some API Description languages because it does not fit with the opinions of that description language. 

This project was inspired by the ideas of https://github.com/swagger-api/swagger-inflector

Current status of this project is just a proof of concept.

Routing is configured by using a OpenAPI document stream create an OpenApiRouter instance which is then configured as the main router:


         var stream = typeof(WebApiConfig).Assembly
               .GetManifestResourceStream("SampleApi.openapi.json");
            config.Routes.Add("default", new OpenApiRouter.OpenApiRouter(stream));

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
