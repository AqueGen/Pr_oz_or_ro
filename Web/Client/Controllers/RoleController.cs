using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Web.Client.Models.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Kapitalist.Web.Client.Controllers
{
    [RoutePrefix("role")]
    [Authorize(Roles = "SeniorAdministrator, Administrator")]
    public class RoleController : Controller
    {
        private ApplicationRoleManager RoleManager => HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();

        [Route("add")]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("add")]
        public async Task<ActionResult> AddRole(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await RoleManager.CreateAsync(new ApplicationRole
                {
                    Name = model.Name,
                    Description = model.Description,
                    NameCyrillic = model.NameCyrillic
                });
                if (result.Succeeded)
                    return RedirectToAction("Index");
                ModelState.AddModelError(string.Empty, "Помилка на сервері");
            }
            return View(model);
        }

        [Route("delete")]
        public async Task<ActionResult> DeleteRole(string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var result = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        [Route("edit")]
        public async Task<ActionResult> EditRole(string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);
            if (role != null)
                return View(new EditRoleModel { Name = role.Name, NameCyrillic = role.NameCyrillic, Description = role.Description, Id = role.Id });
            return RedirectToAction("Index");
        }

        [Route("edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRole(EditRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Description = model.Description;
                    role.Name = model.Name;
                    var result = await RoleManager.UpdateAsync(role);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    ModelState.AddModelError(string.Empty, "Помилка на сервері");
                }
            }
            return View(model);
        }

        [Route("list")]
        public ActionResult Index()
        {
            return View("Roles", RoleManager.Roles);
        }
    }
}