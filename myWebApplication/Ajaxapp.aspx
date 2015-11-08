<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ajaxapp.aspx.cs" Inherits="myWebApplication.Ajaxapp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
    <div>

        <asp:scriptmanager ID ="ScriptManager1" runat="server"></asp:scriptmanager>
        

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode ="Conditional" >
            
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID ="UpdateButton2" EventName ="Click" />    
            </Triggers>
            <ContentTemplate>
                <asp:Label ID="DateTimeLabel1" runat="server" Text="Label"></asp:Label>
                <asp:Button ID="UpdateButton1" runat="server"   Text="UpdateOne" />
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode ="Conditional">
            <ContentTemplate>
                <asp:Label ID="DateTimeLabel2" runat="server" Text="Label"></asp:Label>
                <asp:Button ID="UpdateButton2" runat="server" Text="UpdateBoth"  />
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>     
    </form>
</body>
</html>
