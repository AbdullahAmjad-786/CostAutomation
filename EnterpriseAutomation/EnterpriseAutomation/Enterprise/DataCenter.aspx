<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataCenter.aspx.cs" Inherits="EnterpriseAutomation.Enterprise.DataCenter" %>

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
            <li><a href="Products.aspx"><em class="fa fa-pagelines">&nbsp;</em> Products</a></li>
            <li><a href="ImportExport.aspx"><em class="fa fa-pagelines">&nbsp;</em> Import/Export</a></li>
			<li class="active"><a href="DataCenter.aspx"><em class="fa fa-calendar">&nbsp;</em> Data Center</a></li>
			<li><a href="Login.aspx"><em class="fa fa-power-off">&nbsp;</em> Logout</a></li>
		</ul>
	</div><!--/.sidebar-->
		
	<div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="#">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">Data Center</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Data Center</h1>
			</div>
		</div><!--/.row-->

		<div class="panel panel-container">
			<div class="row">

                <div class="col-md-3">
				<div class="panel panel-default">
					<div class="panel-heading">
						From
						<ul class="pull-right panel-settings panel-button-tab-right">
							<li class="dropdown"><a class="pull-right dropdown-toggle" data-toggle="dropdown" href="#">
								<em class="fa fa-cogs"></em>
							</a>
								<ul class="dropdown-menu dropdown-menu-right">
									<li>
										<ul class="dropdown-settings">
											<li><a href="#">
												<em class="fa fa-cog"></em> Settings 1
											</a></li>
											<li class="divider"></li>
											<li><a href="#">
												<em class="fa fa-cog"></em> Settings 2
											</a></li>
											<li class="divider"></li>
											<li><a href="#">
												<em class="fa fa-cog"></em> Settings 3
											</a></li>
										</ul>
									</li>
								</ul>
							</li>
						</ul>
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
						<ul class="pull-right panel-settings panel-button-tab-right">
							<li class="dropdown"><a class="pull-right dropdown-toggle" data-toggle="dropdown" href="#">
								<em class="fa fa-cogs"></em>
							</a>
								<ul class="dropdown-menu dropdown-menu-right">
									<li>
										<ul class="dropdown-settings">
											<li><a href="#">
												<em class="fa fa-cog"></em> Settings 1
											</a></li>
											<li class="divider"></li>
											<li><a href="#">
												<em class="fa fa-cog"></em> Settings 2
											</a></li>
											<li class="divider"></li>
											<li><a href="#">
												<em class="fa fa-cog"></em> Settings 3
											</a></li>
										</ul>
									</li>
								</ul>
							</li>
						</ul>
						<span class="pull-right clickable panel-toggle panel-button-tab-left"><em class="fa fa-toggle-up"></em></span></div>
					<div class="panel-body">
						<div id="calendar2" runat="server"></div>
					</div>
				</div>
            </div>


               <div class="col-xs-8 col-md-8 col-lg-6 no-padding">
					<div class="panel panel-teal panel-widget border-right">
						<div class="row no-padding">
						<div	<asp:Label ID="Label1" runat="server" CssClass="text-primary" Text="Current Available Data"></asp:Label> </div>
						</div>
					</div>
				</div>
				<div class="col-xs-8 col-md-8 col-lg-6 no-padding">
					<div class="panel panel-teal panel-widget border-right">
						<div class="row no-padding"><em class="fa fa-xl fa-calendar color-blue"></em>
						<div	<asp:Label ID="lblFrom" runat="server" CssClass="large" Text=""></asp:Label> </div>
							<div class="text-muted">From Date</div>
						</div>
					</div>
				</div>
				<div class="col-xs-8 col-md-8 col-lg-6 no-padding">
					<div class="panel panel-blue panel-widget border-right">
						<div class="row no-padding"><em class="fa fa-xl fa-calendar color-orange"></em>
							<div	<asp:Label ID="lblTo" runat="server" CssClass="large" Text=""></asp:Label> </div>
							<div class="text-muted">To Date</div>
						</div>
					</div>
				</div>
			</div><!--/.row-->
		</div>
         <form id="form1" runat="server">
       <div>
           <asp:HiddenField ID="hfstartDate" runat="server" value=""/>
           <asp:HiddenField ID="hfendDate" runat="server" value=""/>
            <asp:Button ID="btnImport" CssClass="btn btn-primary" runat="server" Text="Import Data" OnClick="btnlogin_Click" />
            <asp:Button ID="btnClear" CssClass="btn btn-primary" runat="server" Text="Clear All" OnClick="btnClear_Click" />
            <asp:Label ID="lblMsg" runat="server" style="z-index: 1; left: 5px; top: 0px; position: relative; width: 210px" Text="Email or Password is not Correct" CssClass="text-danger" Visible="False"></asp:Label>
           </div>
         </form>   
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
    <script src="js/custom2.js"></script>
    <script src="js/custom3.js"></script>
		
</body>
</html>