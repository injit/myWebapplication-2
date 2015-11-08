using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace myWebApplication
{
    /// <summary>
    /// Default lacks pagination
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {

        /// <summary>
        /// gets table from database show it on the webpage
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        protected void Page_Load(object sender, EventArgs e)
        {


            peopleGridView.DataSource = dbResult();
            peopleGridView.DataBind();
        }

        /// <summary>
        /// connects to the database, runs a query and returns table
        /// </summary>
        /// <returns>table</returns>
        protected DataTable dbResult()
        {
            try
            {
                string dbConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(dbConString))
                {
                    conn.Open();
                    string query = "SELECT * From People";
                    // string query = "SELECT emplid as [Emplid], firstName as [First Name], lastname as [Last Name], department as [Department], room as [Room], phone as [Phone], email as [Email] From People";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable peopletable = new DataTable();
                        adapter.Fill(peopletable);
                        return peopletable;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }


        /// <summary>
        /// Sorts column asc or desc according to specific column clicked
        /// </summary>
        /// <param name="sender">reference to the control/object (clicking column header) raising an sorting event</param>
        /// <param name="e">contans event data which is Peoples data in this case that needs to sorted</param>
        protected void peopleGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = peopleGridView.DataSource as DataTable;

            if (dt != null)
            {
                DataView dvSortedView = new DataView(dt);
                dvSortedView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                peopleGridView.DataSource = dvSortedView;
                peopleGridView.DataBind();
            }
        }


        /// <summary>
        /// string value of either Ascending or Descending is returned back 
        /// </summary>
        /// <param name="column">takes column name as a string paramater </param>
        /// <returns>returns ascending or descending</returns>
        protected string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExp = ViewState["SortExpression"] as string;

            if (sortExp != null)
            {
                if (sortExp == column)
                {
                    string lstDir = ViewState["SortDirection"] as String;
                    if ((lstDir != null) && (lstDir == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }


        /// <summary>
        /// higlights rows on mouse hovering 
        /// </summary>
        /// <param name="sender">reference to the control/object (hovering over row) raising an highlighting event</param>
        /// <param name="e">contans event data which is Peoples data in this case that needs to sorted</param>

        protected void peopleGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                //on mouse hovering over the row the background color changes
                e.Row.Attributes.Add("onmouseover", "this.originalstyle = this.style.backgroundColor;this.style.backgroundColor='gray';");
                

                //on mouse out the background color of the row changes back to original
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor = this.originalstyle;");
            }
        }



    }
}







/*
t.Columns["room"].ColumnName = "Room";
t.Columns["phone"].ColumnName = "Phone";
t.Columns["email"].ColumnName = "Email";
t.Columns["emplid"].ColumnName = "Emplid";
t.Columns["firstName"].ColumnName = "First Name";
t.Columns["lastName"].ColumnName = "Last Name";
t.Columns["department"].ColumnName = "Department";

        
foreach(DataRow row in t.Rows)
{
String email = row["email"].ToString();
String emailLink = "<a href=mailto:"+email+">"+email+"</a>";
row["email"]= emailLink;
}
*/

/*foreach(DataRow row in t.Rows)
{
    String email = row["email"].ToString();
    //String emailLink = "<a href='mailto:" + email + "'>" + email + "</a>";
    String emailLink = "<a href='mailto:<%# Eval("+email+") %>'><%# Eval("+email+") %></a>";
    row["email"] = emailLink;
}*/