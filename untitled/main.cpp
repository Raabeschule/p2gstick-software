#include <QApplication>
#include "p2gapplication.h"

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    P2GApplication w;
    w.show();
    
    return a.exec();
}
