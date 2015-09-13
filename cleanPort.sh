#!/bin/bash
sudo netstat -tulpn | grep 11337 | awk '{print $7}' | cut -d'/' -f1 | xargs kill -9