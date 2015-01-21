<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="RefIdSP.Status" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Status: <asp:Label runat="server" ID="AuthStatus"></asp:Label>
        </div>
        <div>
            Name: <asp:Label runat="server" ID="Name"></asp:Label>
        </div>
        <div>
            <asp:Button runat="server" ID="startSSO" OnClick="RunSso"/>
        </div>
    </form>
</body>
</html>
