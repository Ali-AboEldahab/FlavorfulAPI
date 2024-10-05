global using Microsoft.AspNetCore.Identity;
global using Microsoft.Extensions.Configuration;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using Flavorful.Core.Entities.Identity;
global using Flavorful.Core.Services;
global using Flavorful.Core;
global using Flavorful.Core.Entities;
global using Flavorful.Core.Entities.Order_Aggregate;
global using Flavorful.Core.IRepository;
global using Flavorful.Core.Specifications.OrderSpecifications;
global using Flavorful.Core.Specifications.ProductSpecifications;
global using Flavorful.Core.IServices;
global using Stripe;
global using Product = Flavorful.Core.Entities.Product;
global using Address = Flavorful.Core.Entities.Order_Aggregate.Address;



