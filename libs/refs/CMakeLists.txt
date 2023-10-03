if(CL_BUILD_CSHARP AND CL_DOTNET_COMMAND)
    message(VERBOSE "Dotnet executable at ${CL_DOTNET_COMMAND}")
    message(VERBOSE "Compiling for system ${CMAKE_SYSTEM_NAME}")
    message(VERBOSE "Compiling for processor ${CMAKE_SYSTEM_PROCESSOR}")

    if(CL_DOTNET_RID)
        add_subdirectory("lang_csharp_ref")
    endif()
endif()
