﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Index(string? nameFilter, string? emailFilter, string? phoneFilter, bool? isBlockedFilter, DateTime? registrationDateFilter)
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
                clients = clients.Where(c => c.RegistrationDate.Date == registrationDateFilter.Value.Date);
            }

            // adiciona os filtros como parâmetros de query string na URL
            ViewData["NameFilter"] = nameFilter;
            ViewData["EmailFilter"] = emailFilter;
            ViewData["PhoneFilter"] = phoneFilter;
            ViewData["registrationDateFilter"] = registrationDateFilter;

            return View(clients.ToList());
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
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,RegistrationDate,CPF_CNPJ,IsFree,IsBlocked,IsStateDocIndividual,TypePerson,StateDoc,BirthDate,Gender")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,RegistrationDate,CPF_CNPJ,IsFree,IsBlocked,IsStateDocIndividual,TypePerson,StateDoc,BirthDate,Gender")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
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
