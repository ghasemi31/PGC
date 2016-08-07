using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using kFrameWork.Util;
using System.Data;
using System.Collections;
using System.Text;

public partial class mgmt : System.Web.UI.Page
{

    #region Extra REqs

    // we can add a crack for testing if this page is not exist or has been deleted then software crash

    #endregion

    //private string Password
    //{
    //    get
    //    {
    //        if (this.ViewState["_Password"] != null)

    //            return this.ViewState["_Password"].ToString();
    //        else
    //            return string.Empty;
    //    }
    //    set
    //    {
    //        this.ViewState["_Password"] = txtPassword.Text;
    //    }
    //}

    private string GetConnectionString()
    {
        return txtConnectionString.Text;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                Table1.Visible = false;
                Table2.Visible = false;
                Table3.Visible = false;
                Table4.Visible = false;
                LoginPanel.Visible = false;

                if (Request.QueryString["Admin"] != null)
                {
                    if (Request.QueryString["Admin"].ToLower() == "True".ToLower())
                    {
                        LoginPanel.Visible = true;
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            Response.Write("Exception throwed during page load : " + Ex.Message.Replace("\n", "</br>"));
        }
    }



    protected void cmdConnect_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand command = CreateCommand("");

            command.CommandTimeout = ConvertorUtil.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

            lbConnectionMessage.ForeColor = System.Drawing.Color.Green;

            lbConnectionMessage.Text = "The connection successfully established!";
        }
        catch (Exception Ex)
        {
            lbConnectionMessage.Text = Ex.Message;
        }
    }

    
    private void LoadDefaults()
    {
        try
        {
            string EntityCS = System.Configuration.ConfigurationManager.ConnectionStrings["pgcEntities"].ToString();

            txtConnectionString.Text = EntityCS.Substring(EntityCS.IndexOf("connection string") + 18).Trim("\"".ToCharArray());

            DataTable dt = ExecuteReaderFromProc("dbo.GetTablesOfDb");

            cboTables.DataSource = dt;

            cboTables.DataTextField = "Description";

            cboTables.DataValueField = "Id";

            cboTables.DataBind();

            lbConnectionMessage.Text = string.Format("Connection establieshed successfully.<br/>Data Loaded Successfully.");

            lbConnectionMessage.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception Ex)
        {
            lbConnectionMessage.Text = string.Format("Exception occured during loading default data : {0}", Ex.Message);
        }

    }

    protected void cmdExecute_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdbNonQuery.Checked)
            {
                int Res = ExexcuteNonQuery(txtQuery.Text);

                lblExecuteMessage.Text = "Query executed successfully. <br/>Rows Affected : " + Res.ToString();
            }

            if (rdbScalar.Checked)
            {
                object Res = ExecuteScalar(txtQuery.Text);

                lblExecuteMessage.Text = "Query executed successfully. <br/>String Of Result : " + Res.ToString();
            }
            if (rdbReader.Checked)
            {
                DataTable dt = ExecuteReader(txtQuery.Text);

                QueryGridView.DataSource = dt;

                QueryGridView.DataBind();

                lblExecuteMessage.Text = "Query executed successfully.";
            }

            lblExecuteMessage.ForeColor = System.Drawing.Color.Green;

            lblQuery.Text = txtQuery.Text.Replace("\n", "<br/>");
        }
        catch (Exception Ex)
        {
            lblExecuteMessage.Text = Ex.Message;
        }
    }

    protected void QueryGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        QueryGridView.PageIndex = e.NewPageIndex;

        cmdExecute_Click(null, null);
    }

    protected void cmdGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            string strCommandName = "";

            if (rdbCreate.Checked)
                strCommandName = "Create";

            if (rdbAlter.Checked)
                strCommandName = "Alter";

            switch (cboTypes.SelectedValue)
            {
                case "0":
                    txtQuery.Text = GenerateRetrieve(cboTables.SelectedValue, strCommandName);
                    break;
                case "1":
                    txtQuery.Text = GenerateDelete(cboTables.SelectedValue, strCommandName);
                    break;
                case "2":
                    txtQuery.Text = GenerateSave(cboTables.SelectedValue, strCommandName);
                    break;
                case "3":
                    txtQuery.Text = GenerateLookup(cboTables.SelectedValue, strCommandName);
                    break;
                case "4":
                    txtQuery.Text = GenerateList(cboTables.SelectedValue, strCommandName);
                    break;
            }
            lblGenerationMessage.Text = "Generation succeed.";

            lblGenerationMessage.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception Ex)
        {
            lblGenerationMessage.Text = Ex.Message;
        }


    }

    #region Required Db  Function

    public SqlCommand CreateCommand(string commandText)
    {
        SqlConnection connection = new SqlConnection(GetConnectionString());

        SqlCommand command = connection.CreateCommand();

        command.CommandTimeout = ConvertorUtil.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

        command.CommandText = commandText;

        command.CommandType = CommandType.Text;

        command.Connection.Open();

        return command;
    }

    public SqlCommand CreateCommandFromProc(string commandText, params SqlParameter[] parameters)
    {
        SqlConnection connection = new SqlConnection(GetConnectionString());

        SqlCommand command = connection.CreateCommand();

        command.CommandTimeout = ConvertorUtil.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

        command.CommandText = commandText;

        command.CommandType = CommandType.StoredProcedure;

        SqlParameter returnValue = new SqlParameter();

        returnValue.Direction = ParameterDirection.ReturnValue;

        command.Parameters.Add(returnValue);

        foreach (SqlParameter p in parameters)

            command.Parameters.Add(p);

        command.Connection.Open();

        return command;
    }

    public DataTable ExecuteReaderFromProc(string ProcName, params SqlParameter[] parameters)
    {
        return ExecuteReader(CreateCommandFromProc(ProcName, parameters));

    }

    public DataTable ExecuteReader(string SqlSelect)
    {
        return ExecuteReader(CreateCommand(SqlSelect));
    }

    public DataTable ExecuteReader(SqlCommand command)
    {
        try
        {
            SqlDataReader reader = command.ExecuteReader();

            DataTable ShemaTable = reader.GetSchemaTable();

            DataTable ResultTable = new DataTable();

            foreach (DataRow Row in ShemaTable.Rows)
            {
                ResultTable.Columns.Add(Row.ItemArray[0].ToString(), (System.Type)Row.ItemArray[12]);
            }

            while (reader.Read())
            {
                ArrayList Row = new ArrayList();

                for (int Counter = 0; Counter < reader.FieldCount; Counter++)
                {
                    Row.Add(reader.GetValue(Counter));

                }
                ResultTable.Rows.Add(Row.ToArray());
            }

            reader.Close();

            return ResultTable;

        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            if (command.Connection.State == ConnectionState.Open)

                command.Connection.Close();

            command.Dispose();
        }
    }

    public object ExecuteScalar(String SqlStatment)
    {
        return ExecuteScalar(CreateCommand(SqlStatment));
    }

    public object ExecuteScalar(SqlCommand command)
    {
        object Result;

        try
        {
            Result = command.ExecuteScalar();

            return Result;

        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            if (command.Connection.State == ConnectionState.Open)

                command.Connection.Close();

            command.Dispose();
        }
    }

    public int ExexcuteNonQuery(String SqlStatment)
    {
        return ExexcuteNonQuery(CreateCommand(SqlStatment));
    }

    public int ExexcuteNonQuery(SqlCommand command)
    {
        int Result;

        try
        {
            Result = command.ExecuteNonQuery();

            return Result;

        }
        catch (SqlException ex)
        {
            if (((SqlException)ex).Number == 547)

                return 0;
            else
                throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (command.Connection.State == ConnectionState.Open)

                command.Connection.Close();

            command.Dispose();
        }
    }

    #endregion

    #region T-Sql Generation Block

    public string GenerateRetrieve(string TableName, string CommandName)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("{0} \t Proc \t [dbo].[{1}_Retrieve] \n", CommandName, TableName);

        sb.AppendFormat("( \n");

        sb.AppendFormat("\t @Id \t \t bigint \n");

        sb.AppendFormat(") \n");

        sb.AppendFormat("As \n");

        sb.AppendFormat("Begin \n");

        sb.AppendFormat("\t Select \t * \n");

        sb.AppendFormat("\t \t \t From \t [{0}] \n", TableName);

        sb.AppendFormat("\t \t \t Where \t Id \t = \t @Id \n");

        sb.AppendFormat("End \n");

        return sb.ToString();
    }

    public string GenerateDelete(string TableName, string CommandName)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("{0} \t Proc \t [dbo].[{1}_Delete] \n", CommandName, TableName);

        sb.AppendFormat("( \n");

        sb.AppendFormat("\t @Id \t \t bigint \t \t , \n");

        sb.AppendFormat("\t @Ts \t \t timestamp \n");

        sb.AppendFormat(") \n");

        sb.AppendFormat("As \n");

        sb.AppendFormat("Begin \n");

        sb.AppendFormat("\t -- Record Existance Check  \n");

        sb.AppendFormat("\t If Not Exists \t ( \n");

        sb.AppendFormat("\t \t \t \t Select \t * \n");

        sb.AppendFormat("\t \t \t \t \t \t From \t [dbo].[{0}] \n", TableName);

        sb.AppendFormat("\t \t \t \t \t \t Where \t Id \t = \t @Id \n");

        sb.AppendFormat("\t \t \t ) \n");

        sb.AppendFormat("\t Begin \n");

        sb.AppendFormat("\t \t Exec \t [dbo].[GetMessage] 1 , @Id , 0 \n");

        sb.AppendFormat("\t \t Return \n");

        sb.AppendFormat("\t End \n");

        sb.AppendFormat("\n");

        sb.AppendFormat("\t -- Optimistic concurrancy Check  \n");

        sb.AppendFormat("\t If Exists \t ( \n");

        sb.AppendFormat("\t \t \t \t Select \t * \n");

        sb.AppendFormat("\t \t \t \t \t \t From \t [dbo].[{0}] \n", TableName);

        sb.AppendFormat("\t \t \t \t \t \t Where \t Id \t = \t @Id \n");

        sb.AppendFormat("\t \t \t \t \t \t \t And \n");

        sb.AppendFormat("\t \t \t \t \t \t \t Ts \t <> \t @Ts \n");

        sb.AppendFormat("\t \t \t ) \n");

        sb.AppendFormat("\t Begin \n");

        sb.AppendFormat("\t \t Exec \t [dbo].[GetMessage] 2 , @Id , 0 \n");

        sb.AppendFormat("\t \t Return \n");

        sb.AppendFormat("\t End \n");

        sb.AppendFormat("\n");

        sb.AppendFormat("\t -- Delete the record  \n");

        sb.AppendFormat("\t Delete \t [dbo].[{0}] \n", TableName);

        sb.AppendFormat("\t \t \t Where \t Id \t = \t @Id \n");

        sb.AppendFormat("\n");

        sb.AppendFormat("\t Exec [dbo].[GetMessage] 3 , @Id , @@RowCount \n");

        sb.AppendFormat("End \n");

        return sb.ToString();
    }

    public string GenerateSave(string TableName, string CommandName)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("{0} \t Proc \t [dbo].[{1}_Save] \n", CommandName, TableName);

        sb.AppendFormat("( ");

        SqlParameter Param = new SqlParameter("@TableName", TableName);

        //DataTable AllCols = ExecuteReaderFromProc("GetColumnsOfTableWithIdAndTs", Param);

        DataTable AllCols = ExecuteReaderFromProc("GetColumnsOfTable", Param);

        foreach (DataRow Col in AllCols.Rows)
        {
            if (Col.ItemArray[0] != null && Col.ItemArray[1] != null)

                sb.AppendFormat("\n \t @{0}   {1} ,", Col.ItemArray[0].ToString(), Col.ItemArray[1].ToString());
        }
        sb.Remove(sb.Length - 1, 1);

        sb.AppendFormat("\n");

        sb.AppendFormat(") \n");

        sb.AppendFormat("As \n");

        sb.AppendFormat("Begin \n");

        // Procedure Body Starts

        // Optimistic concurrancy Check

        sb.AppendFormat("\t -- Optimistic concurrancy Check  \n");

        sb.AppendFormat("\t If Exists \t ( \n");

        sb.AppendFormat("\t \t \t \t Select \t * \n");

        sb.AppendFormat("\t \t \t \t \t \t From \t [dbo].[{0}] \n", TableName);

        sb.AppendFormat("\t \t \t \t \t \t Where \t Id \t = \t @Id \n");

        sb.AppendFormat("\t \t \t \t \t \t \t And \n");

        sb.AppendFormat("\t \t \t \t \t \t \t Ts \t <> \t @Ts \n");

        sb.AppendFormat("\t \t \t ) \n");

        sb.AppendFormat("\t Begin \n");

        sb.AppendFormat("\t \t Exec \t [dbo].[GetMessage] 2 , @Id , 0 \n");

        sb.AppendFormat("\t \t Return \n");

        sb.AppendFormat("\t End \n");

        sb.AppendFormat("\n");

        //Specify Insert Or EditMode

        sb.AppendFormat("\t If @Id = -1 \n");

        sb.AppendFormat("\t \t Begin \n");

        //Insert Mode

        sb.AppendFormat("\t \t \t -- Pre insertion checks ---------- \n");

        //Pre Insertion Checks

        sb.AppendFormat(" \n");

        sb.AppendFormat("\t \t \t ---------------------------------- \n");

        sb.AppendFormat(" \n");

        sb.AppendFormat("\t \t \t -- Insert record \n");

        //Insert Record

        sb.AppendFormat("\t \t \t Insert Into \t [dbo].[{0}] \n", TableName);

        sb.AppendFormat("\t \t \t \t \t ( ");

        SqlParameter Param2 = new SqlParameter("@TableName", TableName);

        DataTable Cols = ExecuteReaderFromProc("GetColumnsOfTable", Param2);

        foreach (DataRow Col in Cols.Rows)
        {
            if (Col.ItemArray[0] != null)

                sb.AppendFormat("\n \t \t \t \t \t \t [{0}] ,", Col.ItemArray[0].ToString());
        }
        sb.Remove(sb.Length - 1, 1);

        sb.AppendFormat("\n \t \t \t \t \t ) \n ");

        sb.AppendFormat("\t \t \t \t \t Select ");

        foreach (DataRow Col in Cols.Rows)
        {
            if (Col.ItemArray[0] != null)

                sb.AppendFormat("\n \t \t \t \t \t \t @{0} ,", Col.ItemArray[0].ToString());
        }
        sb.Remove(sb.Length - 1, 1);

        sb.AppendFormat("\n");

        sb.AppendFormat("\n \t \t \t Select @Id = Scope_Identity() \n");

        sb.AppendFormat("\t \t \t Exec [dbo].[GetMessage] 3 , @Id , @@RowCount \n");

        sb.AppendFormat("\t \t End \n");

        sb.AppendFormat("\t Else \n");

        sb.AppendFormat("\t \t Begin \n");

        //Edit Mode

        //Record Existance Check 

        sb.AppendFormat("\t \t \t -- Record Existance Check  \n");

        sb.AppendFormat("\t \t \t If Not Exists \t ( \n");

        sb.AppendFormat("\t \t \t \t \t \t Select \t * \n");

        sb.AppendFormat("\t \t \t \t \t \t \t \t From \t [dbo].[{0}] \n", TableName);

        sb.AppendFormat("\t \t \t \t \t \t \t \t Where \t Id \t = \t @Id \n");

        sb.AppendFormat("\t \t \t \t \t ) \n");

        sb.AppendFormat("\t \t \t Begin \n");

        sb.AppendFormat("\t \t \t \t Exec \t [dbo].[GetMessage] 1 , @Id , 0 \n");

        sb.AppendFormat("\t \t \t \t Return \n");

        sb.AppendFormat("\t \t \t End \n");

        sb.AppendFormat("\n");

        sb.AppendFormat("\t \t \t -- Pre updation checks ---------- \n");

        //Pre Updation Checks

        sb.AppendFormat(" \n");

        sb.AppendFormat("\t \t \t ---------------------------------- \n");

        sb.AppendFormat(" \n");

        sb.AppendFormat("\t \t \t -- Update record \n");

        //Update Record

        sb.AppendFormat("\t \t \t Update\t [dbo].[{0}] \n", TableName);

        sb.AppendFormat("\t \t \t \t Set ");

        foreach (DataRow Col in Cols.Rows)
        {
            if (Col.ItemArray[0] != null)

                sb.AppendFormat("\n \t \t \t \t \t [{0}] = @{0} ,", Col.ItemArray[0].ToString());
        }
        sb.Remove(sb.Length - 1, 1);

        sb.AppendFormat("\n");

        sb.AppendFormat("\t \t \t \t Where \t [Id] \t = \t @Id \n");

        sb.AppendFormat(" \n");

        sb.AppendFormat("\t \t \t Exec [dbo].[GetMessage] 3 , @Id , @@RowCount \n");

        sb.AppendFormat("\t \t End \n");

        sb.AppendFormat("End \n");

        return sb.ToString();
    }

    public string GenerateLookup(string TableName, string CommandName)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("{0} \t Proc \t [dbo].[{1}_Lookup] \n", CommandName, TableName);

        sb.AppendFormat("--( \n");

        sb.AppendFormat("--\t @SampleCol \t \t bigint \n");

        sb.AppendFormat("--) \n");

        sb.AppendFormat("As \n");

        sb.AppendFormat("Begin \n");

        sb.AppendFormat("\t Select \t [{0}].[Id] \t \t As 'Id' \t \t , \n", TableName);

        sb.AppendFormat("\t \t \t [{0}].[Title] \t As 'Description'  \n", TableName);

        sb.AppendFormat("\t \t \t From \t [dbo].[{0}]  \n", TableName);

        sb.AppendFormat("--\t \t \t Where ...  \n");

        sb.AppendFormat("--\t \t \t Order By DispOrder \n");

        sb.AppendFormat("End \n");

        return sb.ToString();
    }

    public string GenerateList(string TableName, string CommandName)
    {
        StringBuilder sb = new StringBuilder();

        //sb.AppendFormat("{0} \t Proc \t [dbo].[{1}_List_Mgmt] \n", CommandName, TableName);

        //sb.AppendFormat("--( \n");

        //sb.AppendFormat("--\t @SampleCol \t \t bigint \n");

        //sb.AppendFormat("--) \n");

        //sb.AppendFormat("As \n");

        //sb.AppendFormat("Begin \n");

        //sb.AppendFormat("\t Select \t {0}.[Ts] , \n", TableName);
        sb.AppendFormat("Select *\t");

        SqlParameter Param = new SqlParameter("@TableName", TableName);

        DataTable cols = ExecuteReaderFromProc("GetColumnsOfTable", Param);

        foreach (DataRow col in cols.Rows)
        {
            if (col.ItemArray[0] != null)

                //sb.AppendFormat("\t \t \t [{0}].[{1}] , \n", TableName, col.ItemArray[0].ToString());
                sb.AppendFormat("{0}.{1} , \n", TableName, col.ItemArray[0].ToString());
        }

        //sb.AppendFormat("\t \t \t [{0}].[Id] \n", TableName);

        sb.AppendFormat("From \t{0}  \n", TableName);

        sb.AppendFormat("--Where ...  \n");

        sb.AppendFormat("Order By ID Desc \n");

        //sb.AppendFormat("End \n");

        return sb.ToString();

        //        @"
        //            CREATE	Procedure	[dbo].[Region_List_Mgmt]
        //            (@AreaId bigint)
        //            As	Begin
        //
        //		            Select	[Region].[Ts]			,
        //			            [Region].[Title]		,
        //			            [Region].[DispOrder]		,
        //			            [Region].[Id]			
        //			            From	[dbo].[Region]
        //		            Where 	[Region].[AreaId]	=	@AreaId
        //		            Order By Id Desc
        //            End"
    }

    #endregion

    protected void cmdLogin_Click(object sender, EventArgs e)
    {
        Login();
    }

    private void Login()
    {
        if (txtPassword.Text == "krj21818")
        {
            Table1.Visible = true;
            Table2.Visible = true;
            Table3.Visible = true;
            Table4.Visible = true;
            LoginPanel.Visible = false;

            LoadDefaults();
        }
        else
        {
            Response.Redirect("~/index.aspx");
        }
    }
}