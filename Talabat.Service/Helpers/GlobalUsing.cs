global using Microsoft.AspNetCore.Identity;
global using Microsoft.Extensions.Configuration;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using Talabat.Core.Entities.Identity;
global using Talabat.Core.Services;
global using Talabat.Core;
global using Talabat.Core.Entities;
global using Talabat.Core.Entities.Order_Aggregate;
global using Talabat.Core.IRepository;
global using Talabat.Core.Specifications.OrderSpecifications;
global using Talabat.Core.Specifications.ProductSpecifications;
global using Talabat.Core.IServices;
global using Stripe;
global using Product = Talabat.Core.Entities.Product;
global using Address = Talabat.Core.Entities.Order_Aggregate.Address;



