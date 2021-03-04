//
//  main.cpp
//  opencv
//
//  Created by 김현우 on 2021/03/04.
//

#include <iostream>
//https://www.vbflash.net/83
#include <opencv2/opencv.hpp>

using namespace std;
using namespace cv;

bool compareContourAreas (std::vector<cv::Point>, std::vector<cv::Point>);

int main(int argc, const char * argv[]) {
    // insert code here...
    std::cout << "Hello, World!\n";
    cout << "Open CV Version" << CV_VERSION << endl;
    
    Mat img = imread("/Users/stories2/Documents/GitHub/Kangnam-AR/IMG_3965.jpg", IMREAD_COLOR);
    imshow("img", img);
    
    
    float ratio = 700.0 / img.size().height;
    
    Mat smallImg;
    resize(img, smallImg, Size(int(img.size().width * ratio), 700));
    
    imshow("smallImg", smallImg);
    
    Mat gray;
    cvtColor(smallImg, gray, COLOR_BGR2GRAY);
    imshow("gray", gray);
    
    Mat grayBlur;
    GaussianBlur(gray, grayBlur, Size(3, 3), BORDER_CONSTANT);
    imshow("grayBlur", grayBlur);
    
    Mat edge;
    Canny(grayBlur, edge, 100, 200);
    imshow("edge", edge);
    
    vector<vector<Point>> contours;
    vector<Vec4i> hierarchy;
    findContours( edge, contours, hierarchy, RETR_LIST, CHAIN_APPROX_SIMPLE );
    sort(contours.begin(), contours.end(), compareContourAreas);
    
    vector<vector<Point>> topContours = vector<vector<Point>>(contours.end() - 5, contours.end());
    Mat smallImg_copy = smallImg.clone();
    
    vector<Point> screenContours;
    
    for (unsigned long i = topContours.size() - 1; i >= 0; i--) {
        double peri = arcLength(topContours[i], true);
        vector<Point> approx;
        approxPolyDP(topContours[i], approx, 0.02 * peri, true);
        
        if (approx.size() == 4) {
            screenContours = approx;
            break;
        }
    }
    
    if (screenContours.size() <= 0) {
        return 0;
    }

    vector<vector<Point>> screenContours_vec;
    screenContours_vec.push_back(screenContours);

    drawContours(smallImg_copy, screenContours_vec, -1, CV_RGB(0, 255, 0), 2);
    imshow("contours", smallImg_copy);
    
    waitKey(0);
    
    smallImg_copy.release();
    edge.release();
    grayBlur.release();
    gray.release();
    smallImg.release();
    img.release();
    
    return 0;
}

bool compareContourAreas ( std::vector<cv::Point> contour1, std::vector<cv::Point> contour2 ) {
    double i = fabs( contourArea(cv::Mat(contour1)) );
    double j = fabs( contourArea(cv::Mat(contour2)) );
    return ( i < j );
}
