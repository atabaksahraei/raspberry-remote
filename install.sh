#!/bin/bash
sudo apt-get update
sudo apt-get install git-core
cd ~
git clone git://git.drogon.net/wiringPi
cd wiringPi
./build
cd ~
git clone git://github.com/atabaksahraei/raspberry-remote.git
cp ~/raspberry-remote/update ~/update
cd ~
./update
