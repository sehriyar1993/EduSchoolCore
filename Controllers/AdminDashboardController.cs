using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EduSchoolCore.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminDashboardController : Controller
	{
		CourcesManager am = new CourcesManager(new EFCourseDal());

		Context c = new Context();
		public async Task<IActionResult> Index()
		{

			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://currency-exchange.p.rapidapi.com/exchange?from=USD&to=AZN&q=1.0"),
				Headers =
	{
		{ "X-RapidAPI-Key", "91f0b3875emsh29d9702062a63e5p118be4jsn91c12885f9ee" },
		{ "X-RapidAPI-Host", "currency-exchange.p.rapidapi.com" },
	},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				ViewBag.currency = body;
			}
			ViewBag.user = c.Users.Count();
			ViewBag.cateqory = c.Categories.Count();
			ViewBag.product = c.Blogs.Count();
			ViewBag.brend = c.Courses.Count();
			var values = am.TGetAll();
			return View(values);
		}
	}
}
