require 'albacore'

desc "build using msbuild"
msbuild :build do |msb|
    msb.properties :configuration => :Debug
    msb.targets :Clean, :Rebuild
    msb.verbosity = 'quiet'
    msb.solution =File.join("UKanren", "UKanren.sln")
end


