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
    public partial class EditablePage : System.Web.UI.Page
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
                Label3.Text = "Total number of rows: "+peopleGridView.Rows.Count.ToString();
                
            }

            Label4.Text = DateTime.Now.ToString();
            
        }


        /// <summary>
        /// connects to the database, runs a query and returns table
        /// </summary>
        /// <returns>table</returns>


        protected DataTable dbResult(string ddchoice)
        {
            try
            {

                string dbConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(dbConString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    Debug.WriteLine("connected to database---------------------");
                    //Debug.WriteLine("connected to database 2");

                    if (ddchoice == "Please Select One")//or try using DropDownList1.SelectedIndex
                    {
                        cmd.CommandText = "SELECT * From People ORDER BY lastName, firstName ASC";//sorts it based on lastName and then firstName ascending                        
                    }
                    else
                    {
                        //parametrized query with wildcard(%) allowing to search for incomplete input sentences
                        cmd.CommandText = "SELECT * From People WHERE department LIKE '%'+@ddchoice+'%' OR firstName LIKE '%'+@ddchoice+'%' " +
                                            "OR lastName LIKE '%'+@ddchoice+'%' OR room LIKE '%'+@ddchoice+'%' OR phone LIKE '%'+@ddchoice+'%'" +
                                            "OR email LIKE '%'+@ddchoice+'%' ORDER BY lastName, firstName ASC";
                        //.CommandText = "SELECT * From People WHERE department = @ddchoice OR firstName = @ddchoice OR lastName = @ddchoice OR room = @ddchoice "
                        //+ "ORDER BY lastName, firstName ASC";
                        cmd.Parameters.AddWithValue("@ddchoice", ddchoice);
                    }
                    //

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        DataTable peopletable = new DataTable();
                        //DataSet peopletable = new DataSet();
                        adapter.Fill(peopletable);


                        //Cache["Data"] = peopletable;
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
            //string sortFind = Server.HtmlEncode(TextBox2.Text);//
            
            
            DataTable dt = null;
            //DataSet dt = null;
            if (Server.HtmlEncode(TextBox2.Text) == string.Empty)//checks if Textbox field is empty
            {
                dt = dbResult(DropDownList1.SelectedValue);//if textbox value is clear sorts based on the value obtained from dropdown menu
                
            }
            else
            {
                dt = dbResult(Server.HtmlEncode(TextBox2.Text));//otherwise use the text in the text field and sorts the table
            }


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
            //passess selected value form the dropdown menu returning table with selected value equal to passed selected input
            peopleGridView.DataSource = dbResult(DropDownList1.SelectedValue);//bind to the dat
            peopleGridView.DataBind();
            Label3.Text = "Total rows found: "+peopleGridView.Rows.Count.ToString();
            TextBox2.Text = string.Empty;//set the text box field to empty field as inorder to allow sorting property properly on choosing of 
            //dropdown menu 
        }

        /// <summary>
        /// Event handler for searching event, takes input in the text box and displays the result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            //Text on textbox is passed on click of button
            peopleGridView.DataSource = dbResult(Server.HtmlEncode(TextBox2.Text));
            peopleGridView.DataBind();
            //Displays the number of rows on to the page
            Label3.Text = peopleGridView.Rows.Count.ToString() + " match found for " + Server.HtmlEncode(TextBox2.Text);
        }

        protected void peopleGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            peopleGridView.EditIndex = e.NewEditIndex;
            if (Server.HtmlEncode(TextBox2.Text) == "")
            {
                peopleGridView.DataSource = dbResult(DropDownList1.SelectedValue);
            }
            else
            {
                peopleGridView.DataSource = dbResult(Server.HtmlEncode(TextBox2.Text));
            }
            peopleGridView.DataBind();
        }



        protected void peopleGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            peopleGridView.EditIndex = -1;
            //peopleGridView.DataSource = dbResult(DropDownList1.SelectedValue);
            if (Server.HtmlEncode(TextBox2.Text) == "")
            {
                peopleGridView.DataSource = dbResult(DropDownList1.SelectedValue);
            }
            else
            {
                peopleGridView.DataSource = dbResult(Server.HtmlEncode(TextBox2.Text));
            }
            peopleGridView.DataBind();
        }

        protected void peopleGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = peopleGridView.Rows[e.RowIndex];
            TextBox txtlastName = (TextBox)row.FindControl("txtlastName");
            TextBox txtfirstName = (TextBox)row.FindControl("txtfirstName");
            TextBox txtdepartment = (TextBox)row.FindControl("txtdepartment");
            TextBox txtroom = (TextBox)row.FindControl("txtroom");
            TextBox txtphone = (TextBox)row.FindControl("txtphone");
            TextBox txtemail = (TextBox)row.FindControl("txtemail");
            int emplid = Int32.Parse(peopleGridView.DataKeys[e.RowIndex].Value.ToString());
            Debug.WriteLine("emplid passed   ------------" + emplid);
            string lastName = txtlastName.Text;
            string firstName = txtfirstName.Text;
            string department = txtdepartment.Text;
            string room = txtroom.Text;
            string phone = txtphone.Text;
            string email = txtemail.Text;

            UpdatePeopletableValue(emplid, lastName, firstName, department, room, phone, email);
        }

        private void UpdatePeopletableValue(int emp, string ln, string fn, string dpt, string rm, string ph, string eml)
        {
            try
            {
                string dbConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;


                string qry = "UPDATE People SET lastName = @LN, firstName = @FN, department = @Dpt, room = @RM, phone = @PH, email = @Eml WHERE emplid = @Emp";

                using (SqlConnection conn = new SqlConnection(dbConString))
                {
                    SqlCommand cmd = new SqlCommand(qry, conn);
                    Debug.WriteLine("connected to database---------------------");

                    cmd.Parameters.Add("@Emp", SqlDbType.Int).Value = emp;
                    cmd.Parameters.Add("@LN", SqlDbType.NVarChar).Value = ln;
                    cmd.Parameters.Add("@FN", SqlDbType.NVarChar).Value = fn;
                    cmd.Parameters.Add("@Dpt", SqlDbType.NVarChar).Value = dpt;
                    cmd.Parameters.Add("@RM", SqlDbType.NVarChar).Value = rm;
                    cmd.Parameters.Add("@PH", SqlDbType.NVarChar).Value = ph;
                    cmd.Parameters.Add("@Eml", SqlDbType.NVarChar).Value = eml;


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    peopleGridView.EditIndex = -1;
                    //peopleGridView.DataSource = dbResult(DropDownList1.SelectedValue);


                    if (Server.HtmlEncode(TextBox2.Text) == "")
                    {
                        peopleGridView.DataSource = dbResult(DropDownList1.SelectedValue);
                    }
                    else
                    {
                        peopleGridView.DataSource = dbResult(Server.HtmlEncode(TextBox2.Text));
                    }
                    peopleGridView.DataBind();

                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}




