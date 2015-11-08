
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="myWebApplication.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type ="text/css">
        .gridViewHeader a:link{
            text-decoration:none;
        }
        tr:hover td {
            color:white;
        }

        tr:hover td a:link{
            color:white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div  style="margin-left:auto; margin-right:auto;width:1000px;align-content:center;">


        <asp:GridView ID="peopleGridView" runat="server" HeaderStyle-CssClass="gridViewHeader" AllowSorting ="True" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" OnSorting="peopleGridView_Sorting" OnRowCreated="peopleGridView_RowCreated" Width="124px" AutoGenerateColumns="False" >
            <AlternatingRowStyle BackColor="White" />

            <Columns>
                <asp:BoundField DataField="emplid" HeaderText="Emplid" SortExpression="emplid" ItemStyle-Wrap ="false"/>
                <asp:BoundField DataField="firstName" HeaderText="First Name" SortExpression="firstName" ItemStyle-Wrap ="false"/>
                <asp:BoundField DataField="lastName" HeaderText="Last Name" SortExpression="lastName" ItemStyle-Wrap ="false"/>
                <asp:BoundField DataField="department" HeaderText="Department" SortExpression="department" ItemStyle-Wrap ="false"/>
                <asp:BoundField DataField="room" HeaderText="Room" SortExpression="room" ItemStyle-Wrap ="false"/>
                <asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone" ItemStyle-Wrap ="false"/>
                <asp:BoundField DataField="email" DataFormatString="&lt;a href = mailto:{0}&gt;{0}&lt;/a&gt;" HeaderText="eMail" HtmlEncodeFormatString="False" SortExpression="email" ItemStyle-CssClass="gridViewHeader" ItemStyle-Wrap ="false"/>
            </Columns>


            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />

        </asp:GridView>

       
    </div>
    </form>
</body>
</html>
