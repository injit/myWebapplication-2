
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditableCached.aspx.cs" Inherits="myWebApplication.EditableCached" %>
<%@ OutputCache Duration="60" VaryByParam="None" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/filteredstyle.css" rel="stylesheet" type="text/css"/>
    <script>

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div  id="container">
        client: 
        <script>
            document.write(Date());
        </script>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Server: "></asp:Label>
        <asp:Label ID="Label4" runat="server"> </asp:Label>

        <asp:scriptmanager ID ="ScriptManager1" runat="server"></asp:scriptmanager>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="Department"></asp:Label>&nbsp&nbsp
                <asp:DropDownList ID="DropDownList1" runat="server"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                <br /><br />
                <asp:Label ID="Label2" runat="server" Text="Enter Search Value:"></asp:Label><asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox><asp:Button ID="Button1" runat="server" Text="Find"  OnClick="Button1_Click" />

            </ContentTemplate>
        </asp:UpdatePanel><br />

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID ="DropDownList1" EventName ="SelectedIndexChanged" />    
                <asp:AsyncPostBackTrigger ControlID ="Button1" EventName ="Click" />
            </Triggers>

            <ContentTemplate>
                <asp:GridView ID="peopleGridView" runat="server"  HeaderStyle-CssClass="gridViewHeader" AllowSorting ="True" BackColor="White"  DataKeyNames="emplid"
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" OnSorting="peopleGridView_Sorting" 
                    OnRowCreated="peopleGridView_RowCreated"  Width="1200px" AutoGenerateColumns="False"  OnRowEditing ="peopleGridView_RowEditing"  OnRowUpdating="peopleGridView_RowUpdating"
                     OnRowCancelingEdit="peopleGridView_RowCancelingEdit" >
                    <AlternatingRowStyle BackColor="White"  />

                        <Columns>
                            <asp:CommandField  ButtonType="Button" ShowEditButton ="true"   HeaderStyle-CssClass="cmdHeader"  HeaderText="Click To" />
                            <asp:TemplateField HeaderText="Last Name" SortExpression="lastName" ItemStyle-Wrap="false"  ItemStyle-CssClass="LN">
                     
                                <ItemTemplate>
                                    <%# Eval("lastName") %>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox runat= "server" ID="txtlastName" Text='<%# Eval("lastName") %>' CssClass="LN" />

                                </EditItemTemplate>

                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="First Name" SortExpression="firstName" ItemStyle-Wrap="false" ItemStyle-CssClass="FN">
                     
                                <ItemTemplate>
                                    <%# Eval("firstName") %>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox runat= "server" ID="txtfirstName" Text='<%# Eval("firstName") %>'  CssClass="FN"/>

                                </EditItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department" SortExpression="department" ItemStyle-Wrap="false" ItemStyle-CssClass="DPT">
                     
                                <ItemTemplate>
                                    <%# Eval("department") %>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox runat= "server" ID="txtdepartment" Text='<%# Eval("department") %>'  CssClass="DPT"/>

                                </EditItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText ="Room" SortExpression="room" ItemStyle-Wrap="false" ItemStyle-CssClass="RM">
                     
                                <ItemTemplate>
                                    <%# Eval("room") %>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox runat= "server" ID="txtroom" Text='<%# Eval("room") %>'  CssClass="RM"/>

                                </EditItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText ="Phone" SortExpression="phone" ItemStyle-Wrap="false" ItemStyle-CssClass="PHN">
                     
                                <ItemTemplate>
                                    <%# Eval("phone") %>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox runat= "server" ID="txtphone" Text='<%# Eval("phone") %>' CssClass="PHN" />

                                </EditItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText ="eMail" SortExpression="email" ItemStyle-CssClass="gridViewHeader" ItemStyle-Wrap="false">
                                 
                                <ItemTemplate>
                                    <a href='mailto:<%# Eval("email") %>'><%# Eval("email") %></a>
               
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox runat= "server" ID="txtemail" Text='<%# Eval("email") %>' CssClass="gridViewHeader" />

                                </EditItemTemplate>

                            </asp:TemplateField>

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
            
                </asp:GridView><br />
                <asp:Label ID="Label3" runat="server" ></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
        <p>&nbsp;</p>

    </form>
    
</body>
</html>
