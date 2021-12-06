using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crud3.Data;
using Crud3.Models;
using Microsoft.AspNetCore.Authorization;

namespace Crud3.Controllers { 
        


    
    public class ToDoListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public string Alteração { get; private set; }

        public ToDoListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDoLists
        public async Task<IActionResult> Index()
        {
            _context.Auditoria.Add(new Auditoria
            {
                Alteração = string.Concat("Entrou na tela de listagem de atividades ")
            });
            _context.SaveChanges();
            return View(await _context.ToDoList.ToListAsync());
        }

        // GET: ToDoLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoList = await _context.ToDoList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoList == null)
            {
                return NotFound();
            }

            _context.Auditoria.Add(new Auditoria
            {
                Alteração = string.Concat("Entrou na tela de detalhes de atividades ", toDoList.Id, "-", toDoList.Descrição)
            });
            _context.SaveChanges();

            return View(toDoList);
        }

        // GET: ToDoLists/Create
        public IActionResult Create()
        {
            _context.Auditoria.Add(new Auditoria
            {
                Alteração = "Entrou na tela de cadastros "
            });
            _context.SaveChanges();
            return View();
        }

        // POST: ToDoLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descrição,Prioridade")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoList);
                await _context.SaveChangesAsync();

                _context.Auditoria.Add(new Auditoria
                {
                    Alteração = string.Concat("Cadastrou uma atividade: ",
                    toDoList.Descrição, "/ Data de cadastro: ", DateTime.Now.ToLongDateString())
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoList);
        }

        // GET: ToDoLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoList = await _context.ToDoList.FindAsync(id);
            if (toDoList == null)
            {
                return NotFound();
            }
            _context.Auditoria.Add(new Auditoria
            {
                Alteração = string.Concat("Entrou na tela de edição de atividades ", toDoList.Id, "-", toDoList.Descrição)
            });
            _context.SaveChanges();


            return View(toDoList);
        }

        // POST: ToDoLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descrição,Prioridade")] ToDoList toDoList)
        {
            if (id != toDoList.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoList);
                    await _context.SaveChangesAsync();


                    _context.Auditoria.Add(new Auditoria
                    {
                        Alteração = string.Concat("Atualizou a atividade: ",
                    toDoList.Descrição, "/ Data de Atualização : ", DateTime.Now.ToLongDateString())
                    });
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoListExists(toDoList.Id))
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
            return View(toDoList);
        }

        // GET: ToDoLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoList = await _context.ToDoList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // POST: ToDoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoList = await _context.ToDoList.FindAsync(id);
            _context.ToDoList.Remove(toDoList);
            await _context.SaveChangesAsync();



            _context.Auditoria.Add(new Auditoria
            {
                Alteração = string.Concat("Deletou a atividade: ",
                    toDoList.Descrição, "/ Data da exclusão: ", DateTime.Now.ToLongDateString())
            });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListExists(int id)
        {
            return _context.ToDoList.Any(e => e.Id == id);
        }
    }
}