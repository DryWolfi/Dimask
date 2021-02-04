using API.ViewModels;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(Roles = "moderator, admin")]
    public class AssignmentController : Controller
    {
        private readonly IAssignmentService _Assignment;
        private readonly IUserAssignmentService _UserAssignment;
        private UserManager<User> _userManager;

        public AssignmentController(IAssignmentService iAllAssignments, IUserAssignmentService userAssignment, UserManager<User> userManager)
        {
            _Assignment = iAllAssignments;
            _userManager = userManager;
            _UserAssignment = userAssignment;
        }
        
        [Route("Assignments/List")]
        public ViewResult List()
        {
            IEnumerable<AssignmentDTO> assignments = _Assignment.Assignments().OrderBy(x => x.Id);
            var assignmentObj = new AssignmentListViewModel
            {
                AllAssignments = assignments
            };

            ViewBag.Title = "Assignment list";
            return View(assignmentObj);
        }
        public ViewResult AddList(string userId)
        {
            IEnumerable<AssignmentDTO> assignments = _Assignment.Assignments().OrderBy(x => x.Id);
            var assignmentObj = new AssignmentListViewModel
            {
                AllAssignments = assignments,
                UserId = userId
            };

            ViewBag.Title = "Assignment list";
            return View(assignmentObj);
        }
        public IActionResult CreateAssignment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAssignment(AssignmentDTO assignment)
        {
            if (ModelState.IsValid)
            {
                _Assignment.AddAssignment(assignment);
                return RedirectToAction("List");
            }
            return View(assignment);
        }
        public ViewResult ShowAssignment(string id)
        {
            var assignment = _Assignment.GetAssignment(id);
            var assignmentObj = new AssignmentListViewModel
            {
                CurrAssignment = assignment
            };
            return View(assignmentObj);
        }
        [HttpPost]
        public async Task<IActionResult> ShowAssignment(AssignmentListViewModel model, string assignmentId)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(User.Identity.Name);
                AssignmentDTO assignment = _Assignment.GetAssignment(assignmentId);
                _UserAssignment.AddSolution(assignment, user, model.Text);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeleteAssignment(string id)
        {
            _UserAssignment.DeleteUserAssignment(id);
            _Assignment.DeleteAssignment(id);
            return RedirectToAction("List");
        }
    }
}
