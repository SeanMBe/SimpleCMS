param(  
	$siteName,
    $hostName = "*:80:",
	$webdir = "C:\inetpub\wwwroot",
	$clean
)

if([string]::IsNullOrEmpty($hostName) -or [string]::IsNullOrEmpty($siteName)) {
	Write-Host ""
	Write-Host "Missing required arguments!" -ForegroundColor Red
	Write-Host ""
	Write-Host "Examples:"
	Write-Host ""
	Write-Host "deploy -siteName mySite -hostName localhost"
	Write-Host ""
	Write-Host " -siteName       		New site name to be created"	
	Write-Host " -hostName     			hostname for site"
	Write-Host " -webdir        		Directory root for IIS Site (default C:\inetpub\wwwroot)"
	Write-Host " -clean        		  	Delete site (default false)"
	exit 1
}

# load helper functions
. .\functions.ps1

# stop on errors
$ErrorActionPreference = "Stop"

if ($clean -eq $true) {
	removeIISSite $siteName
	removeAppPool $siteName
	removeAppDirectory $webdir $siteName
	Write-Host "cleaning completed" -ForegroundColor Green
	exit 0
}

createAppDirectory $webdir $siteName
createAppPool $siteName
createIISSite $webdir $siteName $hostName
Write-Host "deploy completed"  -ForegroundColor Green