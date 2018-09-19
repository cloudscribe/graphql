using cloudscribe.SimpleContent.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.SimpleContent.GraphQL.GraphTypes
{
    public class PostType : ObjectGraphType<IPost>
    {
        public PostType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The Uniqueidentifier of the page, a guid string");
            Field(x => x.BlogId).Description("The project id, same as siteid in cloudscribe core");

            Field(x => x.MetaDescription, nullable: true).Description("The meta description of the post");

            Field(x => x.PubDate, nullable: true, type: typeof(DateTimeGraphType)).Description("The publication date of the post");
            Field(x => x.ContentType).Description("The content type, html or markdown");
            Field(x => x.Slug).Description("The url slug of the post");
            Field(x => x.Title).Description("The title of the post, also used for the heading");
            Field(x => x.Content, nullable: true).Description("The main content body of the post");
            Field(x => x.Author, nullable: true).Description("The author of the page");
            
            Field(x => x.CreatedUtc, type: typeof(DateTimeGraphType)).Description("The creation date");
            Field(x => x.LastModified, type: typeof(DateTimeGraphType)).Description("The last modified date");
        }
    }
}
