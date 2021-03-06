#include "OpenCVPlugin.h"

extern "C" JNIEXPORT jstring JNICALL
Java_com_example_myapplication_MainActivity_stringFromJNI(
        JNIEnv* env,
        jobject /* this */) {
    std::string hello = "Hello from C++ CV VERSION: ";
    hello.append(CV_VERSION);
    return env->NewStringUTF(hello.c_str());
}