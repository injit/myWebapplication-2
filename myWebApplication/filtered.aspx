<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="filtered.aspx.cs" Inherits="myWebApplication.filtered" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/filteredstyle.css" rel="stylesheet" type="text/css"/>
    <script runat ="server">
        protected String GetTime()
        {
            return DateTime.Now.ToString();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div  id="container">

        <!--Current server time is <% =GetTime() %>-->
        <asp:scriptmanager ID ="ScriptManager1" runat="server"></asp:scriptmanager>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                
                <asp:Label ID="Label1" runat="server" Text="Department"></asp:Label>&nbsp&nbsp
                <asp:DropDownList ID="DropDownList1" runat="server"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            
            </ContentTemplate>
        </asp:UpdatePanel><br />

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <Triggers>
                      <asp:AsyncPostBackTrigger ControlID ="DropDownList1" EventName ="SelectedIndexChanged" />    
            </Triggers>
            <ContentTemplate>
                <!--Current server time is <% =GetTime() %>-->
                <asp:GridView ID="peopleGridView" runat="server" HeaderStyle-CssClass="gridViewHeader" AllowSorting ="True" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" OnSorting="peopleGridView_Sorting" 
                    OnRowCreated="peopleGridView_RowCreated" Width="850px" AutoGenerateColumns="False" >
                    <AlternatingRowStyle BackColor="White" />
            
                        <Columns>
                            <asp:BoundField DataField="lastName" HeaderText="Last Name" SortExpression="lastName" ItemStyle-Wrap="false"   ItemStyle-CssClass="LN" />
                            <asp:BoundField DataField="firstName" HeaderText="First Name" SortExpression="firstName" ItemStyle-Wrap="false" ItemStyle-CssClass="FN" />
                            <asp:BoundField DataField="department" HeaderText="Department" SortExpression="department" ItemStyle-Wrap="false" ItemStyle-CssClass="DPT" />
                            <asp:BoundField DataField="room" HeaderText="Room" SortExpression="room" ItemStyle-Wrap="false" ItemStyle-CssClass="RM" />
                            <asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone" ItemStyle-Wrap="false" ItemStyle-CssClass="PHN" />
                            <asp:BoundField DataField="email" DataFormatString="&lt;a href = mailto:{0}&gt;{0}&lt;/a&gt;" HeaderText="eMail" HtmlEncodeFormatString="False" SortExpression="email" ItemStyle-CssClass="gridViewHeader" ItemStyle-Wrap="false" />
                                
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
            </ContentTemplate>
        </asp:UpdatePanel>

        
       
    </div>
    </form>
</body>
</html>
