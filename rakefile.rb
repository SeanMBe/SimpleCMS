PROJECT_NAME = "SimpleCMS"
BUILD_CONFIG = "Debug"

def build target_name, target, build_config
	msbuild_path = "C:/Windows/Microsoft.NET/Framework/v4.0.30319/msbuild.exe"
	config = "#{target_name} /p:Configuration=#{build_config} /t:#{target} /nologo /verbosity:minimal"
	sh "#{msbuild_path} #{config}"
end

def test test_project_name
	nunit_path = "lib/NUnit.2.5.9.10348/tools/nunit-console.exe"
	config = "src/tests/#{test_project_name}/bin/#{BUILD_CONFIG}/#{test_project_name}.dll /xml=build/#{test_project_name}.xml /nologo"
	sh "#{nunit_path} #{config}"
end

task :default => [:clean, :compile, :test]

task :clean do
	build "#{PROJECT_NAME}.sln", "clean", BUILD_CONFIG
end

task :compile => [:clean] do
	build "#{PROJECT_NAME}.sln", "build", BUILD_CONFIG
end

task :test => [:compile] do
	test "#{PROJECT_NAME}.Tests"
end

task :build_console do
	project_name = "src/app/SimpleCMS.Sandbox/SimpleCMS.Sandbox.csproj"
	build project_name, "build", BUILD_CONFIG
end

task :db => [:build_console] do
	sh "cd src\\app\\SimpleCMS.Sandbox\\bin\\#{BUILD_CONFIG} && SimpleCMS.Sandbox.exe"
end

task :install do
	sh "powershell ./deploy.ps1 -siteName simplecms"
end

task :uninstall do
	sh "powershell ./deploy.ps1 -siteName simplecms -clean true"
end