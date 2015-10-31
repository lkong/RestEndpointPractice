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
            <asp:TextBox ID="InputTextBox" runat="server" Height="30px" Width="600px"></asp:TextBox>
            <asp:Button ID="SpellCheckButton" runat="server" OnClick="SpellCheckButton_Click" Text="Spell Check" />
            <asp:Button ID="InsertWordButton" runat="server" OnClick="InsertWordButton_Click" Text="This is a word!" />
        </p>
        <asp:Label ID="SpellSuggestion" runat="server"></asp:Label>
    </form>
</body>
</html>
