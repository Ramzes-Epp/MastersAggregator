using MastersAggregatorService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

public abstract class BaseController<T> : Controller where T: BaseModel
{
    
}