#include "OpenCVPlugin.h"

extern "C" {
    using namespace cv;
    using namespace std;
    //global variables
    uint8_t *resultBuffer;
    int bufferWidth;
    int bufferHeight;
    // In order to save memory, we call this method to initialize our buffer to the
    // size actually needed by Unity assuming that we will not resize our video buffer
    int InitCV_Internal(int width, int height) {
        size_t size = width * height * 4;
        resultBuffer = new uint8_t[size];

        bufferWidth = width;
        bufferHeight = height;

        return 0;
    }

    // This method is called every frame to apply OpenCV processes to the buffer. Due
    // to passing the whole buffer back and forth being expensive, it would be better
    // to pull camera data from Android and only pass it up to Unity for debug purposes.

    uint8_t *SubmitFrame_Internal(int width, int height, uint8_t *buffer) {

        Mat inFrame(height, width, CV_8UC4, buffer);
        Mat outFrame(inFrame.rows, inFrame.cols, CV_8UC4, Scalar(0, 0, 0));

        Mat processingFrame;

        cvtColor(inFrame, processingFrame, COLOR_RGBA2GRAY);
        Canny(processingFrame, processingFrame, 0, 30, 3);
        cvtColor(processingFrame, outFrame, COLOR_GRAY2RGBA);

        size_t size = bufferWidth * bufferHeight * 4;
        memcpy(resultBuffer, outFrame.data, size);

        inFrame.release();
        outFrame.release();
        processingFrame.release();

        return resultBuffer;
    }
    // This method only exists to ensure that we are getting correct values back from the native
    // side using a simple integer return instead of a more complex byte array.
    int FooTestFunction_Internal() {
        return 12345;
    }

    uint8_t  *resultPicBuffer;
    int picRows = 0;
    int picCols = 0;

    int ResultPicBufferRows() {
        return picRows;
    }

    int ResultPicBufferCols() {
        return picCols;
    }



    uint8_t *ExportPicFromDoc(int width, int height, uint8_t *buffer) {

        Mat img(height, width, CV_8UC4, buffer);

        float maxImgWidth = 2000.0;
        float ratio = maxImgWidth / img.size().height;

        Mat smallImg;
        resize(img, smallImg, Size(int(img.size().width * ratio), maxImgWidth));

    //        imshow("smallImg", smallImg);

        Mat gray;
        cvtColor(smallImg, gray, COLOR_BGR2GRAY);
    //        imshow("gray", gray);

        Mat grayBlur;
        GaussianBlur(gray, grayBlur, Size(3, 3), BORDER_CONSTANT);
    //        imshow("grayBlur", grayBlur);

        Mat edge;
        Canny(grayBlur, edge, 100, 200);
    //        imshow("edge", edge);

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
            picRows = 0;
            picCols = 0;
            return 0;
        }

        vector<vector<Point>> screenContours_vec;
        screenContours_vec.push_back(screenContours);

        drawContours(smallImg_copy, screenContours_vec, -1, CV_RGB(0, 255, 0), 2);
    //        imshow("contours", smallImg_copy);

        Point topLeft, topRight, bottomRight, bottomLeft;
        topLeft.x = 0x0fffffff;
        topLeft.y = 0x0fffffff;

        topRight.y = 0x7fffffff;

        for (unsigned long i = 0; i < screenContours.size(); i++) {
            if (topLeft.x + topLeft.y > screenContours[i].x + screenContours[i].y) {
                topLeft = screenContours[i];
            }

            if (topRight.y - topRight.x > screenContours[i].y - screenContours[i].x) {
                topRight = screenContours[i];
            }

            if (bottomRight.x + bottomRight.y < screenContours[i].x + screenContours[i].y) {
                bottomRight = screenContours[i];
            }

            if (bottomLeft.y - bottomLeft.x < screenContours[i].y - screenContours[i].x) {
                bottomLeft = screenContours[i];
            }
        }

        unsigned long padding = 10;
        topLeft.x += padding;
        topLeft.y += padding;
        topRight.x -= padding;
        topRight.y += padding;
        bottomLeft.x += padding;
        bottomLeft.y -= padding;
        bottomRight.x -= padding;
        bottomRight.y -= padding;

        unsigned long width1 = abs(topLeft.x - topRight.x), width2 = abs(bottomLeft.x - bottomRight.x),
                height1 = abs(topLeft.y - bottomLeft.y), height2 = abs(topRight.y - bottomRight.y);

        unsigned long maxWidth = max(width1, width2), maxHeight = max(height1, height2);

        vector<Point2f> srcRect;
        srcRect.push_back(topLeft);
        srcRect.push_back(topRight);
        srcRect.push_back(bottomLeft);
        srcRect.push_back(bottomRight);

        vector<Point2f> destRect;
        destRect.push_back(Point(0, 0));
        destRect.push_back(Point(maxWidth, 0));
        destRect.push_back(Point(0, maxHeight));
        destRect.push_back(Point(maxWidth, maxHeight));

        Mat perspectMat = getPerspectiveTransform(srcRect, destRect);
        Mat warpedImg;
        warpPerspective(smallImg, warpedImg, perspectMat, Size(maxWidth, maxHeight));
    //        imshow("warpedImg", warpedImg);

        Mat warpedImgGray;
        cvtColor(warpedImg, warpedImgGray, COLOR_BGR2GRAY);
        adaptiveThreshold(warpedImgGray, warpedImgGray, 255, ADAPTIVE_THRESH_MEAN_C, THRESH_BINARY, 21, 10);
    //        imshow("adapted", warpedImgGray);

        Mat edgePic;
        GaussianBlur(warpedImgGray, edgePic, Size(11, 11), BORDER_CONSTANT);
        Canny(edgePic, edgePic, 100, 200);
    //        imshow("edgePic", edgePic);

        vector<vector<Point>> contoursPic;
        vector<Vec4i> hierarchyPic;
        findContours( edgePic, contoursPic, hierarchyPic, RETR_LIST, CHAIN_APPROX_SIMPLE );

        Mat edgePic_copy = warpedImg.clone();
        drawContours(edgePic_copy, contoursPic, -1, CV_RGB(0, 255, 0), 2);
    //        imshow("edgePic_copy", edgePic_copy);

        Mat onlyContours = Mat(Size(edgePic_copy.cols, edgePic_copy.rows), CV_8UC3);
        drawContours(onlyContours, contoursPic, -1, CV_RGB(255, 255, 255), 2);
        cvtColor(onlyContours, onlyContours, COLOR_BGR2GRAY);
    //        imshow("onlyContours", onlyContours);

        picRows = onlyContours.rows;
        picCols = onlyContours.cols;

        size_t size = picRows * picCols * 3;
        memcpy(resultPicBuffer, onlyContours.data, size);

        onlyContours.release();
        edgePic_copy.release();
        edgePic.release();
        warpedImgGray.release();
        warpedImg.release();
        smallImg_copy.release();
        edge.release();
        grayBlur.release();
        gray.release();
        smallImg.release();
        img.release();

        return resultPicBuffer;
    }

    bool compareContourAreas ( std::vector<cv::Point> contour1, std::vector<cv::Point> contour2 ) {
        double i = fabs( contourArea(cv::Mat(contour1)) );
        double j = fabs( contourArea(cv::Mat(contour2)) );
        return ( i < j );
    }
}