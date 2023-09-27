﻿using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CondominiumProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginContriller
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginRoot(string email, string password)
        {
            int validationResult = ValidateCredentialsRoot(email, password);

            switch (validationResult)
            {
                case 1:
                    return RedirectToAction("Index", "Home");

                case 0:
                    // Las credenciales son inválidas, muestra un mensaje de error.
                    ViewBag.Error = new ErrorHandler()
                    {
                        Title = "Invalid login",
                        ErrorMessage = "Incorrect email or password",
                        ActionMessage = "Go to login",
                        Path = "/Login"
                    };
                    return View("ErrorHandler");

                case -1:
                    // El correo electrónico no existe en la base de datos, muestra un mensaje de error.
                    ViewBag.Error = new ErrorHandler()
                    {
                        Title = "Invalid login",
                        ErrorMessage = "User not found",
                        ActionMessage = "Go to login",
                        Path = "/Login"
                    };
                    return View("ErrorHandler");

                default:
                    // Un resultado desconocido, maneja adecuadamente.
                    return View("Error");
            }
        }

        private int ValidateCredentialsRoot(string email, string password)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyLoginRoot", queryParameters);

            if (resultado.Rows.Count == 1)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

            // Resultado desconocido o error
            return -2;
        }


        // GET: LoginContriller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginContriller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginContriller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginContriller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginContriller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginContriller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginContriller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
