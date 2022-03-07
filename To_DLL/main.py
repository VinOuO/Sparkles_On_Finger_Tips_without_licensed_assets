import compileall
import os
import  Test_PYD

lib_path = "V:\Work\Projects\Project CS\SubProject HR"
build_path = "V:\Work\Projects\Project CS\To_DLL\Dest"

compileall.compile_dir(lib_path, force=True, legacy=True)

def moveToNewLocation(cu_path):
    for file in os.listdir(cu_path):
        if os.path.isdir(os.path.join(cu_path, file)):
            A = 1
            #compile(os.path.join(cu_path, file))
            #compile(os.path.join(cu_path, file), "Test", "exec")
        elif file.endswith(".pyc"):
            dest = os.path.join(build_path, cu_path ,file)
            os.makedirs(os.path.dirname(dest), exist_ok=True)
            os.rename(os.path.join(cu_path, file), dest)

moveToNewLocation(lib_path)