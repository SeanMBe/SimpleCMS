[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.Web.Administration")
$iis = new-object Microsoft.Web.Administration.ServerManager

function createAppDirectory($webdir, $siteName) {
	Write-Host "app directory..." -nonewline
	if (Test-Path "$webdir\$siteName") {
		Write-Host "already exists"
	} else {
		new-item "$webdir\$siteName" -type directory | out-null
		Write-Host "created"
	}
}

function removeAppDirectory($webdir, $siteName) {
	Write-Host "app directory..." -nonewline
	if (Test-Path "$webdir\$siteName") {
		Remove-Item "$webdir\$siteName" -Recurse
		Write-Host "removed"
	} else {
		Write-Host "does not exist"
	}
}

function createAppPool($siteName) {
	Write-Host "app pool..." -nonewline
	if ($iis.ApplicationPools["$siteName"]) {
		Write-Host "already exists"
	} else {
		$appPool = $iis.ApplicationPools.Add("$siteName");
		$apppool.ManagedRuntimeVersion = "v4.0";
		$apppool.Enable32BitAppOnWin64 = $TRUE
		$apppool.ManagedPipelineMode = 0; #integrated mode
		$appPool.processModel.identityType = "ApplicationPoolIdentity"
		$iis.CommitChanges();
		Write-Host "created"
	}
}

function removeAppPool($siteName) {
	Write-Host "app pool..." -nonewline
	if ($iis.ApplicationPools["$siteName"]) {
		$iis.ApplicationPools.Remove($iis.ApplicationPools["$siteName"]);
		$iis.CommitChanges();
		Write-Host "removed"
	} else {
		Write-Host "does not exist"
	}
}

function createIISSite($webdir, $siteName, $hostName, $httpPortNumber) {
	Write-Host "iis site..." -nonewline
	if ($iis.Sites["$siteName"]) {
		Write-Host "already exists"
	} else {
		#httpPortNumber
		$webSite = $iis.Sites.Add("$siteName","http", ":80:$hostName", "$webdir\$siteName");
		$webSite.Applications[0].ApplicationPoolName = "$siteName";
		$webSite.ServerAutoStart = $TRUE;
		$iis.CommitChanges();
		Write-Host "created"
	}
}

function removeIISSite($siteName) {
	Write-Host "iis site..." -nonewline
	if ($iis.Sites["$siteName"]) {
		$iis.Sites.Remove($iis.Sites["$siteName"])
		$iis.CommitChanges();
		Write-Host "removed"
	} else {
		Write-Host "does not exist"
	}
}