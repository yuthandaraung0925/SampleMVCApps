using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleMVCApps.Models;

namespace SampleMVCApps.Controllers{
 public class UserController : Controller{
    private readonly CompanyDBContext _context;

    public UserController(CompanyDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.ToListAsync();
        return View(users);
    }

    public async Task<IActionResult> AddOrEdit(int? userId)
    {
        ViewBag.PageName = userId == null ? "Create User" : "Edit User";
        ViewBag.IsEdit = userId == null ? false : true;
        if (userId == null)
        {
            return View();
        }
        else
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {   
                return NotFound();
            }
            return View(user);
        }
    }

    //AddOrEdit Post Method
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit(int userId, [Bind("UserId,Name,Designation,Address,Salary,JoiningDate")]
    User userData)
    {
        bool IsUserExist = false;
        User user = await _context.Users.FindAsync(userId);

        if (user != null)
        {
            IsUserExist = true;
        }
        else
        {
            user = new User();
        }

        if (ModelState.IsValid)
        {
            try
            {
                user.Name = userData.Name;
                user.Designation = userData.Designation;
                user.Address = userData.Address;
                user.Salary = userData.Salary;
                user.JoiningDate = userData.JoiningDate;

                if(IsUserExist)
                {
                    _context.Update(user);
                }
                else
                {
                    _context.Add(user);
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(userData);
    } 

    // Employee Details
    public async Task<IActionResult> Details(int? userId)
    {
        if (userId == null)
        {
            return NotFound();
        }
        var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == userId);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    } 

    // GET: Employees/Delete/1
    public async Task<IActionResult> Delete(int? userId)
    {
        if (userId == null)
        {
            return NotFound();
        }
        var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == userId);

        return View(user);
    }

    // POST: Employees/Delete/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    
  }
}