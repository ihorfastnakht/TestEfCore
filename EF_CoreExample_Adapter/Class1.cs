using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_CoreExample.Shared;


namespace EF_CoreExample_Adapter
{
    public class Class1
    {

        public IEnumerable<BlogDto> GetBlogs()
        {
            using (var db = new BloggingContext())
            {
                var blogs = (from x in db.Blogs
                             select new
                             {
                                id = x.BlogId,
                                posts = x.Posts,
                                url = x.Url
                             }).ToArray();


                foreach (var x in blogs)
                {
                    yield return new BlogDto()
                    {
                        BlogId = x.id,
                        //Posts = x.posts.Select(y => new PostDto()
                        //{
                        //    PostId = y.PostId,
                        //    Content = y.Content,
                        //    BlogId = y.BlogId
                        //})
                        //.ToList(),
                        Url = x.url
                    };
                }
            }
        }

        public void InsertBlogs(BlogDto blog)
        {
            using (var db = new BloggingContext())
            {
                db.Blogs.Add(new Blog()
                {
                    BlogId = blog.BlogId,
                    Posts = new List<Post>(), 
                    //blog.Posts.Select(x => new Post()
                    //{
                    //    BlogId = x.BlogId,
                    //    Content = x.Content,
                    //    PostId = x.PostId
                    //})
                    //.ToList(),
                    Url = blog.Url
                });

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
