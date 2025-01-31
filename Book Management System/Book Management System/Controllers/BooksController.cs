using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Book_Management_System.Data;
using Book_Management_System.Models;
using Book_Management_System.ViewModels;

namespace Book_Management_System.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.list = _context.BookAuthors.Where(ba => ba.BookId == id).Select(ba => ba.Author.Name).ToList();
            

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
        //    ViewData["authorId"] = new SelectList(_context.Authors, "Id", "Name");
            ViewBag.authorId = new SelectList(_context.Authors, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                if(_context.Books.Any(i => i.Title == bookViewModel.Title ))
                {
                    ViewBag.message = "faltu";
                    ViewBag.authorId = new SelectList(_context.Authors, "Id", "Name");
                    return View(bookViewModel);
                }
                Book book = new Book(bookViewModel.Title, bookViewModel.PublishYear);
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                foreach(var author in bookViewModel.authors)
                {
                    BookAuthor bookAuthor = new BookAuthor(book.Id, author); 
                    _context.BookAuthors.Add(bookAuthor);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.authorId = new SelectList(_context.Authors, "Id", "Name");
            return View(bookViewModel);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var book = _context.Books.Include(b => b.BookAuthors).FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.authorId = new MultiSelectList(_context.Authors, "Id", "Name", book.BookAuthors.Select(ba => ba.AuthorId));

            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookViewModel bookViewModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Books.Include(b => b.BookAuthors).FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    book.Title = bookViewModel.Title;
                    book.PublishYear = bookViewModel.PublishYear;

                    var authorList = _context.BookAuthors.Where(x => x.BookId == id).ToList();
                    _context.BookAuthors.RemoveRange(authorList);
                    await _context.SaveChangesAsync();

                    // Re-assign the selected authors
                    foreach (var authorId in bookViewModel.authors)
                    {
                        _context.BookAuthors.Add(new BookAuthor(id, authorId));
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(bookViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.authorId = new SelectList(_context.Authors, "Id", "Name", book.BookAuthors.Select(ba => ba.AuthorId));
            return View(bookViewModel);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
