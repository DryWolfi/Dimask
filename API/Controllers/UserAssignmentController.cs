using API.ViewModels;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace API.Controllers
{
    public class UserAssignmentController : Controller
    {
        private readonly IAssignmentService _Assignment;
        private readonly IUserAssignmentService _UserAssignment;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private UserManager<User> _userManager;

        public UserAssignmentController(IAssignmentService iAllAssignments, IUserAssignmentService userAssignment, 
            UserManager<User> userManager, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _Assignment = iAllAssignments;
            _userManager = userManager;
            _UserAssignment = userAssignment;
            _configuration = configuration;
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddToUser(string userId, string assignmentId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            AssignmentDTO assignment = _Assignment.GetAssignment(assignmentId);
            if (user == null)
            {
                return NotFound();
            }
            if (assignment == null)
            {
                return NotFound();
            }
            _UserAssignment.AddUserAssignment(assignment, user);
            /*string hostemail = _configuration.GetValue<string>("MySettings:Email");
            string hosppassword = _configuration.GetValue<string>("MySettings:Password");
            int port = _configuration.GetValue<int>("MySettings:SMTPPort");
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(user.Email, "You resieved a new assignment XD", 
                $"We granted you a new assignment =) {assignment.Name}, please check it", hostemail, hosppassword, port);*/
            return RedirectToAction("Index", "Users", userId);
        }
        public async Task<IActionResult> ShowUserAssignments(string userName)
        {
            User user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }
            List<AssignmentDTO> assignments = _UserAssignment.GetAssignments(user);
            var assignmentObj = new AssignmentListViewModel
            {
                AllAssignments = assignments,
            };
            return View(assignmentObj);
        }
        public async Task<IActionResult> MarkComplete(string userName, string assignmentId)
        {
            User user = await _userManager.FindByNameAsync(userName);
            AssignmentDTO assignment = _Assignment.GetAssignment(assignmentId);
            if (user == null)
            {
                return NotFound();
            }
            if (assignment == null)
            {
                return NotFound();
            }
            _UserAssignment.MarkAsComplete(assignment, user);
            string id = assignment.Id;
            return RedirectToAction("Index", "Home");
        }
        public ViewResult List()
        {
            IEnumerable<UserAssignmentDTO> userAssignments = _UserAssignment.UserAssignments().OrderBy(x => x.Id);
            var userAssignmentObj = new UserAssignmentListViewModel
            {
                AllUserAssignments = userAssignments
            };
            return View(userAssignmentObj);
        }
        public ViewResult ShowUserAssignment(string id)
        {
            UserAssignmentDTO userAssignment = _UserAssignment.GetUserAssignment(id);
            var userAssignmentObj = new UserAssignmentListViewModel
            {
                CurrAssignment = userAssignment.Assignment,
                Worker = userAssignment.User,
                UserAssignment = userAssignment
            };
            return View(userAssignmentObj);
        }
    }
}
