using System.Collections.Generic;
using System.Linq;
using EF_CoreExample.Shared;
using Microsoft.EntityFrameworkCore;

namespace EF_CoreExample_Adapter
{
    public class DataAdapter
    {

        public IEnumerable<BlogDto> GetBlogs()
        {
            using (var db = new BloggingContext())
            {
                db.ChangeTracker.AutoDetectChangesEnabled = false;
                db.ChangeTracker.LazyLoadingEnabled = false;
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.Database.AutoTransactionsEnabled = false;

                //var blogs = (from x in db.Blogs
                //             select new
                //             {
                //                 id = x.BlogId,
                //                 posts = x.Posts,
                //                 url = x.Url
                //             });

                var blogs = db.Blogs.AsNoTracking().ToList();

                foreach (var x in blogs)
                {
                    yield return new BlogDto()
                    {
                        BlogId = x.BlogId,
                        Url = x.Url
                    };
                }
            }
        }

        public void InsertBlogs(BlogDto blog)
        {
            using (var db = new BloggingContext())
            {
                db.ChangeTracker.AutoDetectChangesEnabled = false;
                db.ChangeTracker.LazyLoadingEnabled = false;
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.Database.AutoTransactionsEnabled = false;

                db.Blogs.Add(new Blog()
                {
                    BlogId = blog.BlogId,
                    Posts = new List<Post>(), 
                    Url = blog.Url
                });

                db.SaveChanges();
            }
        }

        public void Remove()
        {
            //var blogs = GetBlogs().ToList();
            using (var db = new BloggingContext())
            {
                db.ChangeTracker.AutoDetectChangesEnabled = false;
                db.ChangeTracker.LazyLoadingEnabled = false;
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.Database.AutoTransactionsEnabled = false;

                var blogs = db.Blogs.ToList();

                foreach (var b in blogs)
                {
                    db.Remove(b);
                }

                db.SaveChanges();
            }
        }
    }

    public class BlogDto
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<PostDto> Posts { get; set; }
    }

    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public BlogDto Blog { get; set; }
    }
}
