[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.Web.Administration")
$iis = new-object Microsoft.Web.Administration.ServerManager

$siteName = "SimpleCMS"
$hostName = "localhost"
$webdir = "C:\inetpub\wwwroot"

# Create directory
new-item "$webdir\$siteName" -type directory | out-null

# Create IIS Application Pool...
if ($iis.ApplicationPools["$siteName"]) {
	Write-Host "Pool exists"
}
$appPool = $iis.ApplicationPools.Add("$siteName");
$appPool.processModel.identityType = "NetworkService"
$apppool.ManagedPipelineMode = 1;

# Create WebSite and assign host name to it
$webSite = $iis.Sites.Add("$siteName","http", ":80:$hostName", "$webdir\$siteName");
$webSite.Applications[0].ApplicationPoolName = "$siteName";
$webSite.ServerAutoStart = $TRUE;

# Save the changes
$iis.CommitChanges();