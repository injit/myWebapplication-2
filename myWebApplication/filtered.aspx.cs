using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace myWebApplication
{
    /// <summary>
    /// this program consist of drop down list with a choice of departments name, when specific department is selected
    /// table should be displayed that only gives the name of employee on that particular department. Ajax comes in 
    /// handy in accomplishment of this feature.
    /// </summary>
    public partial class filtered : System.Web.UI.Page
    {

        /// <summary>
        /// gets table from database show it on the webpage for the first loading of the page
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//executes only if it is the first loading of the page
            {
                PopulateDropDownList1();//fills in the dropdown with sql table row values
                peopleGridView.DataSource = dbResult(DropDownList1.SelectedValue);//dropdown list selected value is passed
                peopleGridView.DataBind();
            }

        }

        /// <summary>
        /// connects to the database, runs a query and returns table
        /// </summary>
        /// <returns>table</returns>
        protected DataTable dbResultWorks(string ddchoice)
        {

            try
            {
                string dbConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(dbConString))
                {


                    //parametrized query
                    if (ddchoice != "Please Select One")
                    {
                        string query = "SELECT * From People WHERE department = @ddchoice ORDER BY lastName, firstName ASC";

                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@ddchoice", ddchoice);
                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            DataTable peopletable = new DataTable();
                            peopletable.Load(rdr);
                            return peopletable;
                        }

                    }
                    else
                    {
                        string query = "SELECT * From People ORDER BY lastName, firstName ASC";

                        using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                        {
                            conn.Open();
                            DataTable peopletable = new DataTable();
                            adapter.Fill(peopletable);
                            return peopletable;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
            return null;
        }







        protected DataTable dbResult(string ddchoice)
        {
            try
            {
                string dbConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(dbConString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    //parametrized query
                    if (ddchoice == "Please Select One")//or try using DropDownList1.SelectedIndex
                    {
                        cmd.CommandText = "SELECT * From People ORDER BY lastName, firstName ASC";                        
                    }
                    else
                    {
                        cmd.CommandText = "SELECT * From People WHERE department = @ddchoice ORDER BY lastName, firstName ASC";
                        cmd.Parameters.AddWithValue("@ddchoice", ddchoice);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
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
            //DataTable dt = peopleGridView.DataSource as DataTable;

            DataTable dt = dbResult(DropDownList1.SelectedValue);
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

        //get the department name from People table from database without duplicates
        //display them in the list
        

        protected void PopulateDropDownList1()
        {
            try
            {
                string dbConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(dbConString))
                {
                    conn.Open();
                    
                    string query = "SELECT DISTINCT department From People";
                    SqlCommand cmd = new SqlCommand(query, conn);
                        
                    SqlDataReader DropDownValues = cmd.ExecuteReader();
                    DropDownList1.DataSource = DropDownValues;
                    DropDownList1.DataValueField = "department";
                    // DropDownList1.DataTextField = "Department";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, "Please Select One");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            peopleGridView.DataSource = dbResult(DropDownList1.SelectedValue);
            peopleGridView.DataBind();
            //string selectedchoice = DropDownList1.SelectedValue;
            //Debug.WriteLine(selectedchoice);
            
        }



    }
}




