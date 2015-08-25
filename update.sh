#!/bin/bash
echo clean
sudo rm -r -f /var/www/at/ > compile.log
rm -r -f ~/raspberry-remote
echo clone git repo
git clone git://github.com/atabaksahraei/raspberry-remote.git >> compile.log
echo copy webservice files
sudo mkdir /var/www/at >> compile.log
sudo cp -r ~/raspberry-remote/webinterface/* /var/www/at >> compile.log
cd ~/raspberry-remote/core >> compile.log
echo compile: send
make send >> compile.log
echo compile: daemon
make daemon >> compile.log
echo run daemon
sudo ./daemon
cd ~