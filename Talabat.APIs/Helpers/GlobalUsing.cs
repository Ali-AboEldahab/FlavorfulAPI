﻿global using AutoMapper;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using System.Security.Claims;
global using Talabat.APIs.DTOs;
global using Talabat.APIs.Errors;
global using Talabat.APIs.Extensions;
global using Talabat.Core.Entities.Identity;
global using Talabat.Core.Services;
global using Talabat.Core.Entities;
global using Talabat.Core.IRepository;
global using Talabat.Core.Entities.Order_Aggregate;
global using Talabat.Core.Specifications.ProductSpecifications;
global using Talabat.APIs.Helpers;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.EntityFrameworkCore;
global using System.Net;
global using System.Text.Json;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using StackExchange.Redis;
global using System.Text;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Talabat.APIs.Middlewares;
global using Talabat.Core;
global using Talabat.Repository;
global using Talabat.Repository.Data;
global using Talabat.Repository.Identity;
global using Talabat.Service;
global using Talabat.Core.IServices;
global using Order = Talabat.Core.Entities.Order_Aggregate.Order;
global using Stripe;
global using Product = Talabat.Core.Entities.Product;
global using Address = Talabat.Core.Entities.Order_Aggregate.Address;





