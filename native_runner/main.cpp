#include <iostream>
#include <fstream>
#include <cstdlib>

#include <glib.h>
#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>

using namespace std;

int main(int argc, char* argv[]) {
	try {
		char file_name[] = "runner.exe";

		mono_set_dirs("mono/lib", "mono/etc");

		MonoDomain *domain = mono_jit_init (file_name);

		MonoAssembly *assembly = mono_domain_assembly_open(domain, file_name);
		if (!assembly) {
			cerr << "Error! Could not find " << file_name << "!" << endl;
		}
		else {
			#if 1
			MonoImage *image = mono_assembly_get_image (assembly);
			MonoMethod *method;
			guint32 entry = mono_image_get_entry_point (image);

			if (!entry) {
				g_print ("Assembly '%s' doesn't have an entry point.\n", mono_image_get_filename (image));
				/* FIXME: remove this silly requirement. */
				//mono_environment_exitcode_set (1);
				return EXIT_FAILURE;
			}

			method = mono_get_method (image, entry, NULL);
			if (method == NULL){
				g_print ("The entry point method could not be loaded\n");
				//mono_environment_exitcode_set (1);
				return EXIT_FAILURE;
			}

			int retval = mono_runtime_run_main (method, argc, argv, NULL);

			#else
			//Simply calling mono_jit_exec doesn't work,
			//It produces a SIGSEGV...
			int retval = mono_jit_exec (domain, assembly, argc - 1, argv + 1);
			#endif
		}
		mono_jit_cleanup(domain);

	}
	catch(...) {
		cerr << "Error" << endl;
		return EXIT_FAILURE;
	}


	return EXIT_SUCCESS;
}
