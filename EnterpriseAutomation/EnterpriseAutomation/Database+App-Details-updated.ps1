Login-AzureRmAccount

$subscriptionId =     (Get-AzureRmSubscription | Out-GridView -Title 'Select an Azure Subscription' -OutputMode Single)
    Set-AzureRmContext -SubscriptionID $subscriptionId

    $resourcegroupnames= Get-AzureRmResourceGroup
$sqlServerSource = @()
$sqlDBSource = @()
$AppPlanes = @()
 $resources = @()


   foreach($resourcegroupname in $resourcegroupnames)
   {
      $resourcegroup = $resourcegroupname.ResourceGroupName
      $sqlServerSource = Get-AzureRmSqlserver -ResourceGroupName $resourcegroup
     foreach($serversource in $sqlServerSource)
      {
        $sqlDBSource +=Get-AzureRmSqlDatabase -ResourceGroupName $resourcegroup -ServerName $serversource.ServerName | where {$_.DatabaseName -ne 'master'}
      } 
   }

  # $sqlDBSource | Out-GridView   -Title 'inforamtion of databases' -OutputMode Single


  $maxsku = $sqlDBSource[0].CurrentServiceObjectiveName;

  for ($i=1; $i -lt $sqlDBSource.Count; $i++) {
  
    if ($maxsku -lt $sqlDBSource[$i].CurrentServiceObjectiveName) {
    
        $maxsku = $sqlDBSource[$i].CurrentServiceObjectiveName
        
    }
  
  }

  $allskus = @()

  for ($j=0; $j -lt $sqlDBSource.Count; $j++) {
  
    $allskus += $sqlDBSource[$j].CurrentServiceObjectiveName
  
  }



  #[String[]]$uniq = $allskus | Select-Object -Unique


  #for ($i=0; $i -lt $uniq.Count; $i++)
#{
 #   $x=$uniq[$i]
  #  New-Variable -Name "$x" -Value 0
    #Get-Variable -Name "var$i" -ValueOnly
#}




for ($k=0; $k -le [int][convert]::ToInt32($maxsku.Substring(1,($maxsku.length)-1), 10); $k++) {

    


    try
{
    $null = Get-Variable -Scope Global -Name S$k -ErrorAction Stop
    $variableExists = $true
}
catch
{
    $variableExists = $false
}


    if ($variableExists -eq "True" ) {
    Remove-Variable S$k
    New-Variable -Name "S$k" -Value (($allskus | Where-Object {$_ -eq "S$k"}).Count)
    }else {
    
        New-Variable -Name "S$k" -Value (($allskus | Where-Object {$_ -eq "S$k"}).Count)
    
    }

    #$S$k = allskus | Where-Object {$_ -eq "S$k"}

}



$Basic = ($allskus | Where-Object {$_ -eq "Basic"}).Count

$ElasticPool = ($allskus | Where-Object {$_ -eq "ElasticPool"}).Count





####################################### APP PLANS #########################################################



foreach($resourcegroupname in $resourcegroupnames)
   {
       $AppPlanes+= Get-AzureRmAppServicePlan -ResourceGroupName $resourcegroupname.ResourceGroupName
     }
 $appplanecount = $AppPlanes.Count
    
       #$appplanes $resources| Out-GridView  -Title 'Select your app plane'  -OutputMode Single

  $noofsites = 0

   for($b = 0; $b -lt $appplanecount; $b++)
   {
        $appPlane =$AppPlanes[$b]
        $count =$appPlane.NumberOfSites
        $noofsites = $noofsites + $count
   }


    foreach($resourcegroupname in $resourcegroupnames)
   {
       $resources += Get-AzureRmResource -ResourceGroupName $resourcegroupname.ResourceGroupName -ResourceType "microsoft.web/serverfarms"
  
  }
   $basics =0
   $Standard = 0
   $free =0
   $dynamic = 0

   for($c = 0; $c -lt $resources.Count; $c++)
   {

        $resource =  $resources[$c]
        if ($resource.Sku.tier -eq 'Basic')
        { $basics = $basics+1 }
        elseif($resource.Sku.tier -eq 'Standard')
        { $Standard = $Standard+1 }
        elseif($resource.Sku.tier -eq 'Free')
        { $free = $free+1 }
         elseif($resource.Sku.tier -eq 'Dynamic')
        { $dynamic = $dynamic+1 }
        
   }





   
Write-Output ("Total Databases = " + $allskus.Count)


for ($l=0; $l -le [int][convert]::ToInt32($maxsku.Substring(1,($maxsku.length)-1), 10); $l++) {

    Write-output ("S$l = " + (get-variable -Name S$l -ValueOnly))

}



Write-output ("Basic = " + (get-variable -Name Basic -ValueOnly))
Write-output ("Elastic Pool = " + (get-variable -Name ElasticPool -ValueOnly))

Write-Output "`n" 



Write-output " ####### App Service Plan Details##########"
Write-Output "`n"   

   Write-output ("Total App service Plans = " + (get-variable -Name appplanecount -ValueOnly))
Write-output ("Numbers of Sites = " + (get-variable -Name noofsites -ValueOnly))

Write-output ("Basic Tier = " + (get-variable -Name basics -ValueOnly))
Write-output ("Standard Tier = " + (get-variable -Name Standard -ValueOnly))
Write-output ("Free Tier = " + (get-variable -Name free -ValueOnly))
Write-output ("Conusmption Plan Tier = " + (get-variable -Name dynamic -ValueOnly))









$excel = New-Object -ComObject excel.application 




$workbook = $excel.Workbooks.Add()


$workbook.Worksheets.Item(1).Name = "Details"



$db = $workbook.Worksheets.Item("Details")

#$dbdetail = $workbook.Worksheets.Item("Databases")



$db.cells.item(2,2) = "Total Databases"
$db.cells.item(2,3) = $allskus.Count

$db.cells.item(4,2) = "Basic"
$db.cells.item(4,3) = (get-variable -Name Basic -ValueOnly)

$db.cells.item(5,2) = "Elastic Pool"
$db.cells.item(5,3) = (get-variable -Name ElasticPool -ValueOnly)





for ($a=0; $a -le ([int][convert]::ToInt32($maxsku[1], 10)); $a++) {

    $db.cells.item(($a+6),2) = "S$a"
    $db.cells.item(($a+6),3) = (get-variable -Name S$a -ValueOnly)
    

}





$db.cells.item(2,10) = "Total AppService Plans"
$db.cells.item(2,11) = (get-variable -Name appplanecount -ValueOnly)

$db.cells.item(4,10) = "Number of Sites"
$db.cells.item(4,11) = (get-variable -Name noofsites -ValueOnly)

$db.cells.item(5,10) = "Basic Tier"
$db.cells.item(5,11) = (get-variable -Name basics -ValueOnly)

$db.cells.item(6,10) = "Standard Tier"
$db.cells.item(6,11) = (get-variable -Name Standard -ValueOnly)

$db.cells.item(7,10) = "Free Tier"
$db.cells.item(7,11) = (get-variable -Name free -ValueOnly)

$db.cells.item(8,10) = "Consumption Tier"
$db.cells.item(8,11) = (get-variable -Name dynamic -ValueOnly)









$workbook.Worksheets.Add()

$workbook.Worksheets.Item(1).Name = "Databases"

$dbdetail = $workbook.Worksheets.Item("Databases")


$dbdetail.cells.item(2,2) = "Database"
$dbdetail.cells.item(2,3) = "Tier"
$dbdetail.cells.item(2,4) = "Server"


for ($t=0; $t -lt $sqlDBSource.Count; $t++) {

    $dbdetail.cells.item(($t+4),2) = $sqlDBSource[$t].DatabaseName
    $dbdetail.cells.item(($t+4),3) = $sqlDBSource[$t].CurrentServiceObjectiveName
    $dbdetail.cells.item(($t+4),4) = $sqlDBSource[$t].ServerName

}






$excel.visible = $True