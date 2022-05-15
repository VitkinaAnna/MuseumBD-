using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MuseunBD.Models;


namespace MuseunBD.Controllers
{
    public class QueriesController : Controller
    {
        private const string CONNECTION = "Server=DESKTOP-EFQ1BJ6\\SQLEXPRESS; Database=Museum;Trusted_Connection=True;MultipleActiveResultSets=true";

        private const string A1_PATH = @"C:\Users\Павел\source\repos\MuseunBD\MuseunBD\Queries\A1.sql";
        private const string A2_PATH = @"C:\Users\Павел\source\repos\MuseunBD\MuseunBD\Queries\A2.sql";
        private const string A3_PATH = @"C:\Users\Павел\source\repos\MuseunBD\MuseunBD\Queries\A3.sql";
        private const string A4_PATH = @"C:\Users\Павел\source\repos\MuseunBD\MuseunBD\Queries\A4.sql";
        private const string A5_PATH = @"C:\Users\Павел\source\repos\MuseunBD\MuseunBD\Queries\A5.sql";

        private const string ERR_EX = "Екскурсії, що задовольняють дану умову, відсутні";
        private const string ERR_DIN = "Динозаври, що задовольняють дану умову, відсутні";
        private const string ERR_WOR = "Працівники, що задовольняють дану умову, відсутні";
        private const string ERR_TICK = "Квитки, що задовольняють дану умову, відсутні";

        private readonly MuseumContext _context;

        public QueriesController(MuseumContext context)
        {
            _context = context;
        }

        public IActionResult Index(int error)
        {

            if (error == 1)
            {
                ViewBag.ErrorFlag = 1;
                ViewBag.QuantityError = "Введіть коректне число";
            }
            var empty = new SelectList(new List<string> { "---" });
            var anyDinosaurs = _context.Dinosaurs.Any();
            var anyExhibitions = _context.Exhibitions.Any();
            var anyWorkers = _context.Workers.Any();
            var anyTickets = _context.Tickets.Any();
            var anyHalls = _context.Halls.Any();
            var anyPositions = _context.Positions.Any();

            ViewBag.DinosaurNames = anyDinosaurs ? new SelectList(_context.Dinosaurs, "DinosaurId", "Name") : empty;
            ViewBag.DinosaurLifetimes = anyDinosaurs ? new SelectList(_context.Dinosaurs, "DinosaurId", "Lifetime") : empty;
            ViewBag.ExhibitionPrices = anyExhibitions ? new SelectList(_context.Exhibitions, "ExhibitionId", "Price") : empty;
            ViewBag.ExhibitionNames = anyTickets ? new SelectList(_context.Exhibitions, "ExhibitionId", "Name") : empty;
            ViewBag.WorkerNames = anyWorkers ? new SelectList(_context.Workers, "WorkerId", "Name") : empty;
            ViewBag.TicketNames = anyTickets ? new SelectList(_context.Tickets, "TicketId", "Name") : empty;
            ViewBag.HallNames = anyHalls ? new SelectList(_context.Halls, "HallId", "Name") : empty;
            ViewBag.PositionNames = anyPositions ? new SelectList(_context.Positions, "PositionId", "Name") : empty;
            ViewBag.HallIds = anyHalls ? new SelectList(_context.Halls, "HallId", "Name") : empty;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query1(Query queryModel) // всі динозаври що знаходяться в залі h1
        {
            string query = System.IO.File.ReadAllText(A1_PATH);
            query = query.Replace("h1", queryModel.HallName.ToString());
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryName = "A1";
            queryModel.DinosaurNames = new List<string>();
            using (var connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.DinosaurNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.ErrorName = ERR_DIN;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query2(Query queryModel) // ціна екускурсій за які відповідає робітник w1
        {
            string query = System.IO.File.ReadAllText(A2_PATH);
            query = query.Replace("w1", queryModel.WorkerName.ToString());
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryName = "A2";
            queryModel.ExhibitionPrices = new List<string>();
            using (var connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.ExhibitionPrices.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.ErrorName = ERR_EX;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query3(Query queryModel) // всі екскурсії що знаходяться в залі h2
        {
            string query = System.IO.File.ReadAllText(A3_PATH);
            query = query.Replace("h2", queryModel.HallId.ToString());
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryName = "A3";
            queryModel.DinosaurLifetimes = new List<string>();
            using (var connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.DinosaurLifetimes.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.ErrorName = ERR_EX;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query4(Query queryModel) // всі робітники які мають посаду p1
        {
            string query = System.IO.File.ReadAllText(A4_PATH);
            query = query.Replace("p1", queryModel.PositionName.ToString());
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryName = "A4";
            queryModel.WorkerNames = new List<string>();
            using (var connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.WorkerNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.ErrorName = ERR_WOR;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query5(Query queryModel) // всі люди що йдуть на екскурсію e1
        {
            string query = System.IO.File.ReadAllText(A5_PATH);
            query = query.Replace("e1", queryModel.ExhibitionName.ToString());
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryName = "A5";
            queryModel.TicketNames = new List<string>();
            using (var connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.TicketNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.ErrorName = ERR_TICK;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }
        public IActionResult Result(Query queryResult)
        {
            return View(queryResult);
        }
    }
}