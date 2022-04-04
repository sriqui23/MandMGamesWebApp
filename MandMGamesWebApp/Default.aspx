<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 45px">
    <form id="form1" method="get" runat="server">
        <div>
            <!--This form is used to get information for the SQL query -->
            <label for="Name">Name:</label>
            <asp:TextBox type="text" id="name" name="name" autofocus="on" runat="server"/>
            <label for="UPC">UPC:</label>
            <asp:TextBox type="text" id="UPC" name="UPC" runat="server"/>
            <label for="console">Console:</label>
            <select name="console" id="console" runat="server">
                <option value="All Consoles" selected="selected">All Consoles</option>
                <option value="Xbox 360">Xbox 360</option>
            </select>
            <label for="completeness">Contents</label>
            <select name="completeness" id="completeness" runat="server">
                <option value="New" selected="selected">New</option>
                <option value="CIB">CIB</option>
                <option value="Loose">Loose</option>
            </select>
            <label for="condition">Condition</label>
            <select name="condition" id="condition" runat="server">
                <option value="3" selected="selected">Good</option>
                <option value="2">Average</option>
                <option value="1">Poor</option>
            </select>
            <!--Submit Button for form-->
            <asp:Button text="Submit" OnClick="Submit" runat="server" />
            <p></p>
            
            <!--Datasource and Gridview-->
            <asp:SqlDataSource ID="GamesDB" runat="server" ConnectionString="<%$ ConnectionStrings:MandMGamesPriceDatabaseConnectionString %>" />
            <asp:GridView ID="gamePriceGrid" runat="server" DataSourceID="GamesDB" OnDataBound="AddtoTotal" OnRowDataBound="OnRowDataBound" OnSelectedIndexChanged="OnSelectedIndexChanged" />

            <p></p>
            <!--Game List-->
            <asp:Label ID="Gist" Text="" Name="Gist"  runat="server" />
            <p></p>
            <!--Totals-->
            <label for="Total">Cash Total:</label>
            <asp:Label ID="CashTotal" Text="$0 " runat="server" />
            <label for="TradeTotal">Trade Credit Total:</label>
            <asp:Label ID="TradeTotal" Text=" $0" runat="server" />
            <!--Clear Button-->
            <asp:Button Text="Clear" OnClick ="Clear" runat="server" />
        </div>
    </form>
</body>
</html>
