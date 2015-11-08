using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace myWebApplication
{
    /// <summary>
    /// this program consist of drop down list with a choice of departments name, when specific department is selected
    /// table should be displayed that only gives the name of employee on that particular department. Ajax comes in 
    /// handy in accomplishment of this feature.
    /// </summary>
    public partial class CachedOne : System.Web.UI.Page
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
                //peopleGridView.DataSource = dbResult(DropDownList1.SelectedValue);//dropdown list selected value is passed
                peopleGridView.DataSource = dbResult();
                peopleGridView.DataBind();
                Label3.Text = "Total number of rows: "+peopleGridView.Rows.Count.ToString();
                
            }

            Label4.Text = DateTime.Now.ToString();
            
        }


        /// <summary>
        /// connects to the database, runs a query and returns table
        /// </summary>
        /// <returns>table</returns>


        //protected DataTable dbResultWorks(string ddchoice)
        //{
        //    try
        //    {
        //        if (Cache["Data"] == null)
        //        {
        //            string dbConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        //            using (SqlConnection conn = new SqlConnection(dbConString))
        //            {
        //                SqlCommand cmd = new SqlCommand();
        //                cmd.Connection = conn;
        //                Debug.WriteLine("connected to database---------------------");
        //                cmd.CommandText = "SELECT * From People ORDER BY lastName, firstName ASC";//sorts it based on lastName and then firstName ascending                        

        //                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        //                {
        //                    conn.Open();
        //                    DataTable peopletable = new DataTable();
        //                    //DataSet peopletable = new DataSet();
        //                    adapter.Fill(peopletable);


        //                    Cache["Data"] = peopletable;
        //                    return peopletable;
        //                }

        //            }
        //        }
        //        else
        //        {
        //            return (DataTable)Cache["Data"];
        //        }

        //    }



        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }

        //    return null;
        //}


        protected DataTable dbResult()
        {
            try
            {
                if (Cache["Data"] == null)
                {
                    string dbConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

                    using (SqlConnection conn = new SqlConnection(dbConString))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        Debug.WriteLine("----------------------connected to database---------------------");
                        cmd.CommandText = "SELECT * From People ORDER BY lastName, firstName ASC";//sorts it based on lastName and then firstName ascending                        

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            DataTable peopletable = new DataTable();
                            //DataSet peopletable = new DataSet();
                            adapter.Fill(peopletable);


                            Cache["Data"] = peopletable;
                            return peopletable;
                        }

                    }
                }
                else
                {
                    return (DataTable)Cache["Data"];
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
            DataTable dt = dbResult();


            if (dt != null)
            {
                DataView dvSortedView = new DataView(dt);

                if (Server.HtmlEncode(TextBox2.Text)!="")
                {
                    dvSortedView.RowFilter = "department LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%' OR firstName LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%' " +
                                                    "OR lastName LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%' OR room LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%' OR phone LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%'" +
                                                    "OR email LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%'";
                    
                }
                else if (DropDownList1.SelectedValue.ToString()!= "Please Select One")
                {
                    dvSortedView.RowFilter = "department = '" + DropDownList1.SelectedValue.ToString() + "'";
                }


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


        /// <summary>
        /// gets the department name from People table from database wihout repetations and displays on the drop down list
        /// </summary>
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

        /// <summary>
        /// Event handler for selected index change in dropdown menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            //DataTable dt = (DataTable)Cache["Data"];//step 1 this gives us the reutilization of Cache syntax
            //DataView dv = new DataView(dt);//step 2
            //above two can be used as well

            DataView dv = new DataView(dbResult());//reloading returns cached Datatable without invoking on database
            dv.RowFilter = "department = '"+ DropDownList1.SelectedValue.ToString()+ "'";//


            dv.Sort = "lastName, firstName ASC";
            if (DropDownList1.SelectedValue.ToString().Equals("Please Select One"))//To display whole cached table 
            {
                //peopleGridView.DataSource = dt;//if step 1 and 2 were used
                peopleGridView.DataSource = dbResult();//cached table as datasource               
            }
            else//To display according to dropdown menu choice
            {
                peopleGridView.DataSource = dv;//  
            }
            peopleGridView.DataBind();
            Label3.Text = "Total rows found: " + peopleGridView.Rows.Count.ToString();
            TextBox2.Text = string.Empty;
        }

        /// <summary>
        /// Event handler for searching event, takes input in the text box and displays the result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Cache["Data"];
            DataView dv = new DataView(dt);

            dv.RowFilter = "department LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%' OR firstName LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%' " +
                                                "OR lastName LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%' OR room LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%' OR phone LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%'" +
                                                "OR email LIKE '%" + Server.HtmlEncode(TextBox2.Text) + "%'";
            dv.Sort = "lastName, firstName ASC";
            peopleGridView.DataSource = dv;
            peopleGridView.DataBind();
            Label3.Text = peopleGridView.Rows.Count.ToString() + " match found for " + Server.HtmlEncode(TextBox2.Text);
        }

    }
}




