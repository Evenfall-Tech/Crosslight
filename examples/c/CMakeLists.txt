find_package(LLVM CONFIG)

if(LLVM_FOUND)
	find_package(ZLIB REQUIRED)
	add_subdirectory("ast")
else()
	message(STATUS "LLVM or ZLIB not found")
	message(STATUS "Skipping ast")
endif()

if((TARGET cl_lang_csharp_ref) AND (TARGET cl_lang_typescript))
	add_subdirectory("parse")
endif()
