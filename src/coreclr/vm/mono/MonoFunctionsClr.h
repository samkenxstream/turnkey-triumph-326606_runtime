
#include "../../../../unity/unity-sources/Runtime/Mono/MonoTypes.h"

// Mono CoreCLR specifics
DO_API(void, mono_gc_mark_stack_slot, (void* objRef))
DO_API(void, mono_gc_unmark_stack_slot, (void* objRef))
DO_API(void, mono_debug_assert_dialog, (const char *szFile, int iLine, const char *szExpr))
DO_API(gboolean, mono_gc_preemptive, (gboolean enable))
DO_API(MonoObject*, mono_runtime_invoke_with_nested_object, (MonoMethod *method, void *obj, void *parentobj, void **params, MonoException **exc))
DO_API(int, mono_type_get_num_generic_args, (MonoType *type))
DO_API(MonoType*, mono_type_get_generic_arg, (MonoType *type, int index))
DO_API(MonoType*, mono_field_get_type_specific, (MonoClassField *field, MonoClass* owner))
DO_API(void, mono_enter_internal_call,  (MonoInternalCallFrameOpaque *frame))
DO_API(void, mono_exit_internal_call,  (MonoInternalCallFrameOpaque *frame))
DO_API(void, mono_set_gc_conservative,  (bool conservative))

#define ENABLE_MONO_MEMORY_PROFILER 1

// Include regular Unity Mono functions
#include "../../../../unity/unity-sources/Runtime/Mono/MonoFunctions.h"
