<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="EnterpriseAutomation.Enterprise.Products" %>

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
            <li><a href="Dashboard.aspx"><em class="fa fa-dashboard">&nbsp;</em> Dashboard</a></li>
			<li class="active"><a href="Products.aspx"><em class="fa fa-pagelines">&nbsp;</em> Products</a></li>
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
				<li class="active">Products</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Products</h1>
			</div>
		</div><!--/.row-->

        <div class="row">
          <div class="col-md-3">
        <div class="form-group">
			<label>Selects</label>
			<select class="form-control" id="productValue" name="productValue" runat="server" onchange="getComboA(this)">
				<option value ="Almusnet">Almusnet</option>
				<option value ="Vicenna">Vicenna</option>
				<option value ="TMX">TMX</option>
			</select>

        </div>
              </div>

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
						
						<span id ="toggle" class="pull-right clickable panel-toggle panel-button-tab-left" runat="server"><em class="fa fa-toggle-up"></em></span></div>
					<div class="panel-body">
						<div id="calendar2" runat="server"></div>
					</div>
				</div>
            </div>

       </div>
      
        <div class="row">
			<div class="col-md-12">
                <div>
                    <button Id="btnlogin" runat="server" onserverclick="btnlogin_Click" type="button" class="btn btn-primary">Submit</button>
                     <asp:Label ID="lblMsg" runat="server" style="z-index: 1; left: 5px; top: 0px; position: relative; width: 210px" Text="Email or Password is not Correct" CssClass="text-danger" Visible="False"></asp:Label>
                </div>
            </div>
        </div>

		<div class="panel panel-container">
			<div class="row">

               	<div class="col-xs-8 col-md-8 col-lg-6 no-padding">
					<div class="panel panel-blue panel-widget border-right">
						<div class="row no-padding"><em class="fa fa-xl fa-comments color-orange"></em>
							<div class="large" id="productName" runat="server">ALMUSNET</div>
							<div class="text-muted">Current Product</div>
						</div>
					</div>
				</div>
				<div class="col-xs-8 col-md-8 col-lg-6 no-padding">
					<div class="panel panel-teal panel-widget border-right">
						<div class="row no-padding"><em class="fa fa-xl fa-shopping-cart color-blue"></em>
						<div>	<asp:Label ID="lblTotal" runat="server" CssClass="large" Text="$120"></asp:Label> </div>
							<div class="text-muted">Total Cost of this Product</div>
						</div>
					</div>
				</div>
			
			</div><!--/.row-->
		</div>

       
         <form id="form1" runat="server">
       <div>
           <asp:HiddenField ID="hfTags" runat="server" value="Heavy"/>
           <asp:HiddenField ID="hfBillsTags" runat="server" value="Heavy"/>
           <asp:HiddenField ID="hfstartDate" runat="server" value=""/>
           <asp:HiddenField ID="hfendDate" runat="server" value=""/>
           <asp:HiddenField ID="hfValue" runat="server" value="Almusnet" OnValueChanged="hfValue_ValueChanged"/>
           
           
           <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AzureDBConnectionString %>" SelectCommand="Select Tags, SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = 'Almusnet' Group by Tags"></asp:SqlDataSource>
           
           <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:AzureDBConnectionString %>" SelectCommand="Select ConsumedService, SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = 'Almusnet' Group by ConsumedService"></asp:SqlDataSource>
           </div>
        
         <div class="row">
			<div class="col-md-6">
				<div class="panel panel-default">
					<div class="panel-heading">
						Tag Wise Cost - Grid View
						</div>
					<div class="panel-body">
					     <asp:GridView ID="gridTags" Height="356px" Width="520px" CssClass="table-bordered" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" HorizontalAlign="Justify" OnRowCreated="gridTags_RowCreated" OnRowDataBound="gridTags_RowDataBound">
                             <FooterStyle BackColor="White" ForeColor="#000066" />
                             <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                             <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                             <RowStyle ForeColor="#000066" HorizontalAlign="Right" />
                             <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
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
						Main Tags Cost - Grid View
						</div>
					<div class="panel-body">
					     <asp:GridView ID="gridMainTag"  Width="520px" CssClass="main-chart" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                             <FooterStyle BackColor="White" ForeColor="#000066" />
                             <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                             <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                             <RowStyle ForeColor="#000066" HorizontalAlign="Right" />
                             <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                             <SortedAscendingCellStyle BackColor="#F1F1F1" />
                             <SortedAscendingHeaderStyle BackColor="#007DBB" />
                             <SortedDescendingCellStyle BackColor="#CAC9C9" />
                             <SortedDescendingHeaderStyle BackColor="#00547E" />
                         </asp:GridView>
					</div>
				</div>
			</div>
		</div><!--/.row-->

        <div class="row">
			<div class="col-md-12">
				<div class="panel panel-default">
					<div class="panel-heading">
						Tag Wise Cost - Bar Chart
						</div>
					<div class="panel-body">
						<asp:Chart ID="TagChart" runat="server" DataSourceID="SqlDataSource1" Height="787px" Width="1139px" EnableTheming="True">
               <Series>
                   <asp:Series Name="Series1" XValueMember="Tags" YValueMembers="Cost" ChartType="Bar" IsValueShownAsLabel="True">
                   </asp:Series>
               </Series>
               <ChartAreas>
                   <asp:ChartArea Name="ChartArea1">
                       <AxisY>
                           <MajorGrid Enabled="False"/>
                       </AxisY>
                       <AxisX>
                           <MajorGrid Enabled="False" />
                       </AxisX>
                   </asp:ChartArea>
               </ChartAreas>
           </asp:Chart>
					</div>
				</div>
			</div>
		</div><!--/.row-->


        <div class="row">
			<div class="col-md-12">
				<div class="panel panel-default">
					<div class="panel-heading">
						Consumed Service Wise Cost - Bar Chart
						</div>
					<div class="panel-body">
						<asp:Chart ID="CServiceChart" runat="server" DataSourceID="SqlDataSource2" Height="571px" Width="1084px">
               <Series>
                   <asp:Series Name="Series1" XValueMember="ConsumedService" YValueMembers="Cost" ChartType="Bar" IsValueShownAsLabel="True">
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
					</div>
				</div>
			</div>
		</div><!--/.row-->

       <!--  <div class="row">
			<div class="col-md-12">
                <div>
                    <asp:Button ID="btnResource" runat="server" Text="Get Resources Count" OnClick="btnResource_Click1" />
                </div>
            </div>
        </div> -->

      </form>
      
        <!--/.row-->

			<div class="col-sm-12">
				<p class="back-link">Copyright © <a href="https://www.techlogix.com/">Techlogix. All rights reserved</a></p>
			</div>
		</div><!--/.row-->
	
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
    <script src="js/product-data.js"></script>
    
		
</body>
</html>