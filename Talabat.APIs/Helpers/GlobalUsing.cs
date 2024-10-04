global using AutoMapper;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using System.Security.Claims;
global using Flavorful.APIs.DTOs;
global using Flavorful.APIs.Errors;
global using Flavorful.APIs.Extensions;
global using Flavorful.Core.Entities.Identity;
global using Flavorful.Core.Services;
global using Flavorful.Core.Entities;
global using Flavorful.Core.IRepository;
global using Flavorful.Core.Entities.Order_Aggregate;
global using Flavorful.Core.Specifications.ProductSpecifications;
global using Flavorful.APIs.Helpers;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.EntityFrameworkCore;
global using System.Net;
global using System.Text.Json;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using StackExchange.Redis;
global using System.Text;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Flavorful.APIs.Middlewares;
global using Flavorful.Core;
global using Flavorful.Repository;
global using Flavorful.Repository.Data;
global using Flavorful.Repository.Identity;
global using Flavorful.Service;
global using Flavorful.Core.IServices;
global using Order = Flavorful.Core.Entities.Order_Aggregate.Order;
global using Stripe;
global using Product = Flavorful.Core.Entities.Product;
global using Address = Flavorful.Core.Entities.Order_Aggregate.Address;





