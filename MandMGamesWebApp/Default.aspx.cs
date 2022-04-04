using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Drawing;

//System.Web.HttpRuntime.UnloadAppDomain();


public partial class _Default : System.Web.UI.Page
{
    //varbiable for cash total
    public static decimal cashtot = 0;
    //variable for trade total
    public static decimal tradetot = 0;
    //variable for list of games with prices
    public static string glist = "";

    //public static int currindex = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Submit(Object sender, EventArgs e)
    {
            //variables get the values of the form fields
            var gname = Request.QueryString["Name"];
            var upc = Request.QueryString["UPC"];
            var complete = completeness.Value;
            var condition1 = condition.Value;
            var console1 = console.Value;
            var selected = "";
        //make's sure that there is something in the game name text field
        if (!string.IsNullOrEmpty(gname))
        {
            //generates select statement for SQL database query
            selected = "SELECT Console, Name, BasePrice" + complete + ", OurPrice" + complete + "_Cash, OurPrice" + complete + "_Trade FROM Games INNER JOIN OurPrices ON Games.GameID=OurPrices.GameID WHERE Name Like'%" + gname + "%' AND Condition=" + condition1;
            //If all consoles are selected than console is not included in the selection
            if (console.Value == "All Consoles")
            {
                GamesDB.SelectCommand = selected;
            }
            //If a console is selected then which one it is is included in the selection
            else if(console.Value != "All Consoles")
            {
                GamesDB.SelectCommand = selected + " AND Console='" + console1 + "'";  
            }
            //Makes sure that if the user input a game through the name then there is focus on it for the next entry
            name.Focus();
            //clear text field
            name.Text = "";
        }
        //Used for if the UPC is used instead of name for finding a game
        else if (!string.IsNullOrEmpty(upc))
        {
            selected = "SELECT Console, Name, BasePrice" + complete + ", OurPrice" + complete + "_Cash, OurPrice" + complete + "_Trade FROM Games INNER JOIN OurPrices ON Games.GameID=OurPrices.GameID WHERE UPC='" + upc + "' AND Condition=" + condition1;
            //if the user specified a console or not
            if (console.Value == "All Consoles")
            {
                GamesDB.SelectCommand = selected;
            }
            else
            {
                GamesDB.SelectCommand= selected + " AND Console='" + console1 + "'";
            }
            UPC.Focus();
            UPC.Text = "";
        }
        else { }
        gamePriceGrid.DataBind();
    }
    
    public void AddtoTotal(Object sender, EventArgs e)
    {
        
        //if there is only one row retuned from the query search it is automatically added to the total and list of games for the sake of speed
        //if there is more than one row than user must select which one they want
        if (gamePriceGrid.Rows.Count > 0 && gamePriceGrid.Rows.Count < 2)
        {
            showRecept(0);
        }
    }
    public void Clear (Object sender, EventArgs e)
    {
        //clears out the totals and list of games and restores thier values to default
        cashtot = 0;
        tradetot = 0;
        glist = "";
        CashTotal.Text = "$0";
        TradeTotal.Text = "$0";
        Gist.Text = "";
    }
    //the following two methods are used for clicking on and selecting rows from the table
    protected void OnRowDataBound(Object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)//
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gamePriceGrid, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row";
        }
    }
    public void OnSelectedIndexChanged(Object sender, EventArgs e)
    {
        foreach (GridViewRow row in gamePriceGrid.Rows)
        {
            //if a row is selected it changes color to show that it has been and adds to total and to the list
            if(row.RowIndex == gamePriceGrid.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                row.ToolTip = String.Empty;
                showRecept(row.RowIndex);

            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                row.ToolTip = "Click to select this row";
            }
            
        }
        //gamePriceGrid.SelectedIndex = -1;

        
    }
    private void showRecept(int i)//this method adds to the running totals and to the current list of games selected
    {
        GridViewRow row2 = gamePriceGrid.Rows[i];
        //adds to cash total
        var cash = row2.Cells[3].Text.ToString();
        decimal cashdec = decimal.Parse(cash);
        cashtot = cashtot + cashdec;
        CashTotal.Text = "$" + cashtot.ToString();

        //adds to trade total
        var credit = row2.Cells[4].Text.ToString();
        decimal creditdec = decimal.Parse(credit);
        tradetot = tradetot + creditdec;
        TradeTotal.Text = "$" + tradetot.ToString();

        //adds to game list
        var gname2 = row2.Cells[0].Text.ToString() + " " + row2.Cells[1].Text.ToString() + " Cash:$" + cash + " Trade Credit:$" + credit;
        glist += gname2 + "<br>";
        Gist.Text = glist;
    }

}