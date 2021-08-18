# MyApiGateway
My API Gateway with IdentityServer4 and Ocelot. 
There are several Microservices, such as  IdentityService, ClientService, ProductService and AgentService.
## Example to get token
URI: http://localhost:8800/identityservice/login
Body: 
{
	"UserName":"damienbod",
	"Password":"damienbod",
	"ClientId":"product.api.service"
}
## Example to get client values
URI: http://localhost:8800/clientservice/values
Headers:
Content-Type: application/json
Authorization: Bearer <Token>