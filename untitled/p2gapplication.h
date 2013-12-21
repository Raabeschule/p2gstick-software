#ifndef P2GAPPLICATION_H
#define P2GAPPLICATION_H

#include <QMainWindow>

namespace Ui {
class P2GApplication;
}

class P2GApplication : public QMainWindow
{
    Q_OBJECT
    
public:
    explicit P2GApplication(QWidget *parent = 0);
    ~P2GApplication();
    
private:
    Ui::P2GApplication *ui;
};

#endif // P2GAPPLICATION_H
