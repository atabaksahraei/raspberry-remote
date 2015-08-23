#!/bin/bash
sudo rm -r /var/www/at/
sudo mkdir /var/www/at
rm -r ~/raspberry-remote
git clone git://github.com/atabaksahraei/raspberry-remote.git
sudo cp ~/raspberry-remote/webinterface/* /var/www/at
cd ~/raspberry-remote/
make send
make daemon
sudo ./daemon
cd ~