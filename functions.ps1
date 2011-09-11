Import-Module WebAdministration;

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
	if (Test-Path "IIS:\AppPools\$siteName") {
		Write-Host "already exists"
	} else {
		$appPool = new-item "IIS:\AppPools\$siteName"
		Set-ItemProperty "IIS:\AppPools\$siteName" -name managedPipelineMode -value 0 #Integrated
		Set-ItemProperty "IIS:\AppPools\$siteName" -name managedRuntimeVersion -value "v4.0" #Runtime
		Write-Host "created"
	}
}

function removeAppPool($siteName) {
	Write-Host "app pool..." -nonewline
	if (Test-Path "IIS:\AppPools\$siteName") {
		Remove-Item "IIS:\AppPools\$siteName" -Recurse -WarningAction SilentlyContinue
		Write-Host "removed"
	} else {
		Write-Host "does not exist"
	}
}

function createIISSite($webdir, $siteName) {
	Write-Host "iis site..." -nonewline
	if (Test-Path "IIS:\Sites\$siteName") {
		Write-Host "already exists"
	} else {
		$httpBinding = "*:" + $httpPortNumber + ":" + $hostName
		$bindings = @() + @{protocol="http";bindingInformation=$httpBinding} 
		new-item "IIS:\Sites\$siteName" -bindings $bindings -physicalPath "$webdir\$siteName" | Out-Null
		set-itemproperty "IIS:\Sites\$siteName" -name applicationpool -value "$siteName" | Out-Null
		set-itemproperty "IIS:\Sites\$siteName" -name logFile.directory -value $logdir | Out-Null
		Write-Host "created"
	}
}

function removeIISSite($webdir, $siteName) {
	Write-Host "iis site..." -nonewline
	if (Test-Path "IIS:\Sites\$siteName") {
		Remove-Item "IIS:\Sites\$siteName" -Recurse -WarningAction SilentlyContinue
		Write-Host "removed"
	} else {
		Write-Host "does not exist"
	}
}

function setIISDefaults() {
	Write-Host "setting iis defaults..."
	$filter = "/system.applicationHost/applicationpools/applicationPoolDefaults"
	$name = "enable32BitAppOnWin64"
	Set-WebConfigurationProperty -filter $filter -name $name -value "false"
}