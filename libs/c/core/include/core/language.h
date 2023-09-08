#pragma once

#include "core/definitions.h"
#include "core/resource.h"
#include "core/node.h"

#define CL_RESULT int

CL_C_DECL
CL_RESULT
language_init();

CL_C_DECL struct cl_resource_types language_resource_types_input();

CL_C_DECL struct cl_resource_types language_resource_types_output();
