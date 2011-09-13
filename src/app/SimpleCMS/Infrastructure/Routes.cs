using RestfulRouting;
using SimpleCMS.Controllers;

namespace SimpleCMS.Infrastructure
{
    public class Routes : RouteSet {
        public override void Map(IMapper map)
        {
            map.Root<PostsController>(x => x.Show());
            map.Resource<PostsController>();
            map.Resource<AccountsController>();
        }
    }
}