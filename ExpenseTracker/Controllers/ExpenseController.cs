﻿using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ExpenseDBContext _context;
        public ExpenseController(ExpenseDBContext context)
        {
            _context = context;
        }

        ExpensesDataAcessLayer objexpense = new ExpensesDataAcessLayer();
        public IActionResult Index(string searchString)
        {
            List<ExpenseReport> lstEmployee = new List<ExpenseReport>();
            lstEmployee = objexpense.GetAllExpenses().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                lstEmployee = objexpense.GetSearchResult(searchString).ToList();
            }
            return View(lstEmployee);
        }
        public ActionResult AddEditExpenses(int itemId)
        {
            ExpenseReport model = new ExpenseReport();
            if (itemId > 0)
            {
                model = objexpense.GetExpenseData(itemId);
            }
            return PartialView("_expenseForm", model);
        }
        [HttpPost]
        public ActionResult Create(ExpenseReport newExpense)
        {
            if (ModelState.IsValid)
            {
                if (newExpense.ItemId > 0)
                {
                    objexpense.UpdateExpense(newExpense);
                }
                else
                {
                    objexpense.AddExpense(newExpense);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            objexpense.DeleteExpense(id);
            return RedirectToAction("Index");
        }
        public ActionResult ExpenseSummary()
        {
            return PartialView("_expenseReport");
        }
        public JsonResult GetMonthlyExpense()
        {
            Dictionary<string, decimal> monthlyExpense = objexpense.CalculateMonthlyExpense();
            return new JsonResult(monthlyExpense);
        }
        public JsonResult GetWeeklyExpense()
        {
            Dictionary<string, decimal> weeklyExpense = objexpense.CalculateWeeklyExpense();
            return new JsonResult(weeklyExpense);
        }
    }
}
