PROJECT = "SimpleCMS"
BUILD_CONFIG = "Debug"

def build target_name, target
	msbuild_path = "C:/Windows/Microsoft.NET/Framework/v4.0.30319/msbuild.exe"
	config = "#{target_name} /p:Configuration=#{BUILD_CONFIG} /t:#{target} /nologo /verbosity:minimal"
	sh "#{msbuild_path} #{config}"
end

def test test_project_name
	nunit_path = "lib/NUnit.2.5.9.10348/tools/nunit-console.exe"
	config = "src/tests/#{test_project_name}/bin/#{BUILD_CONFIG}/#{test_project_name}.dll /xml=build/#{test_project_name}.xml /nologo"
	sh "#{nunit_path} #{config}"
end

task :default => [:clean, :compile, :test]

task :clean do
	build "#{PROJECT}.sln", "clean"
end

task :compile => [:clean] do
	build "#{PROJECT}.sln", "build"
end

task :test => [:compile] do
	test "#{PROJECT}.Tests"
end

task :build_console do
	project_name = "src/app/SimpleCMS.Sandbox/SimpleCMS.Sandbox.csproj"
	build project_name, "build"
end

task :db => [:build_console] do
	sh "src\\app\\SimpleCMS.Sandbox\\bin\\#{BUILD_CONFIG}\\SimpleCMS.Sandbox.exe"
end