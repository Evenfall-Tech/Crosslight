find_package(LLVM CONFIG)
find_package(ZLIB CONFIG)

if(LLVM_FOUND AND ZLIB_FOUND)
	add_subdirectory("ast")
else()
	message(STATUS "LLVM or ZLIB not found")
	message(STATUS "Skipping ast")
endif()

add_subdirectory("parse")
