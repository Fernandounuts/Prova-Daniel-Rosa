using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova_Rosa.Data;

namespace Prova_Rosa.Controllers;

public class ProvaController : Controller
{
	private readonly AppDbContext _context;

	public ProvaController(AppDbContext context)
	{
		_context = context;
	}

	public IActionResult Crete() {
		return View();
	}

	[HttpPost]

	public async Task<IActionResult> Create(long? id) {
		if(id == null) {
			return NotFound();
		}

		var post = await _context.Users.Where(usr => usr.Id == id).FirstOrDefaultAsync();

		if (post == null) {
			return NotFound();
		}

		if(ModelState.IsValid) {
			_context.Add(post);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Create));
		}
		return View(post);
	}

	[HttpGet]
	public async Task<IActionResult> Get(long? id) {
		if(id == null) {
			return NotFound();
		}

		var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

		if (user == null) {
			return NotFound();
		}

		return View(user);
	}

	[HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
			if(id == null) {
				return NotFound();
			}
            var user = await _context.Users.FindAsync(id);
			if(user==null) {
				return NotFound();
			}
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
}