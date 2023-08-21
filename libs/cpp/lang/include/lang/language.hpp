#ifndef CL_LANG_LANGUAGE
#define CL_LANG_LANGUAGE

namespace cl::lang {
  class language_base {
  public:
    virtual int r() = 0;
    virtual ~language_base() {}
  };
}

#endif
