module Build

#r "packages/FAKE/tools/FakeLib.dll"
#load "packages/Mag.Build/tools/mag.fsx"

open Fake
open MAG

MAG.Build(fun b ->
  { b with product = "Chassis"
           description = "MAG App Host"
           dbs =
             [| |]
           nugets =
             [| { name = "Chassis"
                  version = "1.0"
                  nuspec = "Package.nuspec"
                  dependencies = [
                                  ("Autofac", "3.5.2");
                                  ("Autofac.Extras.Multitenant", "3.1.1");
                      ]
                  files = [ (@"**/*.*"), Some "lib", None ]
                  copyFiles =
                    (fun target -> CopyFile target ("./src/Chassis/bin/" @@ b.mode @@ "/Chassis.dll")) } |]
           dotCoverFilter = [| "-:*.UnitTests;" |] })
