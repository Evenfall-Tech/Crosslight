find_package(LLVM CONFIG)

if(LLVM_FOUND)
	add_subdirectory("ast")
else()
	message(STATUS "LLVM not found")
	message(STATUS "Skipping ast")
endif()
