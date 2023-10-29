set(PROJECT_NAME "crosslight_languages")
project(
    "${PROJECT_NAME}"
    VERSION ${CL_VERSION_STRING}
    LANGUAGES CXX
)

add_subdirectory("lang")
add_subdirectory("lang_base_antlr")
add_subdirectory("lang_typescript")
