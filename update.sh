#!/bin/bash
echo clean
sudo rm -r /var/www/at/
rm -r ~/raspberry-remote
echo clone git repo
git clone git://github.com/atabaksahraei/raspberry-remote.git
echo copy webservice files
sudo mkdir /var/www/at
sudo cp ~/raspberry-remote/webinterface/* /var/www/at
cd ~/raspberry-remote/core
echo compile: send
make send
echo compile: daemon
make daemon
echo run daemon
sudo ./daemon
cd ~