require 'albacore'

desc "build using msbuild"
msbuild :build do |msb|
    msb.properties :configuration => :Debug
    msb.targets :Clean, :Rebuild
    msb.verbosity = 'quiet'
    msb.solution =File.join("UKanren", "UKanren.sln")
end

task :core_copy_to_nuspec => [:build] do
    output_directory_lib = File.join('nuget',"lib/Net40/")
    mkdir_p output_directory_lib
    cp Dir.glob(File.join("UKanren/UKanren/bin/Debug/*.dll")), output_directory_lib
end

desc "create the nuget package"
task :nugetpack => [:core_copy_to_nuspec] do |nuget|
    cd File.join("nuget") do
        sh "..\\UKanren\\.nuget\\NuGet.exe pack MikroKanren.nuspec"
    end
end


