using inHealthTechnicalExam.DataAccessLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using inHealthTechnicalExam.BusinessLayer.Repository;
using inHealthTechnicalExam.BusinessLayer.IRepository;

namespace inHealthTechnicalExam
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(options =>
            {
                options.Cookie.Name = ".MyApplication.Session";
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            //services.Add(new ServiceDescriptor(typeof(IBlogRepository), new BlogRepository(new BlogDbContext(new DbContextOptions<BlogDbContext>()))));
            //services.Add(new ServiceDescriptor(typeof(ICommentRepository), new CommentRepository(new BlogDbContext(new DbContextOptions<BlogDbContext>()))));
            //services.Add(new ServiceDescriptor(typeof(IRoleRepository), new RoleRepository(new BlogDbContext(new DbContextOptions<BlogDbContext>()))));
            //services.Add(new ServiceDescriptor(typeof(IUserRepository), new UserRepository(new BlogDbContext(new DbContextOptions<BlogDbContext>()))));
            //services.Add(new ServiceDescriptor(typeof(IUserRoleRepository), new UserRoleRepository(new BlogDbContext(new DbContextOptions<BlogDbContext>()))));

            var connection = @"Server=LAPTOP-VFPDU88O\SQLEXPRESS;Database=BlogDB;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(connection));
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Strict,
                Secure = CookieSecurePolicy.SameAsRequest
            });
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
