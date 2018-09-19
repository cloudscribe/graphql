using cloudscribe.SimpleContent.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.SimpleContent.GraphQL.GraphTypes
{
    public class PageType : ObjectGraphType<IPage>
    {
        public PageType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The Uniqueidentifier of the page, a guid string");
            Field(x => x.ProjectId).Description("The project id, same as siteid in cloudscribe core");

            Field(x => x.MetaDescription, nullable: true).Description("The meta description of the page");

            Field(x => x.PubDate, nullable: true, type: typeof(DateTimeGraphType)).Description("The publication date of the page");
            Field(x => x.ContentType).Description("The content type, html or markdown");
            Field(x => x.Slug).Description("The url slug of the page");
            Field(x => x.Title).Description("The title of the page, also used for the heading");
            Field(x => x.Content, nullable: true).Description("The main content body of the page");
            Field(x => x.Author, nullable: true).Description("The author of the page");

            Field(x => x.ShowHeading).Description("A boolean indicating if the heading should be shown");

            Field(x => x.ParentId, nullable: true).Description("The id of the parent page or 0 if no parent");
            Field(x => x.ParentSlug, nullable: true).Description("The slug of the parent page");
            Field(x => x.PageOrder, type: typeof(IntGraphType)).Description("The sort order of the page");


            Field(x => x.CreatedUtc, type: typeof(DateTimeGraphType)).Description("The creation date");
            Field(x => x.LastModified, type: typeof(DateTimeGraphType)).Description("The last modified date");
        }
    }
}
