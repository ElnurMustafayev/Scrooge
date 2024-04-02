using ScroogeApp.Attributes.Http;
using ScroogeApp.Controllers.Base;
using ScroogeApp.Extensions;
using ScroogeApp.Models;
using ScroogeApp.Repositories;

namespace ScroogeApp.Controllers;

public class UsersController : ControllerBase
{
    private readonly UserSqlRepository userSqlRepository;

    public UsersController()
    {
        this.userSqlRepository = new UserSqlRepository();
    }

    // GET: "/Users/GetAllUsers"
    [HttpGet(ActionName = "GetAll")]
    public async Task GetAllUsersAsync() {
        var users = await this.userSqlRepository.GetAllUsersAsync();
        
        if(users is not null && users.Any()) {
            var html = "<a href=\"/Users/Create\">Create new User</a><br>"
                + users.AsHtml();
            await base.LayoutAsync(html);
        }
        else {
            await new ErrorController().NotFound(nameof(users));
        }
    }

    // GET: "/Users/Create
    [HttpGet(ActionName = "Create")]
    public async Task ShowUserCreateForm() {
        await base.WriteViewAsync("usercreate");
    }

    // POST: "/Users/Create
    [HttpPost(ActionName = "Create")]
    public async Task CreateNewUser() {
        // TODO: get from form
        await this.userSqlRepository.CreateNewUserAsync(new User() {
            Name = "FORTEST",
            Surname = "FORTEST",
            BirthDate = DateTime.Now
        });

        base.Response.StatusCode = 201;
    }

    // GET: "/Users/GetUser?Id=1"
    [HttpGet(ActionName = "GetUser")]
    public async Task GetUser() {
        var idStr = base.Request?.QueryString["Id"];
        if(string.IsNullOrWhiteSpace(idStr)) {
            this.Response.StatusCode = 400;
            return;
        }

        if(int.TryParse(idStr, out int id) == false) {
            this.Response.StatusCode = 400;
            return;
        }
        
        var user = await this.userSqlRepository.GetUserByIdAsync(id);

        if(user == null) {
            this.Response.StatusCode = 404;
            return;
        }

        await base.WriteViewAsync("userinfo", 
        new() {
            {"name", user.Name ?? "UNKNOWN" },
            {"surname", user.Surname ?? "UNKNOWN" },
            {"birthdate", user.BirthDate ?? default }
        });
    }
}