#include "p2gapplication.h"
#include "ui_p2gapplication.h"

P2GApplication::P2GApplication(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::P2GApplication)
{
    ui->setupUi(this);
}

P2GApplication::~P2GApplication()
{
    delete ui;
}
