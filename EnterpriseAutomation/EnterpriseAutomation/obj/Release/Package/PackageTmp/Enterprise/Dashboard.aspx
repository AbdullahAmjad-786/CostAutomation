<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EnterpriseAutomation.lumino.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html>
<head>
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>Techlogix - Azure Reporting Portal</title>
	<link href="css/bootstrap.min.css" rel="stylesheet">
	<link href="css/font-awesome.min.css" rel="stylesheet">
	<link href="css/datepicker3.css" rel="stylesheet">
	<link href="css/styles.css" rel="stylesheet">
	
	<!--Custom Font-->
	<link href="https://fonts.googleapis.com/css?family=Montserrat:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">
	<!--[if lt IE 9]>
	<script src="js/html5shiv.js"></script>
	<script src="js/respond.min.js"></script>
	<![endif]-->
</head>
<body>
	<nav class="navbar navbar-custom navbar-fixed-top" role="navigation">
		<div class="container-fluid">
			<div class="navbar-header">
				<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#sidebar-collapse"><span class="sr-only">Toggle navigation</span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span></button>
				<a class="navbar-brand" href="#"><span>Techlogix-CloudOps</span>Admin</a>

			</div>
		</div><!-- /.container-fluid -->
	</nav>
	<div id="sidebar-collapse" class="col-sm-3 col-lg-2 sidebar">
		<div class="profile-sidebar">
			<div class="profile-userpic">
				<img src="http://placehold.it/50/30a5ff/fff" class="img-responsive" alt="">
			</div>
			<div class="profile-usertitle">
				<div class="profile-usertitle-name">Admin</div>
				<div class="profile-usertitle-status"><span class="indicator label-success"></span>Online</div>
			</div>
			<div class="clear"></div>
		</div>
		<div class="divider"></div>
		<form role="search">
			<div class="form-group">
				<input type="text" class="form-control" placeholder="Search">
			</div>
		</form>
		<ul class="nav menu">
			<li class="active"><a href="Dashboard.aspx"><em class="fa fa-dashboard">&nbsp;</em> Dashboard</a></li>
            <li><a href="Products.aspx"><em class="fa fa-pagelines">&nbsp;</em> Products</a></li>
            <li><a href="ImportExport.aspx"><em class="fa fa-pagelines">&nbsp;</em> Import/Export</a></li>
			<li><a href="DataCenter.aspx"><em class="fa fa-calendar">&nbsp;</em> Data Center</a></li>
			<li><a href="Login.aspx"><em class="fa fa-power-off">&nbsp;</em> Logout</a></li>
		</ul>
	</div><!--/.sidebar-->
		
	<div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="#">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Dashboard</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Dashboard</h1>
			</div>
		</div><!--/.row-->

		<div class="panel panel-container">
			<div class="row">

                <div class="col-md-3">
				<div class="panel panel-default">
					<div class="panel-heading">
						From
						
						<span class="pull-right clickable panel-toggle panel-button-tab-left"><em class="fa fa-toggle-up"></em></span></div>
					<div class="panel-body">
						<div id="calendar1" runat="server"></div>
					</div>
				</div>
            </div>

            <div class="col-md-3">
				<div class="panel panel-default">
					<div class="panel-heading">
						To
						
						<span class="pull-right clickable panel-toggle panel-button-tab-left"><em class="fa fa-toggle-up"></em></span></div>
					<div class="panel-body">
						<div id="calendar2" runat="server"></div>
					</div>
				</div>
            </div>



				<div class="col-xs-8 col-md-8 col-lg-6 no-padding">
					<div class="panel panel-teal panel-widget border-right">
						<div class="row no-padding"><em class="fa fa-xl fa-shopping-cart color-blue"></em>
						<div	<asp:Label ID="lblTotal" runat="server" CssClass="large" Text="$120"></asp:Label> </div>
							<div class="text-muted">Total Cost</div>
						</div>
					</div>
				</div>
			</div><!--/.row-->
		</div>
         <form id="form1" runat="server">
       <div>
           <asp:HiddenField ID="hfLabels" runat="server" value="Heavy"/>
           <asp:HiddenField ID="hfBillsProduct" runat="server" value="Heavy"/>
           <asp:HiddenField ID="hfTags" runat="server" value="Heavy"/>
           <asp:HiddenField ID="hfBillsTags" runat="server" value="Heavy"/>
           <asp:HiddenField ID="hfDates" runat="server" value=""/>
           <asp:HiddenField ID="hfBillsDates" runat="server" value=""/>
           <asp:HiddenField ID="hfstartDate" runat="server" value=""/>
           <asp:HiddenField ID="hfendDate" runat="server" value=""/>
            <asp:Button ID="btnlogin" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnlogin_Click" />
       </div>
             <div class="row">
                 <div class="col-md-12">
                     <div class="panel panel-default">
                <div class="panel-heading">
						Pie Chart - Product Wise 
				

                </div>
                <div class="panel-body">
                 <asp:chart runat="server" ID="PieTemp" DataSourceID="SqlDataSource1" Height="404px" Width="1133px" CssClass="fa-pie-chart" OnLoad="PieTemp_Load">
                     <Series><asp:Series Name="Default" ChartType="Pie" XValueMember="SubscriptionName" YValueMembers="Cost" IsValueShownAsLabel="True" Legend="Legend1"></asp:Series></Series>
                     <ChartAreas><asp:ChartArea Name="ChartArea1">
                     <Position Height="100" Width="100" />
                         <Area3DStyle Enable3D="True" IsClustered="True" />
                     </asp:ChartArea></ChartAreas>
                     <Legends>
                         <asp:Legend Name="Legend1" TextWrapThreshold="100" MaximumAutoSize="100" TableStyle="Tall">
                             <Position Height="31.4381275" Width="20" X="70" Y="3" />
                         </asp:Legend>
                     </Legends>
                 </asp:chart>

                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AzureDBConnectionString %>" >

				    </asp:SqlDataSource>
                    </div>
                         </div>
		</div>
		</div><!--/.row-->

              <div class="row">
			<div class="col-md-6">
				<div class="panel panel-default">
                <div class="panel-heading">
						Top Ten Tags - Cost Wise
						

                </div>
                <div class="panel-body">
                    <asp:GridView ID="gridTags" runat="server" ShowHeaderWhenEmpty="True" CellSpacing="2" Height="397px" Width="350px" BorderStyle="Solid" OnRowCreated="gridTags_RowCreated1">
                        
                        <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" BorderStyle="Double" />
                        
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Font-Strikeout="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <RowStyle ForeColor="#000066" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <SelectedRowStyle Wrap="True" BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
				</div>
              </div>
			</div>

                  <div class="col-md-6">
				<div class="panel panel-default">
                <div class="panel-heading">
						Product Wise Cost - Bar Chart
						

                </div>
                <div class="panel-body">
                    <asp:Chart ID="ProductChart" runat="server" DataSourceID="SqlDataSource2" Height="397px" Width="575px">
                        <Series>
                            <asp:Series Name="Default" ChartType="Bar" IsValueShownAsLabel="True" XValueMember="SubscriptionName" YValueMembers="Cost">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <AxisY>
                                    <MajorGrid Enabled="False" />
                                </AxisY>
                                <AxisX>
                                    <MajorGrid Enabled="False" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
				    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:AzureDBConnectionString %>" SelectCommand="Select SubscriptionName, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData Group by SubscriptionName"></asp:SqlDataSource>
				</div>
              </div>
			</div>
           </div><!--/.row-->


         <div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">
						Date Wise Cost - Bar Chart
						</div>
					<div class="panel-body">
						
					    <asp:Chart ID="DateChart" runat="server" DataSourceID="SqlDataSource3" Height="820px" Width="984px">
                            <Series>
                                <asp:Series ChartType="Bar" IsValueShownAsLabel="True" IsVisibleInLegend="False" Name="Default" XValueMember="Date" YValueMembers="Cost">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY>
                                        <MajorGrid Enabled="False" />
                                    </AxisY>
                                    <AxisX>
                                        <MajorGrid Enabled="False" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:AzureDBConnectionString %>" SelectCommand="Select Date, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData Group by Date order by Date"></asp:SqlDataSource>
						
					</div>
				</div>
			</div>
		</div><!--/.row-->		
                 
       </form>
   

		<!--	<div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">
						Product Wise Cost - Bar Chart
						
						</div>
					<div class="panel-body">
						<div class="canvas-wrapper">
							<canvas class="main-chart" id="bar-chart" height="200" width="600"></canvas>
						</div>
					</div>
				</div>
			</div>
		</div>/.row-->		

      


      <!--  <div class="row">
			<div class="col-lg-12">
				<div class="panel panel-default">
					<div class="panel-heading">
						Date Wise Cost - Bar Chart
						</div>
					<div class="panel-body">
						<div class="canvas-wrapper">
							<canvas class="main-chart" id="bar-chart-Date" height="200" width="600"></canvas>
						</div>
					</div>
				</div>
			</div>
		</div>/.row-->		


				


			<div class="col-sm-12">
				<p class="back-link">Copyright © <a href="https://www.techlogix.com/">Techlogix. All rights reserved</a></p>
			</div>
		</div><!--/.row-->
	</div>	<!--/.main-->
	
	<script src="js/jquery-1.11.1.min.js"></script>
	<script src="js/bootstrap.min.js"></script>
	<script src="js/chart.min.js"></script>
	<script src="js/chart-data.js"></script>
	<script src="js/easypiechart.js"></script>
	<script src="js/easypiechart-data.js"></script>
	<script src="js/bootstrap-datepicker.js"></script>
	<script src="js/custom.js"></script>
    <script src="js/custom2.js"></script>
    <script src="js/custom3.js"></script>
	<script>
		window.onload = function () {
   /* var chart2 = document.getElementById("bar-chart").getContext("2d");
	window.myBar = new Chart(chart2).Bar(barChartData, {
	responsive: true,
	scaleLineColor: "rgba(0,0,0,.2)",
	scaleGridLineColor: "rgba(0,0,0,.05)",
    scaleFontColor: "#c5c7cc",
    responsive : true,
    tooltipFillColor: "rgba(0,0,0,0)",
    tooltipFontColor: "#444",
    tooltipEvents: [],
    tooltipCaretSize: 0,
    onAnimationComplete: function()
    {
        this.showTooltip(this.datasets[0].bars, true);
    }
    });
    var chart6 = document.getElementById("bar-chart-Date").getContext("2d");
	window.myBar = new Chart(chart6).Bar(barChartDataDate, {
	responsive: true,
	scaleLineColor: "rgba(0,0,0,.2)",
	scaleGridLineColor: "rgba(0,0,0,.05)",
    scaleFontColor: "#c5c7cc",
    showTooltips: false,
    onAnimationComplete: function () {

        var ctx = this.chart.ctx;
        ctx.font = this.scale.font;
        ctx.fillStyle = "#444";
        ctx.textAlign = "center";
        ctx.textBaseline = "bottom";

        this.datasets.forEach(function (dataset) {
            dataset.bars.forEach(function (bar) {
                ctx.fillText(bar.value, bar.x, bar.y - 5);
            });
        })
    }
    });*/
};
	</script>
		
</body>
</html>
