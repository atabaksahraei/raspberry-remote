#!/bin/bash
cd ~/raspberry-remote
./cleanPort.sh
cd core
sudo ./daemon.sh &
exit 0