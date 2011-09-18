PROJECT_NAME = "SimpleCMS"
BUILD_CONFIG = "Debug"

def run_command command
	begin
	  sh command
	rescue => e
	  puts "#{e}"
	end
end

def build target_name, target, build_config
	msbuild_path = "C:/Windows/Microsoft.NET/Framework/v4.0.30319/msbuild.exe"
	config = "#{target_name} /p:Configuration=#{build_config} /t:#{target} /nologo /verbosity:minimal"
	run_command "#{msbuild_path} #{config}"
end

def test test_project_name
	nunit_path = "lib/NUnit.2.5.9.10348/tools/nunit-console.exe"
	config = "src/tests/#{test_project_name}/bin/#{BUILD_CONFIG}/#{test_project_name}.dll /xml=build/#{test_project_name}.xml /nologo"
	run_command "#{nunit_path} #{config}"
end

#rake -T "shows tasks"
task :default => [:clean, :compile, :test]

desc "Remove dlls from bin (should be build folder)"
task :clean do
	build "#{PROJECT_NAME}.sln", "clean", BUILD_CONFIG
end

desc "Compile dlls into bin (should be build folder)"
task :compile => [:clean] do
	build "#{PROJECT_NAME}.sln", "build", BUILD_CONFIG
end

desc "Runs mixed unit/integration test suite"
task :test => [:compile] do
	test "#{PROJECT_NAME}.Tests"
end

desc "Compile console application for running db setup"
task :build_console do
	project_name = "src/app/SimpleCMS.Sandbox/SimpleCMS.Sandbox.csproj"
	build project_name, "build", BUILD_CONFIG
end

desc "Setup database (create & seed)"
task :db => [:build_console] do
	run_command "cd src\\app\\SimpleCMS.Sandbox\\bin\\#{BUILD_CONFIG} && SimpleCMS.Sandbox.exe db"
end

desc "Display routing information"
task :routes => [:build_console] do
	run_command "cd src\\app\\SimpleCMS.Sandbox\\bin\\#{BUILD_CONFIG} && SimpleCMS.Sandbox.exe routes"
end

desc "Setup website in iis"
task :install do
	sh "powershell ./deploy.ps1 -siteName #{PROJECT_NAME}"
end

desc "Remove website from iis"
task :uninstall do
	sh "powershell ./deploy.ps1 -siteName #{PROJECT_NAME} -clean true"
end