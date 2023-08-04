using EduSchoolCore.EL.AllClass;
using EduSchoolCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EduSchoolCore.Controllers
{
	[Authorize(Roles = "Admin")]

	public class AdminRoleController : Controller
	{
		
			private readonly RoleManager<AppRole> _roleManager;
			private readonly UserManager<AppUser> _userManager;

			public AdminRoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
			{
				_roleManager = roleManager;
				_userManager = userManager;
			}

			public IActionResult Index()
			{
				var values = _roleManager.Roles.ToList();
				return View(values);
			}
			public IActionResult Users()
			{
				var values = _userManager.Users.ToList();
				return View(values);
			}
			public async Task<IActionResult> Delete(int id)
			{
				var values = _roleManager.Roles.FirstOrDefault(c => c.Id == id);
				await _roleManager.DeleteAsync(values);
				return RedirectToAction("Index");
			}
			[HttpGet]
			public IActionResult Create()
			{
				return View();
			}
			[HttpPost]
			public async Task<IActionResult> Create(CreateRoleViewModel p)
			{
				AppRole r = new AppRole()
				{
					Name = p.RoleName
				};
				var result = await _roleManager.CreateAsync(r);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					return View();

				}

			}
			[HttpGet]
			public async Task<IActionResult> AssingRole(int id)
			{
				var user = _userManager.Users.FirstOrDefault(c => c.Id == id);
				TempData["userId"] = user.Id;
				var role = _roleManager.Roles.ToList();
				var uroles = await _userManager.GetRolesAsync(user);
				List<AssingRoleViewModel> assingRoleViewModels = new List<AssingRoleViewModel>();
				foreach (var i in role)
				{
					AssingRoleViewModel m = new AssingRoleViewModel();
					m.RoleId = i.Id;
					m.RoleName = i.Name;
					m.RoleExist = uroles.Contains(i.Name);
					assingRoleViewModels.Add(m);
				}
				return View(assingRoleViewModels);
			}
			[HttpPost]
			public async Task<IActionResult> AssingRole(List<AssingRoleViewModel> p)
			{
				var userid = (int)TempData["userId"];
				var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);
				foreach (var x in p)
				{
					if (x.RoleExist)
					{
						await _userManager.AddToRoleAsync(user, x.RoleName);
					}
					else
					{
						await _userManager.RemoveFromRoleAsync(user, x.RoleName);
					}
				}
				return RedirectToAction("Index");
			}
			[HttpGet]
			public IActionResult UpdateRole(int id)
			{
				var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
				UpdateRoleViewModel vm = new UpdateRoleViewModel
				{
					RoleId = role.Id,
					RoleName = role.Name,
				};
				return View(vm);
			}
			[HttpPost]
			public async Task<IActionResult> UpdateRole(UpdateRoleViewModel p)
			{
				var values = _roleManager.Roles.FirstOrDefault(x => x.Id == p.RoleId);
				values.Name = p.RoleName;
				await _roleManager.UpdateAsync(values);
				return RedirectToAction("Index");
			}
		}
	
}
