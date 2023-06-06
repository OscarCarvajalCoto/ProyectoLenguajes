using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Models.DTO;
using ProyectoLenguajes.Models;
using ProyectoLenguajes.Repositories.Abstract;
using System.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace ProyectoLenguajes.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IUserAdministrationService _service;
        public AdminController(IUserAdministrationService service)
        {
            this._service = service;
        }
        // GET: SuperAdminAdministrationController
        public async Task<IActionResult> Index()
        {
            var userListT = new List<UserInformation>();
            var userClientList = await _service.GetUsersByRoleAsync("client");
            userListT.AddRange(userClientList);
            return View(userListT);
        }

        // GET: SuperAdminAdministrationController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = await _service.GetUserByIdAsync(id);
            return View(user);
        }

        // GET: SuperAdminAdministrationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuperAdminAdministrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("There was an error");
                }
                model.Role = "client";
                var result = await _service.CreateAsync(model);
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: SuperAdminAdministrationController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _service.GetUserByIdAsync(id);
            return View(user);
        }

        // POST: SuperAdminAdministrationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserInformation userI)
        {
            try
            {
                await _service.UpdateAsyncA(userI);
                return View(userI);
            }
            catch
            {
                return View();
            }
        }

        // GET: SuperAdminAdministrationController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _service.GetUserByIdAsync(id);
            return View(user);
        }

        // POST: SuperAdminAdministrationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, UserInformation userI)
        {
            try
            {
                await _service.DeleteAsync(userI.User);
                return View();
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> DownloadUsersReportPDF()
        {
            var userListT = new List<UserInformation>();
            var userClientList = await _service.GetUsersByRoleAsync("client");
            userListT.AddRange(userClientList);

            var documentPDF = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    /*page.Header().Height(100).Background(Colors.Blue.Medium);
                    page.Content().Background(Colors.Grey.Medium);
                    page.Footer().Height(100).Background(Colors.LightBlue.Medium);*/

                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Border(1).Background("#1C1C1C").AlignCenter().Height(40).Text("Application Users Report (SuperAdmin)").FontColor("#FFFFFF").FontFamily("Century Gothic").FontSize(20);
                    }); //para hacer filas, relative item divide en 3 porque tenemos 3

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });//lo que le pone a los encabezados de las columnas con columnsDefinitions

                            table.Header(header =>
                            {
                                header.Cell().Background("#08298A").AlignCenter().Text("User Id").FontColor("#fff").FontFamily("Century Gothic").FontSize(15);
                                header.Cell().Background("#08298A").AlignCenter().Text("Roles").FontColor("#fff").FontFamily("Century Gothic").FontSize(15);
                                header.Cell().Background("#08298A").AlignCenter().Text("Name").FontColor("#fff").FontFamily("Century Gothic").FontSize(15);
                                header.Cell().Background("#08298A").AlignCenter().Text("UserName").FontColor("#fff").FontFamily("Century Gothic").FontSize(15);
                                header.Cell().Background("#08298A").AlignCenter().Text("Email").FontColor("#fff").FontFamily("Century Gothic").FontSize(15);
                            });

                            foreach (var user in userListT)
                            {
                                table.Cell().Border(0.5f).BorderColor(Colors.Black).Padding(2).AlignCenter().Text(user.User.Id).FontColor("#000").FontFamily("Century Gothic").FontSize(10);
                                table.Cell().Border(0.5f).BorderColor(Colors.Black).Padding(2).AlignCenter().Text(user.Roles[0]).FontColor("#000").FontFamily("Century Gothic").FontSize(10);
                                table.Cell().Border(0.5f).BorderColor(Colors.Black).Padding(2).AlignCenter().Text(user.User.Name).FontColor("#000").FontFamily("Century Gothic").FontSize(10);
                                table.Cell().Border(0.5f).BorderColor(Colors.Black).Padding(2).AlignCenter().Text(user.User.UserName).FontColor("#000").FontFamily("Century Gothic").FontSize(10);
                                table.Cell().Border(0.5f).BorderColor(Colors.Black).Padding(2).AlignCenter().Text(user.User.Email).FontColor("#000").FontFamily("Century Gothic").FontSize(10);
                            }

                        });
                    });
                });
            }).GeneratePdf(); //nos devuelve bytes con el PDF
            var stream = new MemoryStream(documentPDF); //crear el PDF y lo guarda en memoria
            return File(stream, "application/pdf", "UsersReport_" + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf"); //enviar el documento con su tipo y nombre
        }

    }
}
