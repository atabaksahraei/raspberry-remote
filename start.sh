#!/bin/bash
cd ~/raspberry-remote
./cleanPort.sh
cd core
sudo ./daemon & >> daemon.log
exit 0