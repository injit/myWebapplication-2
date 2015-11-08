<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="alternate.aspx.cs" Inherits="myWebApplication.alternate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" >

    <div   id="container">


        <asp:GridView ID="peopleGridView" runat="server" HeaderStyle-CssClass="gridViewHeader" AllowSorting ="True" CellPadding="4" GridLines="Horizontal" 
            OnSorting="peopleGridView_Sorting" OnRowCreated="peopleGridView_RowCreated" Width="950px"  AutoGenerateColumns="False" AllowPaging="True"
             OnPageIndexChanging="peopleGridView_PageIndexChanging" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" PageSize="20" RowStyle-Wrap="true" style="margin-left: 0px" >

            <Columns>
                <asp:BoundField DataField="emplid" HeaderText="Emplid" SortExpression="emplid" ItemStyle-Wrap="False" ItemStyle-CssClass="EMP">
<ItemStyle Wrap="False" CssClass="EMP"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="firstName" HeaderText="First Name" SortExpression="firstName" ItemStyle-Wrap="False" ItemStyle-CssClass="FN">
<ItemStyle Wrap="False" CssClass="FN"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="lastName" HeaderText="Last Name" SortExpression="lastName" ItemStyle-Wrap="False"  ItemStyle-CssClass="LN">
<ItemStyle Wrap="False" CssClass="LN"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="department" HeaderText="Department" SortExpression="department" ItemStyle-Wrap="False"  ItemStyle-CssClass="DPT">
<ItemStyle Wrap="False" CssClass="DPT"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="room" HeaderText="Room" SortExpression="room" ItemStyle-Wrap="False"  ItemStyle-CssClass="RM">
<ItemStyle Wrap="False" CssClass="RM"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="phone" HeaderText="Telephone" SortExpression="phone" ItemStyle-Wrap="false" ItemStyle-CssClass="PHN" >
<ItemStyle Wrap="False" CssClass="PHN"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="eMail" ItemStyle-CssClass="gridViewHeader" SortExpression="email"  ItemStyle-Wrap="False" >

                    <itemtemplate>

                    <a href='mailto:<%# Eval("email") %>'><%# Eval("email") %></a>

                    </itemtemplate>

                    <ItemStyle Wrap="False"  CssClass="gridViewHeader"></ItemStyle>

                </asp:TemplateField>
            </Columns>


            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" Wrap="false" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" Height="30px" VerticalAlign="Bottom" BorderStyle="None" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />

        </asp:GridView>

       
    </div>
    </form>
</body>
</html>
