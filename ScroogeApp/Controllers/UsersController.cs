using ScroogeApp.Attributes.Http;
using ScroogeApp.Controllers.Base;
using ScroogeApp.Extensions;
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
            var html = users.AsHtml();
            await base.LayoutAsync(html);
        }
        else {
            await new ErrorController().NotFound(nameof(users));
        }
    }
}