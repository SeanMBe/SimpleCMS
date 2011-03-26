using FluentNHibernate.Mapping;

namespace SimpleCMS.Core.Models.Mapping
{
    public class PostMapping : ClassMap<Post>
    {
        public PostMapping()
        {
            Table("Post");
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Body);
            Map(x => x.CreatedDate);
            Map(x => x.ModifiedDate);
            References(x => x.Author, "AuthorId");
        }
    }
}