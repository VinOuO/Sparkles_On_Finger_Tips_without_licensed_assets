from distutils.core import setup
from distutils.extension import Extension
from Cython.Distutils import build_ext
from Cython.Build import cythonize

'''
#ext_modules = [Extension("Test3",  ["V:\Work\Projects\Project CS\SubProject HR\main.py"]),]

setup(
    name = 'Test_PYD',
    ext_modules = cythonize(("V:\Work\Projects\Project CS\SubProject HR\main.py")),
)
'''

ext_modules = [
    Extension("main",  ["main.py"]),
]

setup(
    name = 'My Program',
    cmdclass = {'build_ext': build_ext},
    ext_modules = ext_modules
)