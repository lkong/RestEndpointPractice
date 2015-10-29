<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ActivehoursAssignment.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #Text1 {
            height: 517px;
            width: 560px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="NotSoFancyTextEditor"></asp:Label>
    
    </div>
        <p>
            <asp:TextBox ID="TextBox1" runat="server" Height="600px" Width="600px" TextMode="MultiLine"></asp:TextBox>
        </p>
    </form>
</body>
</html>
