//
//  OpenCVPlugin.hpp
//  opencv
//
//  Created by 김현우 on 2021/03/06.
//

#ifndef OpenCVPlugin_hpp
#define OpenCVPlugin_hpp

#include <iostream>
//https://www.vbflash.net/83
#include <opencv2/opencv.hpp>

using namespace std;
using namespace cv;

struct Color32
{
    uchar red;
    uchar green;
    uchar blue;
    uchar alpha;
};

int FooTestFunction_Internal();
int ResultPicBufferRows();
int ResultPicBufferCols();
bool compareContourAreas (std::vector<cv::Point>, std::vector<cv::Point>);
unsigned char* ExportPicFromDoc(int width, int height, unsigned char* buffer);
unsigned char* GetResultPicBuffer();
void FreeBuffer();
void TestMat(int width, int height, unsigned char* data);
void ReturnGlobalMat(unsigned char* data);
void FlipImage(Color32 **rawImage, int width, int height);

#endif /* OpenCVPlugin_hpp */
