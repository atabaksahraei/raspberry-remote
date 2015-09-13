#!/bin/bash
cd ~/raspberry-remote/
./cleanPort.sh
cd ~/raspberry-remote/core/
sudo .daemon.sh &> /myOutput.log  &
echo $! >> my_pids.log
echo "PID of myJob.sh is: $!  - Saved to my_pids.log"
exit 0