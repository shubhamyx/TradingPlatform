using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TradingPlatform.Domain.Entities;
using TradingPlatform.Infrastructure.Identity;
using TradingPlatform.Web.Models;
using TradingPlatform.Infrastructure.Persistence;

namespace TradingPlatform.Web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly TradingPlatformDbContext _dbContext;

    public AccountController(
     UserManager<ApplicationUser> userManager,
     SignInManager<ApplicationUser> signInManager,
     TradingPlatformDbContext dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName,
            DateOfBirth = model.DateOfBirth
        };

        var result = await _userManager.CreateAsync(user, model.Password);



        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        await _userManager.AddToRoleAsync(user, "Trader");

        var wallet = new Wallet(Guid.Parse(user.Id), startingBalance: 100000m);
        _dbContext.Wallets.Add(wallet);
        await _dbContext.SaveChangesAsync();

        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction("Index", "Home");
    }
}  