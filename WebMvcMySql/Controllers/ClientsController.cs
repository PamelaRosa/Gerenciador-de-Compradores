using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMvcMySql.Data;
using WebMvcMySql.Models;

namespace WebMvcMySql.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Clients e/ou Filtros
        public async Task<IActionResult> Index(string? nameFilter, string? emailFilter, string? phoneFilter, bool? isBlockedFilter, DateTime? registrationDateFilter, DateTime? registrationDateFilterEnd, int page = 1, int pageSize = 20)
        {
            var clients = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                clients = clients.Where(c => c.Name.Contains(nameFilter));
            }

            if (!string.IsNullOrEmpty(emailFilter))
            {
                clients = clients.Where(c => c.Email.Contains(emailFilter));
            }

            if (!string.IsNullOrEmpty(phoneFilter))
            {
                clients = clients.Where(c => c.Phone.Contains(phoneFilter));
            }

            if (isBlockedFilter.HasValue)
            {
                clients = clients.Where(c => c.IsBlocked == isBlockedFilter.Value);
            }

            if (registrationDateFilter.HasValue)
            {
                clients = clients.Where(c => c.RegistrationDate.Date >= registrationDateFilter.Value.Date);
            }

            if (registrationDateFilterEnd.HasValue)
            {
                clients = clients.Where(c => c.RegistrationDate.Date <= registrationDateFilterEnd.Value.Date);
            }

            // contar o número total de clientes que correspondem aos filtros
            int totalClients = await clients.CountAsync();

            // calcular número total de páginas
            int totalPages = (int)Math.Ceiling((double)totalClients / pageSize);

            // verifique se o número da página está dentro do intervalo
            page = Math.Max(1, Math.Min(totalPages, page));

            // calcular o número de clientes a pular
            int skip = (page - 1) * pageSize;

            // Aplicar e ordenar
            List<Client> paginatedClients = await clients
                .OrderBy(c => c.Name)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            // Passar dados para a view
            ViewData["NameFilter"] = nameFilter;
            ViewData["EmailFilter"] = emailFilter;
            ViewData["PhoneFilter"] = phoneFilter;
            ViewData["RegistrationDateFilter"] = registrationDateFilter;
            ViewData["IsBlockedFilter"] = isBlockedFilter;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;

            return View(paginatedClients);
        }


        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,RegistrationDate,CPF_CNPJ,IsFree,IsBlocked,IsStateDocIndividual,TypePerson,StateDoc,BirthDate,Gender,Password,ConfirmPassword")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (!string.IsNullOrEmpty(client.Password))
                    {
                        if (client.Password.Length < 8 || client.Password.Length > 15)
                        {
                            ModelState.AddModelError(nameof(client.Password), "A senha deve ter entre 8 e 15 caracteres.");
                            return View(client);
                        }

                    }

                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(client);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao salvar o cliente: " + ex.Message);
                Console.WriteLine(ex.ToString());

                return View(client);
            }
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,RegistrationDate,CPF_CNPJ,IsFree,IsBlocked,IsStateDocIndividual,TypePerson,StateDoc,BirthDate,Gender,Password,ConfirmPassword")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingClient = await _context.Clients.FindAsync(id);
                    if (existingClient == null)
                    {
                        return NotFound();
                    }

                    existingClient.Name = client.Name;
                    existingClient.Email = client.Email;
                    existingClient.Phone = client.Phone;
                    existingClient.RegistrationDate = client.RegistrationDate;
                    existingClient.CPF_CNPJ = client.CPF_CNPJ;
                    existingClient.IsFree = client.IsFree;
                    existingClient.IsBlocked = client.IsBlocked;
                    existingClient.IsStateDocIndividual = client.IsStateDocIndividual;
                    existingClient.TypePerson = client.TypePerson;
                    existingClient.StateDoc = client.StateDoc;
                    existingClient.BirthDate = client.BirthDate;
                    existingClient.Gender = client.Gender;
                    existingClient.Password = client.Password;
                    existingClient.ConfirmPassword = client.ConfirmPassword;

                    _context.Update(existingClient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }


        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
