try:
    from setuptools import setup
    from setuptools import Extension
except ImportError:
    from distutils.core import setup
    from distutils.extension import Extension

from Cython.Distutils import build_ext
import numpy as np

ext_modules = [Extension("HR_PYD",["main_To_PYD.py"]),]

setup(
    name= 'Generic model class',
    cmdclass = {'build_ext': build_ext},
    include_dirs = [np.get_include()],
    ext_modules = ext_modules)
'''
from distutils.core import setup
from Cython.Build import cythonize

setup(name = 'Hello world', ext_modules=cythonize("test.py"),)

'''