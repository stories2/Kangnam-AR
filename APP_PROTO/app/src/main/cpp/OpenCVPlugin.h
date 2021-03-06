//
// Created by 김현우 on 3/6/21.
//

#ifndef MY_APPLICATION_OPENCVPLUGIN_H
#define MY_APPLICATION_OPENCVPLUGIN_H
#include <jni.h>
#include <string>
#include "opencv2/opencv.hpp"
#include "opencv2/core.hpp"
#include "opencv2/imgproc.hpp"

#include <iostream>

extern "C" int InitCV_Internal(int , int );
extern "C" uint8_t *SubmitFrame_Internal(int , int , uint8_t *);
extern "C" int FooTestFunction_Internal();
extern "C" int ResultPicBufferRows();
extern "C" int ResultPicBufferCols();
extern "C" bool compareContourAreas (std::vector<cv::Point>, std::vector<cv::Point>);
extern "C" uint8_t *ExportPicFromDoc(int , int , uint8_t *);
#endif //MY_APPLICATION_OPENCVPLUGIN_H