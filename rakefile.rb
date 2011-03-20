PROJECT = "SimpleCMS"
BUILD_CONFIG = "Debug"
MSBUILD_PATH = "C:/Windows/Microsoft.NET/Framework/v4.0.30319/msbuild.exe"
NUNIT_PATH = "lib/NUnit.2.5.9.10348/tools/nunit-console.exe"

task :default => ["build:all"]
 
namespace :build do
  
  task :all => [:compile, :test]

  task :compile do
    SOLUTION_NAME = "#{PROJECT}.sln"
    CONFIG = "/p:Configuration=#{BUILD_CONFIG} #{SOLUTION_NAME} /t:Rebuild /nologo /verbosity:minimal"
    sh "#{MSBUILD_PATH} #{CONFIG}"
  end
 
  task :test => [:compile] do
    TEST_PROJECT_NAME = "#{PROJECT}.Tests"
    CONFIG = "src/tests/#{TEST_PROJECT_NAME}/bin/#{BUILD_CONFIG}/#{TEST_PROJECT_NAME}.dll /xml=build/#{TEST_PROJECT_NAME}.xml /nologo"
    sh "#{NUNIT_PATH} #{CONFIG}"
  end
  
end