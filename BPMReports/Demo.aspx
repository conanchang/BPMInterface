<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="BPMReports.Demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <div align="center">
	<p>
		<strong><span style="font-size:24px;"></span><span style="font-size:24px;">报表Demo</span></strong> 
	</p>
	<div align="left">
		<table style="width:100%;" cellspacing="0" cellpadding="2" bordercolor="#000000" border="1">
			<tbody>
				<tr>
					<td colspan="2">
						查询条件<br />
					</td>
				</tr>
				<tr>
					<td>
						供应商名称：<br />
					</td>
					<td>
						<asp:TextBox ID="tbxVendor" runat="server" Width="320px"></asp:TextBox>
						<asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" />
                        <br />
					</td>
				</tr>
			</tbody>
		</table>
        <br />
        <div style="text-align: center"  align="center">
            <table class="auto-style1">
                <tr>
                    <td align ="center">
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView></td>
                </tr>
            </table>
        </div>
        
<br />
<br />
	</div>
</div>
    </form>
</body>
</html>
