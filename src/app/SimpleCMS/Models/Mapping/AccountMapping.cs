using FluentNHibernate.Mapping;

namespace SimpleCMS.Models.Mapping
{
    public class AccountMapping : ClassMap<Account>
    {
        public AccountMapping()
        {
            Table("User");
            Id(x => x.Id);
            Map(x => x.Email);
        }
    }
}