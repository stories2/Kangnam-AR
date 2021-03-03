#include <jni.h>
#include <string>
//#include <opencv2/opencv.hpp>
//#include <opencv2/opencv2.hpp>
#include "opencv2/opencv.hpp"

extern "C" JNIEXPORT jstring JNICALL
Java_com_example_myapplication_MainActivity_stringFromJNI(
        JNIEnv* env,
        jobject /* this */) {
    std::string hello = "Hello from C++ CV VERSION: ";
    hello.append(CV_VERSION);
    return env->NewStringUTF(hello.c_str());
}