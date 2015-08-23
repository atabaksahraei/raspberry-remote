#!/bin/bash
sudo rm -r /var/www/at/
sudo mkdir /var/www/at
rm -r ~/raspberry-remote
git clone git://github.com/atabaksahraei/raspberry-remote.git
sudo mv ~/raspberry-remote/webinterface/* /var/www/at
make ~/raspberry-remote/send
make ~/raspberry-remote/daemon
sudo ~/raspberry-remote/daemon