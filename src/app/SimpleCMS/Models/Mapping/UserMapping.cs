using FluentNHibernate.Mapping;

namespace SimpleCMS.Models.Mapping
{
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Table("User");
            Id(x => x.Id);
            Map(x => x.DisplayName);
            Map(x => x.UserName);
        }
    }
}