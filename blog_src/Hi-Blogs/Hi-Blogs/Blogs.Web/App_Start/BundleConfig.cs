using System.Web;
using System.Web.Optimization;

namespace Blogs.Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-1.8.2.js", "~/Scripts/blog/Blog.js", "~/Scripts/blog/jquery.BlogCommon.js"));

            bundles.Add(new ScriptBundle("~/bundles/ValidateAndAjax")
               .Include("~/Scripts/jquery-1.8.2.*"
               , "~/Scripts/blog/jquery.BlogCommon.js"
               , "~/Scripts/jquery.validate.*"
               , "~/Scripts/jquery.validate.unobtrusive.*"
               , "~/Scripts/jquery.unobtrusive-ajax.*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/blogs/StyleBlog.css"));

            //博客 显示 UserBlog 文件夹下 (PC端)
            bundles.Add(new StyleBundle("~/Content/UserBlog")
                .Include("~/Content/blogs/default/_LayoutCSS.css")
                .Include("~/Content/blogs/default/UserBlogCSS.css")
                .Include("~/Content/blogs/default/UserBlogListCSS.css")
                .Include("~/Content/blogs/default/GetTagBlogsCSS.css")
                .Include("~/Content/blogs/default/GetTypeBlogsCSS.css")
                .Include("~/Content/blogs/StyleBlog.css")
                );
            //博客 显示 UserBlog 文件夹下 (移动端)
            bundles.Add(new StyleBundle("~/Content/UserBlogM")
               .Include("~/Content/blogs/default/_Layout.MobileCSS.css")
               .Include("~/Content/blogs/default/UserBlogCSS.css")
               .Include("~/Content/blogs/default/UserBlogListCSS.css")
               .Include("~/Content/blogs/default/GetTagBlogsCSS.css")
               .Include("~/Content/blogs/default/GetTypeBlogsCSS.css")
               .Include("~/Content/blogs/StyleBlog.css")
               );

            //其实 这里不用打开   发布到生产  会自动 开启
            //BundleTable.EnableOptimizations = true;//开启
        }
    }
}