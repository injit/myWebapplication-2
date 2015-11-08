using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace myWebApplication
{
    /// <summary>
    /// alternate allows pagination 
    /// </summary>
    public partial class alternate : System.Web.UI.Page
    {

        /// <summary>
        /// gets table from database show it on the webpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            peopleGridView.DataSource = dbResult();//
            peopleGridView.DataBind();

        }

        /// <summary>
        /// connects to the database, runs a query and returns table
        /// </summary>
        /// <returns>table</returns>
        protected DataTable dbResult()
        {
            try//exception handler
            {
                //connString defined in webconfig
                //dbConString holds the credentials to connect to database
                string dbConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

                //Connection to data base is checked 
                using (SqlConnection conn = new SqlConnection(dbConString))
                {
                    //Opens the connection
                    conn.Open();
                    string query = "SELECT * From People";

                    //adapter holds the content after connected to database table
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable peopletable = new DataTable();//empty table created
                        adapter.Fill(peopletable);//content filled to empty table
                        return peopletable;//filled table returned
                    }

                    //end of Using disconnects the connection to database
                }
            }
            catch (Exception ex)//catches the exception 
            {
                Debug.WriteLine(ex.Message);//writes on the screen if any exceptions is catched
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
            //dt holds the table 
            DataTable dt = peopleGridView.DataSource as DataTable;


            if (dt != null)//checks if the table is empty
            {
                
                DataView dataView = new DataView(dt);//gives a customize view that can be sorted 

                //string with column header name and sorting direction
                dataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);//

                //
                peopleGridView.DataSource = dataView;
                //peopleGridView.PageIndex = 0;//takes to the first page when differnt column is clicked for sorting
                peopleGridView.DataBind();
            }

        }


        /// <summary>
        /// string value of either Ascending or Descending is returned back 
        /// </summary>
        /// <param name="column">takes column name as a string paramater </param>
        /// <returns>returns ascending or descending</returns>
        protected string GetSortDirection(string column)//
        {
            string sortDirection = "DESC";

            //sortExp gets the returned value that are the header name of the column
            string sortExp = ViewState["SortExpression"] as string;
            if (sortExp != null && sortExp == column)
            {
                //Whether ASC or DESC is stored as string in lstDir
                string lstDir = ViewState["SortDirection"] as String;

                if ((lstDir != null) && (lstDir == "DESC"))
                {
                    sortDirection = "ASC";

                }
            }
            //ViewState holds the direction being set utilized on next page when clicked
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;//ViewState holds the column name utilized on next page when clicked


            return sortDirection;

        }


        protected void peopleGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["SortDirection"] != null)//executed on second click on the header
            {

                DataTable dt = peopleGridView.DataSource as DataTable;//obtaining the data
                if (dt != null) //
                {
                    DataView dataView = new DataView(dt);//represents view of the table without storing it

                    //header name and sorting dirction is passed from previous page as sorted accordingly and 
                    //assigned to dvsortedView
                    dataView.Sort = ViewState["SortExpression"] + " " + ViewState["SortDirection"];
                    peopleGridView.PageIndex = e.NewPageIndex;//skiping to new page
                    peopleGridView.DataSource = dataView;
                    peopleGridView.DataBind();
                }
            }
            else
            {
                peopleGridView.PageIndex = e.NewPageIndex;
                peopleGridView.DataBind();
            }
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
                //
                e.Row.Attributes.Add("onmouseover", "this.originalstyle = this.style.backgroundColor;this.style.backgroundColor='#E8E8E8';");

                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor = this.originalstyle;");

            }
        }


    }
}