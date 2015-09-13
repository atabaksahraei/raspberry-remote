#!/bin/bash
echo clean
sudo rm -r -f /var/www/at/ > compile.log
#rm -r -f ~/raspberry-remote
#echo clone git repo
#git clone git://github.com/atabaksahraei/raspberry-remote.git >> compile.log
cd ~/raspberry-remote
git reset --hard origin/master
git pull
echo copy webservice files
sudo mkdir /var/www/at >> compile.log
sudo cp -r ~/raspberry-remote/webinterface/* /var/www/at >> compile.log
cd ~/raspberry-remote/core >> compile.log
echo compile: send
make send >> compile.log
echo compile: daemon
make daemon >> compile.log
cd ~/raspberry-remote
echo run start
sudo ./start.sh
cd ~