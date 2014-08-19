using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Poco;


namespace LinqToSql_1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DataContext ctx = new DataContext(
                //    ConfigurationManager.ConnectionStrings["movies"].ConnectionString,
                //    XmlMappingSource.FromUrl(Server.MapPath("~/App_Data/MovieMapping.xml")));
                //var movies = from m in ctx.GetTable<Movie>()
                //             select m;
                //grdView.DataSource = movies;
                //grdView.DataBind();

                MovieReviewsDataContext ctx = new MovieReviewsDataContext();
                var movies = ctx.Movies
                    .Where(r => r.ReleaseDate.Year > 2000 && r.Reviews.Count > 3)
                    .OrderBy(r=>r.ReleaseDate)
                    .Take(15)
                    .Select(m => m);
                
                ctx.Log = Response.Output;

                grdView.DataSource = movies;
                grdView.DataBind();
            }
        }
    }
}