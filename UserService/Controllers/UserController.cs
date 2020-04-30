using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            string data = "[{\"id\":1,\"firstname\":\"John\",\"lastname\":\"smith\",\"address\":\"35 avenue road\",\"contact\":\"1125-4569873\",\"email\":\"john.smith@example.com\"},{\"id\":2,\"firstname\":\"jane\",\"lastname\":\"smdoeith\",\"address\":\"35 avenue road\",\"contact\":\"1125-4569873\",\"email\":\"john.smith@example.com\"},{\"id\":3,\"firstname\":\"John\",\"lastname\":\"smith\",\"address\":\"35 avenue road\",\"contact\":\"1125-4569873\",\"email\":\"john.smith@example.com\"},{\"id\":4,\"firstname\":\"John\",\"lastname\":\"smith\",\"address\":\"35 avenue road\",\"contact\":\"1125-4569873\",\"email\":\"john.smith@example.com\"}]";
            return data;
        }
    }
}