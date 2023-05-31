using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Models;
using ProyectoLenguajes.Models.DTO;
using ProyectoLenguajes.Repositories.Abstract;

namespace ProyectoLenguajes.Controllers
{
    [Authorize(Roles = "superadmin")]
    public class SuperAdminAdministrationController : Controller
    {
        private readonly IUserAdministrationService _service;
        public SuperAdminAdministrationController(IUserAdministrationService service)
        {
            this._service = service;
        }
        // GET: SuperAdminAdministrationController
        public async Task<IActionResult> Index()
        {
            var userListT = new List<UserInformation>();
            var userAdminList = await _service.GetUsersByRoleAsync("admin");
            userListT.AddRange(userAdminList);
            var userClientList = await _service.GetUsersByRoleAsync("client");
            userListT.AddRange(userClientList);
            GeneralData.userList = userListT;
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
                await _service.UpdateAsync(userI);
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
    }
}
