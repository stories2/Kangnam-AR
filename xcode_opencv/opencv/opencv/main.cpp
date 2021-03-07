//
//  main.cpp
//  opencv
//
//  Created by 김현우 on 2021/03/04.
//

#include "OpenCVPlugin.hpp"

int main(int argc, const char * argv[]) {
    // insert code here...
    std::cout << "Hello, World!\n";
    cout << "Open CV Version" << CV_VERSION << endl;
    
    Mat img = imread("/Users/stories2/Documents/GitHub/Kangnam-AR/IMG_3993.png", IMREAD_COLOR);
    imshow("img origin", img);
    unsigned char* ptr = ExportPicFromDoc(img.size().width, img.size().height, img.data);
    img.release();
    return 0;
}
