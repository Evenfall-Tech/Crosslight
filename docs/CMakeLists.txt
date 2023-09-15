set(DOXYGEN_BUILTIN_STL_SUPPORT YES)

doxygen_add_docs(
    cl_docs
    "${CMAKE_CURRENT_SOURCE_DIR}/index.md"
    "${CMAKE_CURRENT_SOURCE_DIR}/notes"
    "${PROJECT_SOURCE_DIR}/libs/c/core/include"
    "${PROJECT_SOURCE_DIR}/libs/cpp/lang/include"
)
